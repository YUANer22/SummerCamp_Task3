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
        //构造函数
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
        //开启线程
        ThreadRecvive();
    }
    private void Update()
    {
        if (ReceiveCallBack != null &&
            !string.IsNullOrEmpty(receiveData))
        {
            //调用处理函数去数据进行处理
            ReceiveCallBack(receiveData);
            //使用之后清空接受的数据
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
    /// 开始线程接收
    /// </summary>
    private void ThreadRecvive()
    {
        //开一个新线程接收UDP发送的数据
        RecviveThread = new Thread(() =>
        {
            //实例化一个IPEndPoint，任意IP和对应端口 端口自行修改
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 5768);
            UdpClient udpReceive = new UdpClient(endPoint);
            UDPData data = new UDPData(endPoint, udpReceive);
            //开启异步接收
            udpReceive.BeginReceive(CallBackRecvive, data);
        })
        {
            //设置为后台线程
            IsBackground = true
        };
        //开启线程
        RecviveThread.Start();
    }

    /// <summary>
    /// 异步接收回调
    /// </summary>
    /// <param name="ar"></param>
    private void CallBackRecvive(IAsyncResult ar)
    {
        try
        {
            //将传过来的异步结果转为我们需要解析的类型
            UDPData state = ar.AsyncState as UDPData;
            IPEndPoint ipEndPoint = state.EndPoint;
            //结束异步接受 不结束会导致重复挂起线程卡死
            byte[] data = state.UDPClient.EndReceive(ar, ref ipEndPoint);
            //解析数据 编码自己调整暂定为默认 依客户端传过来的编码而定
            receiveData = Encoding.Default.GetString(data);
            // Debug.Log(receiveData);
            //数据的解析再Update里执行 Unity中Thread无法调用主线程的方法
            //再次开启异步接收数据
            state.UDPClient.BeginReceive(CallBackRecvive, state);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
    }
    /// <summary>
    /// 发送UDP信息
    /// </summary>
    /// <param name="remoteIP">发送地址</param>
    /// <param name="remotePort">发送端口</param>
    /// <param name="message">需要发送的信息</param>
    public void UDPSendMessage(string remoteIP, int remotePort, string message)
    {
        //将需要发送的内容转为byte数组 编码依接收端为主，自行修改
        byte[] sendbytes = Encoding.Unicode.GetBytes(message);
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();
        //发送数据到对应目标
        udpSend.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
        //关闭
        udpSend.Close();
    }
}