cmake_minimum_required(VERSION 3.9)

project(gcast_protocol_certificates VERSION 0.1.0 DESCRIPTION "Unit conversion library")


set(CMAKE_CXX_STANDARD 17)

find_package(OpenSSL REQUIRED)

#add_executable(GCast_Protocol_Certificates device_certificate.cpp)
add_library(${PROJECT_NAME} SHARED device_certificate.cpp device_certificate.h)
target_link_libraries(${PROJECT_NAME} PRIVATE OpenSSL::SSL OpenSSL::Crypto)

set_target_properties(${PROJECT_NAME} PROPERTIES VERSION ${PROJECT_VERSION})
set_target_properties(${PROJECT_NAME} PROPERTIES SOVERSION ${PROJECT_VERSION_MAJOR})
set_target_properties(${PROJECT_NAME} PROPERTIES PUBLIC_HEADER device_certificate.h)