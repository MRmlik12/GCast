﻿using System.Runtime.InteropServices;

namespace GCast.Protocol.Cert;

public static class AuthCert
{
    private const string DllPath = "Libs/gcast_protocol_certificates.dll";

    [DllImport(DllPath)]
    private static extern IntPtr GetDevicePeerCertificate(string ip);

    public static string? GetCert(string ip)
    {
        var cert = GetDevicePeerCertificate(ip);
        
        return Marshal.PtrToStringAnsi(cert);
    }
}