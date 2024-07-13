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

        // ����UDPͨ�ŵ�Client
        udpClient = new UdpClient();
        // ����IP��ַ��˿ں�
        remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31415);

        UDPManager manager = GetComponent<UDPManager>();
        if (manager == null)
        {
            manager = gameObject.AddComponent<UDPManager>();
        }
        //���úý����ķ���
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
        //�������Ϳ��Լ���ν�����
        UnityEngine.Debug.Log(receiveData);
        PythonJsonData data = JsonUtility.FromJson<PythonJsonData>(receiveData);
        UI_Dialog.Instance.SaySth(data.content);
        UI_Dialog.Instance.UpdateScore(data.score);
        UI_Dialog.Instance.input.SetActive(true);
    }

    private void StartSubProcess()
    {
        // ����python���ļ���
        string fileName = "gpt_test.py";
        // ��ȡUnity��Ŀ������·��
        string currdir = System.Environment.CurrentDirectory;
        // ƴ��Python�ļ�������·��
        string pythonPath = currdir + "/" + "Python";
        string fullPath = pythonPath + "/" + fileName;
        // ���������в���
        
        string command = "/c python \"" + fullPath + "\"" + args;

        // ����ProcessStartInfo����
        startInfo = new ProcessStartInfo
        {
            // �趨ִ��cmd
            FileName = "cmd.exe",
            // ���ù���Ŀ¼
            WorkingDirectory = pythonPath,
            // �����������һ����command�ַ���
            Arguments = command,
            // ��ΪǶ��Unity�к�̨ʹ�ã��������ò���ʾ����
            CreateNoWindow = true,
            // ������Ҫ�趨Ϊfalse��ʹ��CreateProcess�������̣�
            UseShellExecute = false
        };

        // ����Process
        Process.Start(startInfo);
    }
    private void StartSubProcessFromExe()
    {
        // ��ȡUnity��Ŀ������·��
        string currdir = System.Environment.CurrentDirectory;
        // ƴ��exe������·��
        string pythonPath = currdir + "/" + "Python";

        // ����ProcessStartInfo����
        startInfo = new ProcessStartInfo
        {
            // �趨ִ�д����exe
            FileName = pythonPath + "/" + "gpt_test.exe",
            // ���ù���Ŀ¼
            WorkingDirectory = pythonPath,
            // ���ò���
            Arguments = args,
            // ��ΪǶ��Unity�к�̨ʹ�ã��������ò���ʾ����
            CreateNoWindow = true,
            // ������Ҫ�趨Ϊfalse��ʹ��CreateProcess�������̣�
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
                // ��ȡ���̵�����
                string processName = process_1.ProcessName;
                // ������������а���"python"������ֹ�ý���
                if (processName.ToLower().Contains(contain) && process_1.Id != Process.GetCurrentProcess().Id)
                {
                    process_1.Kill();
                }
            }
            catch (Exception ex)
            {
                // �����쳣
                print(ex);
            }
        }
    }
    public void OnApplicationQuit()
    {
        // ��Ӧ�ó����˳�ǰִ��һЩ����
        UnityEngine.Debug.Log("Ӧ�ó��򼴽��˳�����������Python����");
        // ��������Python����
        Kill_All_Python_Process();
        // �ص�UDP
        udpClient.Close();
    }
}