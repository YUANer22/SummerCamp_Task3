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

    private ProcessStartInfo startInfo;
    private Process process;

    private UdpClient udpClient;
    private IPEndPoint remoteEP;

    private Text input;

    // Start is called before the first frame update
    void Start()
    {
        input = transform.Find("InputField/Text").GetComponent<Text>();
        UnityEngine.Debug.Log(transform.Find("InputField/Text"));

        Kill_All_Python_Process();

        // ����UDPͨ�ŵ�Client
        udpClient = new UdpClient();
        // ����IP��ַ��˿ں�
        remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31415);
        // ����python���ļ���
        string fileName = "gpt_test.py";
        // ��ȡUnity��Ŀ������·��
        string dataPath = Application.dataPath;
        // ƴ��Python�ļ�������·��
        string pythonPath = dataPath + "/" + "Python";
        string fullPath =  pythonPath + "/" + fileName;
        // ���������в���
        string command = "/c python \"" + fullPath + "\"";

        // ����ProcessStartInfo����
        startInfo = new ProcessStartInfo();
        // �趨ִ��cmd
        startInfo.FileName = "cmd.exe";
        // ���ù���Ŀ¼
        startInfo.WorkingDirectory = pythonPath;
        // �����������һ����command�ַ���
        startInfo.Arguments = command;
        // ��ΪǶ��Unity�к�̨ʹ�ã��������ò���ʾ����
        startInfo.CreateNoWindow = true;
        // ������Ҫ�趨Ϊfalse
        startInfo.UseShellExecute = false;
        // �����ض���������̵ı�׼�����������ֱ�ӱ�Unity C#���񣬴Ӷ�ʵ�� Python -> Unity ��ͨ��
        startInfo.RedirectStandardOutput = true;
        // �����ض���������̵ı�׼��������������Unity��C#�н���Debug Python���bug
        startInfo.RedirectStandardError = true;

        // ����Process
        process = new Process();
        process.StartInfo = startInfo;
        process.OutputDataReceived += new DataReceivedEventHandler(OnOutputDataReceived);
        process.ErrorDataReceived += new DataReceivedEventHandler(OnErrorDataReceived);

        //�����ű�Process�����Ҽ������ж�ȡ����뱨��
        process.Start();
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();

    }

    public void Send()
    {
        UnityEngine.Debug.Log(input.text);
        byte[] message = Encoding.UTF8.GetBytes(input.text);
        udpClient.Send(message, message.Length, remoteEP);
    }

    private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received!");
        if (!string.IsNullOrEmpty(e.Data))
        {
            string output = e.Data;
            UnityEngine.Debug.Log(output);
            if (output.Contains("[ChatGPT]"))
            {
                UI_Dialog.Instance.SaySth(output.Split(']')[1]);
            }
        }
    }

    private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            // �������
            UnityEngine.Debug.LogError("Received error output: " + e.Data);
        }
    }

    void Kill_All_Python_Process()
    {
        Process[] allProcesses = Process.GetProcesses();
        foreach (Process process_1 in allProcesses)
        {
            try
            {
                // ��ȡ���̵�����
                string processName = process_1.ProcessName;
                // ������������а���"python"������ֹ�ý���
                if (processName.ToLower().Contains("python") && process_1.Id != Process.GetCurrentProcess().Id)
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
    void OnApplicationQuit()
    {
        // ��Ӧ�ó����˳�ǰִ��һЩ����
        UnityEngine.Debug.Log("Ӧ�ó��򼴽��˳�����������Python����");
        // ��������Python����
        Kill_All_Python_Process();
    }
}