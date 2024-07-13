using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Base : MonoBehaviour
{
    public string npcname;
    private int favorability;
    public int Favorability
    {
        get => favorability;
        set
        {
            favorability = value;
            text.text = favorability.ToString();
        }
    }
    private Text text;
    
    public bool isGoodCondition;
    public List<DialogConf> thresholds; // �����Ի�����ֵ�б�
    protected bool[] thresholdsTriggered; // ���ÿ����ֵ�Ƿ��Ѿ�������
    protected Queue<DialogConf> confs; // ��Ҫ�����ĶԻ�����
    public DialogConf defaultConf; // Ĭ�϶Ի�


    public NPC_Base() : base()
    {
        confs = new Queue<DialogConf>();
        isGoodCondition = false;
    }

    public void Awake()
    {
        text = transform.Find("Canvas/Text").GetComponent<Text>();
        Favorability = 50;
    }
    public void Start()
    {
        thresholdsTriggered = new bool[thresholds.Count];
    }

    public virtual void UpdateQueue() { }

    protected void Enqueue(int i)
    {
        confs.Enqueue(thresholds[i]);
        thresholdsTriggered[i] = true;
    }

    public virtual void StartDialog()
    {
        UpdateQueue();
        if (confs.Count != 0)
            DialogueManager.Instance.StartDialog(confs.Dequeue());
        else
            DialogueManager.Instance.StartDialog(defaultConf);
    }
}