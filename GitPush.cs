using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Diagnostics;

// Need: OdinInspector
public class GitPush : OdinEditorWindow
{
    [MenuItem("Tools/GitPush")]
    private static void OpenWindow()
    {
        GetWindow<GitPush>().Show();
    }

    [Button(ButtonSizes.Large)]
    private void push()
    {
        string output;
        string formattedDate;
        string arg;

        UnityEngine.Debug.Log("Pushing...");
        exec("git", "add -A");
        formattedDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        arg = "commit -m " + '"' + formattedDate + '"';
        exec("git", arg);
        output = exec("git", "push");
        UnityEngine.Debug.Log("Push done!: " + output);
    }

    private string exec(string cmd, string arg)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = cmd,
                Arguments = arg,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string output2 = process.StandardError.ReadToEnd();
        output += output2;
        process.WaitForExit();
        return output;
    }
}
