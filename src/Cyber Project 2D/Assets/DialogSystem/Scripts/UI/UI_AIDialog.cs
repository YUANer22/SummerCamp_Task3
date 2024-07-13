using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_AIDialog : MonoBehaviour
{
    public static UI_AIDialog Instance;
    public GameObject npcController;

    private ProcessStartInfo startInfo;
    private Process process;

    private UdpClient udpClient;
    private IPEndPoint remoteEP;

    public InputField input;
    public Text EP;

    public APISettings settings;
    private string args;
    void Awake()
    {
        Instance = this;
        UnityEngine.Debug.Log(System.Environment.CurrentDirectory);
        // input = transform.Find("DialogCanvas/Input/InputField").GetComponent<InputField>();
        args = " --base " + (PlayerPrefs.HasKey("api_base") ? PlayerPrefs.GetString("api_base") : settings.api_base) + " --key " + (PlayerPrefs.HasKey("api_key") ? PlayerPrefs.GetString("api_key") : settings.api_key);
        UnityEngine.Debug.Log(args);
        Kill_All_Python_Process();

        // 创建UDP通信的Client
        udpClient = new UdpClient();
        // 设置IP地址与端口号
        remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31415);

        UDPManager manager = GetComponent<UDPManager>();
        if (manager == null)
        {
            manager = gameObject.AddComponent<UDPManager>();
        }
        //设置好解析的方法
        manager.SetReceiveCallBack(ReceiveUDPMessage);

#if PYTEST
        StartSubProcess();
#else
        StartSubProcessFromExe();
#endif
    }

    [Serializable]
    public class PythonJsonData
    {
        public string content;
        public int score;
    }

    [Serializable]
    public class UnityJsonData
    {
        public string content;
        public string name;
        public int now_state;
    }
    public void Send()
    {
        if (input.text == "")
        {
            return;
        }
        if (NewDaySystem.Instance.AItimes > 0)
        {
            NewDaySystem.Instance.AItimes--;
        }
        else
        {
            UI_Dialog.Instance.InitDialog(UI_Dialog.Instance.aiend);
            return;
        }
        UnityEngine.Debug.Log(input.text);
        NPC_Base curr = UI_Dialog.Instance.Currnpc;
        UnityJsonData jsonData = new()
        {
            content = input.text,
            name = curr.npcname,
            now_state = curr.Favorability
        };
        UI_Dialog.Instance.SaySth("......");
        byte[] message = Encoding.UTF8.GetBytes(JsonUtility.ToJson(jsonData));
        udpClient.Send(message, message.Length, remoteEP);
        UI_Dialog.Instance.input.SetActive(false);
    }

    void ReceiveUDPMessage(string receiveData)
    {
        //接下来就看自己如何解析了
        UnityEngine.Debug.Log(receiveData);
        PythonJsonData data = JsonUtility.FromJson<PythonJsonData>(receiveData);
        UI_Dialog.Instance.SaySth(data.content);
        UI_Dialog.Instance.UpdateScore(data.score);
        UI_Dialog.Instance.input.SetActive(true);
    }

    private void StartSubProcess()
    {
        // 运行python的文件名
        string fileName = "gpt_test.py";
        // 获取Unity项目的数据路径
        string currdir = System.Environment.CurrentDirectory;
        // 拼接Python文件的完整路径
        string pythonPath = currdir + "/" + "Python";
        string fullPath = pythonPath + "/" + fileName;
        // 设置命令行参数
        
        string command = "/c python \"" + fullPath + "\"" + args;

        // 创建ProcessStartInfo对象
        startInfo = new ProcessStartInfo
        {
            // 设定执行cmd
            FileName = "cmd.exe",
            // 设置工作目录
            WorkingDirectory = pythonPath,
            // 输入参数是上一步的command字符串
            Arguments = command,
            // 因为嵌入Unity中后台使用，所以设置不显示窗口
            CreateNoWindow = true,
            // 这里需要设定为false（使用CreateProcess创建进程）
            UseShellExecute = false
        };

        // 创建Process
        Process.Start(startInfo);
    }
    private void StartSubProcessFromExe()
    {
        // 获取Unity项目的数据路径
        string currdir = System.Environment.CurrentDirectory;
        // 拼接exe的完整路径
        string pythonPath = currdir + "/" + "Python";

        // 创建ProcessStartInfo对象
        startInfo = new ProcessStartInfo
        {
            // 设定执行打包的exe
            FileName = pythonPath + "/" + "gpt_test.exe",
            // 设置工作目录
            WorkingDirectory = pythonPath,
            // 设置参数
            Arguments = args,
            // 因为嵌入Unity中后台使用，所以设置不显示窗口
            CreateNoWindow = true,
            // 这里需要设定为false（使用CreateProcess创建进程）
            UseShellExecute = false
        };

        Process.Start(startInfo);
    }
    void Kill_All_Python_Process()
    {
        Process[] allProcesses = Process.GetProcesses();
        foreach (Process process_1 in allProcesses)
        {
            try
            {
#if PYTEST
                string contain = "python";
#else
                string contain = "gpt_test";
#endif
                // 获取进程的名称
                string processName = process_1.ProcessName;
                // 如果进程名称中包含"python"，则终止该进程
                if (processName.ToLower().Contains(contain) && process_1.Id != Process.GetCurrentProcess().Id)
                {
                    process_1.Kill();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                print(ex);
            }
        }
    }
    public void OnApplicationQuit()
    {
        // 在应用程序退出前执行一些代码
        UnityEngine.Debug.Log("应用程序即将退出，清理所有Python进程");
        // 结束所有Python进程
        Kill_All_Python_Process();
        // 关掉UDP
        udpClient.Close();
    }
}