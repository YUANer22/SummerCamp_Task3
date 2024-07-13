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
    public List<DialogConf> thresholds; // 触发对话的阈值列表
    protected bool[] thresholdsTriggered; // 标记每个阈值是否已经被触发
    protected Queue<DialogConf> confs; // 需要触发的对话队列
    public DialogConf defaultConf; // 默认对话


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