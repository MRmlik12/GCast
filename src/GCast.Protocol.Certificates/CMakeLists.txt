cmake_minimum_required(VERSION 3.9)

project(gcast_protocol_certificates VERSION 0.1.0 DESCRIPTION "Device certification library")

set(CMAKE_CXX_STANDARD 17)

find_package(OpenSSL REQUIRED)

add_library(${PROJECT_NAME} SHARED device_certificate.cpp)
target_link_libraries(${PROJECT_NAME} PRIVATE OpenSSL::SSL OpenSSL::Crypto)

set_target_properties(${PROJECT_NAME} PROPERTIES VERSION ${PROJECT_VERSION})
set_target_properties(${PROJECT_NAME} PROPERTIES SOVERSION ${PROJECT_VERSION_MAJOR})
