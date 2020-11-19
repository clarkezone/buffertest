var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

Task("Clean")
    .WithCriteria(c=>HasArgument("rebuild"))
    .Does(()=> {

        CleanDirectory($"./src/frontendservice/bin/{configuration}");
        CleanDirectory($"./src/testclient/bin/{configuration}");
    });

Task("Build")
    .IsDependentOn("Clean")
    .Does(()=>{
        DotNetCoreBuild("./src/buffertest.sln", new DotNetCoreBuildSettings{
            Configuration = configuration,
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(()=>{
        DotNetCoreTest("./src/buffertest.sln", new DotNetCoreTestSettings{
            Configuration = configuration,
            NoBuild = true,
        });

    });

RunTarget(target);