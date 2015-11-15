#I @"packages/FAKE/tools"
#r "FakeLib.dll"

open Fake
open Fake.FileHelper
open System.IO

// ---------------------------------------------------------------------------- Variables

let binDir = "bin"

// ------------------------------------------------------------------------- Unity Helper

let UnityPath = 
    @"C:\Program Files\Unity\Editor\Unity.exe" 

let Unity projectPath args = 
    let result = Shell.Exec(UnityPath, "-quit -batchmode -logFile -projectPath \"" + projectPath + "\" " + args) 
    if result < 0 then failwithf "Unity exited with error %d" result 

// ------------------------------------------------------------------------------ Targets

Target "Clean" (fun _ -> 
    CleanDirs [binDir]
)

Target "Build" (fun _ ->
    Unity (Path.GetFullPath "src/UnityPackage") "-executeMethod PackageBuilder.BuildPackage"
    (!! "src/UnityPackage/*.unitypackage") |> Seq.iter (fun p -> MoveFile binDir p)
)

Target "Help" (fun _ ->  
    List.iter printfn [
      "usage:"
      "build [target]"
      ""
      " Targets for building:"
      " * Build        Build"
      ""]
)

// --------------------------------------------------------------------------- Dependency

// Build order
"Clean"
  ==> "Build"

RunTargetOrDefault "Help"
