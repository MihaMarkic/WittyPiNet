#addin "Cake.FileHelpers"

var Project = Directory("./Righthand.WittyPI/");
var TestProject = Directory("./Righthand.WittyPI.Tests/");
var CakeDockerProj = Project + File("Righthand.WittyPI.csproj");
var CakeTestDockerProj = TestProject + File("Righthand.WittyPI.Test.csproj");
var CakeTestDockerAssembly = TestProject + Directory("bin/Release") + File("Righthand.WittyPI.Tests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var CakeDockerSln = File("./Righthand.WittyPI.sln");
var CakeDockerNuspec = File("./Righthand.WittyPI.nuspec");
var Nupkg = Directory("./nupkg");

var target = Argument("target", "Default");
var version = "";

Task("Default")
	.Does (() =>
	{
		NuGetRestore (CakeDockerSln);
		DotNetBuild (CakeDockerSln, c => {
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
	NuGetPack (CakeDockerNuspec, new NuGetPackSettings { 
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
