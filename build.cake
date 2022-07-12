#addin nuget:?package=Cake.CMake&version=1.3.1
var target = Argument("target", "Default");

Task("Initialize")
    .Does(() => {
        var cmakeSettings = new CMakeSettings
        {
            OutputPath = "src/GCast.Protocol.Certificates/build",
            SourcePath = "src/GCast.Protocol.Certificates",
            Generator = "Visual Studio 17 2022",
            Options = new List<string>
            {
                "-DCMAKE_TOOLCHAIN_FILE=C:/vcpkg/scripts/buildsystems/vcpkg.cmake"
            }
        };

        CMake(cmakeSettings);
    });

Task("Build")
    .Does(() => {
        var cmakeSettings = new CMakeBuildSettings
        {
            BinaryPath = "src/GCast.Protocol.Certificates/build",
            Configuration = "Release",
            Targets = new [] { "ALL_BUILD" }
        };

        CMakeBuild(cmakeSettings);
        
        CreateDirectory("src/GCast.Protocol/Libs/");
        CopyFile("src/GCast.Protocol.Certificates/build/Release/gcast_protocol_certificates.dll", "src/GCast.Protocol/Libs/gcast_protocol_certificates.dll");
        CopyFile("src/GCast.Protocol.Certificates/build/Release/libcrypto-3-x64.dll", "src/GCast.Protocol/Libs/libcrypto-3-x64.dll");
        CopyFile("src/GCast.Protocol.Certificates/build/Release/libssl-3-x64.dll", "src/GCast.Protocol/Libs/libssl-3-x64.dll");
    });

Task("Default")
    .IsDependentOn("Initialize")
    .IsDependentOn("Build");

RunTarget(target);