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
            NarratorSystem.Instance.ShowInfo("�������ʹ�����򾲼������������úܺ�");
            isGoodCondition = true;
            return;
        }
        NarratorSystem.Instance.ShowInfo("���β���������ϡ�");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Screw")
        {
            Favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("�����õ�����˿�����øж�������10");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("���β�����Ҫ�����");
        }
    }
    public void GiveItem()
    {
        foreach(GameObject item in ItemSystem.Instance.uniqueItems)
        {
            if (item.name == "Scanner")
            {
                GameObject.Find("MainInventory").GetComponent<Inventory>().AddItem(item.GetComponent<InventoryItem>(), 1);
                NarratorSystem.Instance.ShowInfo("���θ�����һ��ɨ����");
                return;
            }
        }
    }
    #region ����ģʽ
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
