#ifdef _WIN32
#pragma comment( lib, "Ws2_32.lib")
#include <WinSock2.h>
#include <WS2tcpip.h>
#elif
#include <arpa/inet.h>
#include <sys/types.h>
#include <sys/socket.h>
#endif

#include <iostream>
#include <io.h>
#include <memory>
#include <openssl/x509.h>
#include <openssl/ssl.h>
#include <openssl/bio.h>
#include <openssl/pem.h>
#include <string>

using BIO_MEM_ptr = std::unique_ptr<BIO, decltype(&::BIO_free)>;

extern "C" {
    __declspec(dllexport) BSTR GetDevicePeerCertificate(const char* ip);
}

// Based on: http://www.zedwood.com/article/cpp-libssl-get-peer-certificate
BSTR GetDevicePeerCertificate(const char* ip) {
    struct sockaddr_in sa {};
    SSL* ssl;
    X509* server_cert;
    BUF_MEM* mem = NULL;
    std::string pem;
    WSADATA wsaData;

    SSLeay_add_ssl_algorithms();
    SSL_load_error_strings();
    SSL_CTX* ctx = SSL_CTX_new(SSLv23_method());

#ifdef _WIN32
    auto isReady = WSAStartup(MAKEWORD(2, 2), &wsaData);

    if (isReady == NOERROR) {
        return NULL;
    }
#endif

    auto sd = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    BIO_MEM_ptr bio(BIO_new(BIO_s_mem()), ::BIO_free);

    if (sd != SOCKET_ERROR && ctx != NULL) {
        memset(&sa, '\0', sizeof(sa));
        sa.sin_family = AF_INET;
        sa.sin_addr.s_addr = inet_addr(ip);
        sa.sin_port = htons(8009);

        int err = connect(sd, (struct sockaddr*)&sa, sizeof(sa));
        if (err != INVALID_SOCKET) {
            ssl = SSL_new(ctx);
            if (ssl != NULL) {
                SSL_set_fd(ssl, sd);
                err = SSL_connect(ssl);
                if (err != -1) {
                    server_cert = SSL_get_peer_certificate(ssl);
                    if (server_cert != NULL) {
                        if (bio) {
                            PEM_write_bio_X509(bio.get(), server_cert);
                            BIO_get_mem_ptr(bio.get(), &mem);
                            pem = std::string(mem->data, mem->length);
                        }
                        X509_free(server_cert);
                    }
                }
                SSL_free(ssl);
                SSL_CTX_free(ctx);
            }
        }
    }
    
    auto allocPemStr = SysAllocStringByteLen(pem.c_str(), pem.size());

    return allocPemStr;
}