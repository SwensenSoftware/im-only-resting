// include Fake lib
#r @"tools\FAKE\tools\FakeLib.dll"
open Fake
open System
open System.IO

 
let RestorePackages() = 
    !+ "./**/packages.config"
    ++ ".nuget/packages.config" //solution-only packages
    |> Scan
    |> Seq.iter (RestorePackage (fun rpp -> {rpp with ToolPath = @".nuget\NuGet.exe"})) //we use NuGet's default NuGet.exe location

RestorePackages()
 
// Properties
let buildDir = @".\build\"
let stageDir = @"stage"
let testDir  = @".\test\"
let deployDir = @".\deploy\"
let packagesDir = @".\packages"
 
// tools
let nunitVersion = GetPackageVersion packagesDir "NUnit.Runners"
let nunitPath = sprintf @"./packages/NUnit.Runners.%s/tools/" nunitVersion
 
//// version info
let version = "1.1.1.x"  // or retrieve from CI server
 
// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; deployDir]
)
 
Target "BuildApp" (fun _ ->
    !! @"Ior\Ior.csproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "AppBuild-Output: "
)
 
Target "BuildTest" (fun _ ->
    !! @"**\Tests.*.csproj"
    |> MSBuildDebug testDir "Build"
    |> Log "TestBuild-Output: "
)
 
Target "Test" (fun _ ->
    !! (testDir + @"\NUnit.Test.*.dll") 
    |> NUnit (fun p ->
        {p with
            ToolPath = nunitPath;
            DisableShadowCopy = true;
            OutputFile = testDir + @"TestResults.xml" })
)

let buildFiles = 
    !+ (buildDir + @"*.dll")
    ++ (buildDir + @"im-only-resting.exe")
    ++ (buildDir + @"NLog.config")
    |> Scan

let nonBuildFiles = 
    !+ @"NOTICE"
    ++ @"LICENSE"
    |> Scan


Seq.append nonBuildFiles buildFiles
|> Seq.iter (fun fullFileName -> 
    //let fi = FileInfo(fullFileName)
    ignore <| ExecProcess (fun psi -> psi.FileName <- "cmd.exe"; psi.Arguments <- sprintf "/c copy \"%s\" stage" fullFileName) TimeSpan.MaxValue)

Target "Zip" (fun _ ->
    !! (stageDir + "\*.*")
    |> Seq.map (fun x -> stdout.WriteLine(x : string); x)
    |> Zip stageDir (stageDir + "im-only-resting-" + version + ".zip")
)
 
Target "Default" (fun _ ->
    trace ("Success building IOR version " + version)
)
 
// Dependencies
"Clean"
  ==> "BuildApp"
  //==> "BuildTest"
  //==> "Test"
  ==> "Zip"
  ==> "Default"
 
// start build
Run "Default"

//System.Environment.CurrentDirectory <- @"C:\Users\Stephen\Documents\Visual Studio 2012\Projects\im-only-resting";;