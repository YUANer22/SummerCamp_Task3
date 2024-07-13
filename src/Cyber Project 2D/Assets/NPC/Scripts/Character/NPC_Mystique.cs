using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Mystique : NPC_Base
{

    public NPC_Mystique() : base()
    {
        npcname = "Mystique";
    }
    public override void UpdateQueue()
    {
        if (Favorability >= 50 && !thresholdsTriggered[0])
            Enqueue(0);
    }
    public void Buff(string itemName)
    {
        if (itemName == "Sedative")
        {
            NarratorSystem.Instance.ShowInfo("你对迷梦使用了镇静剂，她的心情变得很好");
            isGoodCondition = true;
            return;
        }
        NarratorSystem.Instance.ShowInfo("迷梦并不想喝饮料。");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Screw")
        {
            Favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("迷梦拿到了螺丝钉，好感度增加了10");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("迷梦并不想要这个。");
        }
    }
    public void GiveItem()
    {
        foreach(GameObject item in ItemSystem.Instance.uniqueItems)
        {
            if (item.name == "Scanner")
            {
                GameObject.Find("MainInventory").GetComponent<Inventory>().AddItem(item.GetComponent<InventoryItem>(), 1);
                NarratorSystem.Instance.ShowInfo("迷梦给了你一个扫描仪");
                return;
            }
        }
    }
    #region 单例模式
    private static NPC_Mystique instance;
    public static NPC_Mystique Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Mystique>();
            }
            return instance;
        }
    }
    #endregion
}
