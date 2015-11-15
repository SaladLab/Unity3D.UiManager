using UnityEditor;

public static class PackageBuilder
{
    [MenuItem("Assets/Build UnityPackage")]
    public static void BuildPackage()
    {
        var assetPaths = new string[]
        {
            "Assets/Middleware/UiManager",
            "Assets/Middleware/UiManagerSample",
        };

        var packagePath = "UiManager.unitypackage";
        var options = ExportPackageOptions.Recurse;
        AssetDatabase.ExportPackage(assetPaths, packagePath, options);
    }
}
