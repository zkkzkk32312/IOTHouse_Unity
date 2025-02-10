using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Profile;
using System;
using System.IO;

public class BuildScript
{
    [MenuItem("BuildScript/From BuildProfile/Web")]
    public static void PerformBuild()
    {
        var buildProfile = AssetDatabase.LoadAssetAtPath<BuildProfile>
            ("Assets/Settings/Build Profiles/Web.asset");
        BuildPlayerWithProfileOptions options = new BuildPlayerWithProfileOptions();

        // Get build number from Jenkins or fallback to date
        string buildNumber = Environment.GetEnvironmentVariable("BUILD_NUMBER") ?? DateTime.UtcNow.ToString("yyyyMMdd");
        string buildPath = Path.Combine("Builds", buildProfile.name, buildNumber);

        options.buildProfile = buildProfile;
        options.locationPathName = buildPath;
        options.options = BuildOptions.EnableHeadlessMode | BuildOptions.StrictMode;

        BuildPipeline.BuildPlayer(options);
    }
}
