using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPManager : MonoBehaviour
{
    class UDPData
    {
        private readonly UdpClient udpClient;
        public UdpClient UDPClient
        {
            get { return udpClient; }
        }
        private readonly IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get { return endPoint; }
        }
        //���캯��
        public UDPData(IPEndPoint endPoint, UdpClient udpClient)
        {
            this.endPoint = endPoint;
            this.udpClient = udpClient;
        }
    }
    string receiveData = string.Empty;
    private Action<string> ReceiveCallBack = null;
    private Thread RecviveThread;
    private void Start()
    {
        //�����߳�
        ThreadRecvive();
    }
    private void Update()
    {
        if (ReceiveCallBack != null &&
            !string.IsNullOrEmpty(receiveData))
        {
            //���ô�����ȥ���ݽ��д���
            ReceiveCallBack(receiveData);
            //ʹ��֮����ս��ܵ�����
            receiveData = string.Empty;
        }
    }
    private void OnDestroy()
    {
        if (RecviveThread != null)
        {
            RecviveThread.Abort();
        }
    }
    public void SetReceiveCallBack(Action<string> action)
    {
        ReceiveCallBack = action;
    }
    /// <summary>
    /// ��ʼ�߳̽���
    /// </summary>
    private void ThreadRecvive()
    {
        //��һ�����߳̽���UDP���͵�����
        RecviveThread = new Thread(() =>
        {
            //ʵ����һ��IPEndPoint������IP�Ͷ�Ӧ�˿� �˿������޸�
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 5768);
            UdpClient udpReceive = new UdpClient(endPoint);
            UDPData data = new UDPData(endPoint, udpReceive);
            //�����첽����
            udpReceive.BeginReceive(CallBackRecvive, data);
        })
        {
            //����Ϊ��̨�߳�
            IsBackground = true
        };
        //�����߳�
        RecviveThread.Start();
    }

    /// <summary>
    /// �첽���ջص�
    /// </summary>
    /// <param name="ar"></param>
    private void CallBackRecvive(IAsyncResult ar)
    {
        try
        {
            //�����������첽���תΪ������Ҫ����������
            UDPData state = ar.AsyncState as UDPData;
            IPEndPoint ipEndPoint = state.EndPoint;
            //�����첽���� �������ᵼ���ظ������߳̿���
            byte[] data = state.UDPClient.EndReceive(ar, ref ipEndPoint);
            //�������� �����Լ������ݶ�ΪĬ�� ���ͻ��˴������ı������
            receiveData = Encoding.Default.GetString(data);
            // Debug.Log(receiveData);
            //���ݵĽ�����Update��ִ�� Unity��Thread�޷��������̵߳ķ���
            //�ٴο����첽��������
            state.UDPClient.BeginReceive(CallBackRecvive, state);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
    }
    /// <summary>
    /// ����UDP��Ϣ
    /// </summary>
    /// <param name="remoteIP">���͵�ַ</param>
    /// <param name="remotePort">���Ͷ˿�</param>
    /// <param name="message">��Ҫ���͵���Ϣ</param>
    public void UDPSendMessage(string remoteIP, int remotePort, string message)
    {
        //����Ҫ���͵�����תΪbyte���� ���������ն�Ϊ���������޸�
        byte[] sendbytes = Encoding.Unicode.GetBytes(message);
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();
        //�������ݵ���ӦĿ��
        udpSend.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
        //�ر�
        udpSend.Close();
    }
}