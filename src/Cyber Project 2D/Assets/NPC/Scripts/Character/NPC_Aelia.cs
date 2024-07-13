using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Aelia : NPC_Base
{
    public NPC_Aelia() : base()
    {
        npcname = "Aelia";
    }

    public override void UpdateQueue()
    {
        // 测试Demo
        //if (Favorability >= 50 && !thresholdsTriggered[1])
        //    Enqueue(1);
        if (Favorability > 70 && !thresholdsTriggered[0])
            Enqueue(0);
    }

    public override void StartDialog()
    {
        if (Favorability >= 70)
            CGManager.Instance.LoadNewScene(3);
        else
            base.StartDialog();
    }

    public void Buff(string itemName)
    {
        NarratorSystem.Instance.SendDialogueInfo("艾莉现在并不渴。");
        NarratorSystem.Instance.ShowInfo(3);
    }

    #region 单例模式
    private static NPC_Aelia instance;
    public static NPC_Aelia Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Aelia>();
            }
            return instance;
        }
    }
    #endregion
}
