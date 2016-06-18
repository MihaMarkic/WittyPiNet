#addin "Cake.FileHelpers"

var Project = Directory("./Righthand.WittyPi/");
var TestProject = Directory("./Righthand.WittyPi.Tests/");
var WittyPiProj = Project + File("Righthand.WittyPi.csproj");
var WittyPiTestProj = TestProject + File("Righthand.WittyPi.Test.csproj");
var WittyPiTestAssembly = TestProject + Directory("bin/Release") + File("Righthand.WittyPi.Tests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var WittyPiSln = File("./Righthand.WittyPi.sln");
var WittyPiNuspec = File("./WittyPiNet.nuspec");
var Nupkg = Directory("./nupkg");

var target = Argument("target", "Default");
var version = "";

Task("Default")
	.Does (() =>
	{
		NuGetRestore (WittyPiSln);
		DotNetBuild (WittyPiSln, c => {
			c.Configuration = "Release";
			c.Verbosity = Verbosity.Minimal;
		});
});

Task("NuGetPack")
	.IsDependentOn("GetVersion")
	.IsDependentOn("Default")
	.Does (() =>
{
	CreateDirectory(Nupkg);
	NuGetPack (WittyPiNuspec, new NuGetPackSettings { 
		Version = version,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = Nupkg,
		BasePath = "./",
	});	
});

Task("GetVersion")
	.Does(() => {
		var assemblyInfo = ParseAssemblyInfo(AssemblyInfo);
		var semVersion = string.Join(".", assemblyInfo.AssemblyVersion.Split('.').Take(3));
		Information("Version {0}", semVersion);
		version = semVersion;
	});

RunTarget (target);
