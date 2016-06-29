#addin "Cake.FileHelpers"
#addin "Cake.Putty"

var Project = Directory("./Righthand.WittyPi/");
var TestProject = Directory("./Righthand.WittyPi.Test/");
var WittyPiProj = Project + File("Righthand.WittyPi.csproj");
var WittyPiTestProj = TestProject + File("Righthand.WittyPi.Test.csproj");
var WittyPiTestAssembly = TestProject + Directory("bin/Release") + File("Righthand.WittyPi.Tests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var WittyPiSln = File("./Righthand.WittyPi.sln");
var WittyPiNuspec = File("./WittyPiNet.nuspec");
var Nupkg = Directory("./nupkg");
var NUnitConsoleDirectory = Directory("./packages/NUnit.ConsoleRunner.3.4.0/tools");
var deployFile = Directory("./") + File("./deploy.txt");

var target = Argument("target", "Default");
var config = Argument("config", "Release");
const string DeployArgumentName = "deploy";
var pi = Argument<string>(DeployArgumentName, null);
var version = "";

Task("Default")
	.Does (() =>
	{
		NuGetRestore (WittyPiSln);
		DotNetBuild (WittyPiSln, c => {
			c.Configuration = config;
			c.Verbosity = Verbosity.Minimal;
		});
});

Task("DeployTest")
	.IsDependentOn("Default")
	.Does(() => {
		string deployTarget;
		Information("Test");
		if (!HasArgument(DeployArgumentName))
		{
			if (FileExists(deployFile))
			{
				deployTarget = FileReadText(deployFile);
				Information("Got deploy target {0} from file", deployTarget);
			}
		}
		else
		{
			deployTarget = pi;
			Information("Got deploy target {0} from argument", deployTarget);
		}
		if (string.IsNullOrEmpty(deployTarget))
		{
			throw new Exception ("Couldn't find deploy target. Either set -deploy argument or deploy.txt file");
		}
		string testDirectory = TestProject + Directory("bin/" + config);
		Pscp(testDirectory + "/*", deployTarget);
		Pscp((string)NUnitConsoleDirectory + "/*", deployTarget);
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
