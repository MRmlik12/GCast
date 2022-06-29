#addin nuget:?package=Cake.CMake&version=1.3.1
var target = Argument("target", "Default");

Task("Initialize")
    .Does(() => {
        var cmakeSettings = new CMakeSettings
        {
            OutputPath = "src/GCast.Protocol.Certificates/build",
            SourcePath = "src/GCast.Protocol.Certificates",
            Generator = "Visual Studio 17 2022"
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
    });

Task("Default")
    .IsDependentOn("Initialize")
    .IsDependentOn("Build");

RunTarget(target);