using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Sherylina : NPC_Base
{

    public NPC_Sherylina() : base()
    {
        npcname = "Sherylina";
    }
    public override void UpdateQueue()
    {
        if (Favorability >= 50 && !thresholdsTriggered[0])
            Enqueue(0);
        if (Favorability >= 70 && !thresholdsTriggered[1])
            Enqueue(1);
        if (Favorability >= 90 && !thresholdsTriggered[2])
            Enqueue(2);
    }

    public void Buff(string itemName)
    {
        if (itemName == "Water")
        {
            NarratorSystem.Instance.SendDialogueInfo("ѩ����һ��ˮ���øж�û�б仯");
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Cola")
        {
            NarratorSystem.Instance.SendDialogueInfo("ѩ���˿��֣��øж�������10");
            Favorability += 10;
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Coffee")
        {
            NarratorSystem.Instance.SendDialogueInfo("ѩ���˿��ȣ��øжȼ�����10");
            Favorability -= 10;
            NarratorSystem.Instance.ShowInfo(3);
        }else if(itemName== "Sedative")
        {
            NarratorSystem.Instance.BroadcastMessage("���ѩʹ�����򾲼������������úܺ�");
            isGoodCondition = true;
        }
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "BrokenCatDoll")
        {
            Favorability += 20;
            NarratorSystem.Instance.SendDialogueInfo("ѩ�õ��������èè��ż���øж�������20");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("ѩ������Ҫ�����");
        }
    }
    #region ����ģʽ
    private static NPC_Sherylina instance;
    public static NPC_Sherylina Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Sherylina>();
            }
            return instance;
        }
    }
    #endregion
}
