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
            NarratorSystem.Instance.SendDialogueInfo("艾芙琳喝了咖啡，好感度增加了10");
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Sedative")
        {
            NarratorSystem.Instance.BroadcastMessage("你对艾芙琳使用了镇静剂，她的心情变得很好");
            isGoodCondition = true;
        }
        else
        { 
            Favorability -= 10;
            NarratorSystem.Instance.SendDialogueInfo("艾芙琳觉得这个并不好喝，好感度减少了10");
        }
    }

    #region 单例模式
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
