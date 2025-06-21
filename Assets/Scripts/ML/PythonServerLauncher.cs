/*using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PythonServerLauncher : MonoBehaviour
{
    private Process serverProcess;

    void Start()
    {
        StartPythonServer();
    }

    void StartPythonServer()
    {
        string pythonPath = @"C:\Yulia\ML\.venv\Scripts\python.exe";
        string scriptPath = @"C:\Yulia\ML\server.py";

        ProcessStartInfo startInfo = new()
        {
            FileName = pythonPath,
            Arguments = $"\"{scriptPath}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };

        serverProcess = new Process { StartInfo = startInfo };

        serverProcess.OutputDataReceived += (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                Debug.Log("📤 PY OUT: " + e.Data);
        };

        serverProcess.ErrorDataReceived += (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                Debug.LogError("❌ PY ERR: " + e.Data);
        };

        serverProcess.Start();
        serverProcess.BeginOutputReadLine();
        serverProcess.BeginErrorReadLine();

        Debug.Log("🚀 Python-сервер запущено!");
    }

    void OnApplicationQuit()
    {
        if (serverProcess != null && !serverProcess.HasExited)
        {
            serverProcess.Kill();
            serverProcess.Dispose();
        }
    }
}
*/