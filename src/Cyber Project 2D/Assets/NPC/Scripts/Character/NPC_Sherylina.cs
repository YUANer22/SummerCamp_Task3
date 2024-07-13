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
            NarratorSystem.Instance.SendDialogueInfo("雪喝了一口水，好感度没有变化");
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Cola")
        {
            NarratorSystem.Instance.SendDialogueInfo("雪喝了可乐，好感度增加了10");
            Favorability += 10;
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Coffee")
        {
            NarratorSystem.Instance.SendDialogueInfo("雪喝了咖啡，好感度减少了10");
            Favorability -= 10;
            NarratorSystem.Instance.ShowInfo(3);
        }else if(itemName== "Sedative")
        {
            NarratorSystem.Instance.BroadcastMessage("你对雪使用了镇静剂，她的心情变得很好");
            isGoodCondition = true;
        }
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "BrokenCatDoll")
        {
            Favorability += 20;
            NarratorSystem.Instance.SendDialogueInfo("雪拿到了破损的猫猫玩偶，好感度增加了20");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("雪并不想要这个。");
        }
    }
    #region 单例模式
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
