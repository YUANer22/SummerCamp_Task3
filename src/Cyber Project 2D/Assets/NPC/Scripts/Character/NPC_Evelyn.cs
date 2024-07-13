using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Evelyn : NPC_Base
{
    public NPC_Evelyn() : base()
    {
        npcname = "Evelyn";
    }

    public override void UpdateQueue()
    {
        if (Favorability >= 50 && !thresholdsTriggered[0])
            Enqueue(0);
        if (Favorability >= 70 && !thresholdsTriggered[1])
            Enqueue(1);
    }

    public void Buff(string itemName)
    {
        if (itemName == "Coffee")
        {
            Favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("��ܽ�պ��˿��ȣ��øж�������10");
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Sedative")
        {
            NarratorSystem.Instance.BroadcastMessage("��԰�ܽ��ʹ�����򾲼������������úܺ�");
            isGoodCondition = true;
        }
        else
        { 
            Favorability -= 10;
            NarratorSystem.Instance.SendDialogueInfo("��ܽ�վ�����������úȣ��øжȼ�����10");
        }
    }

    #region ����ģʽ
    private static NPC_Evelyn instance;
    public static NPC_Evelyn Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Evelyn>();
            }
            return instance;
        }
    }
    #endregion
}
