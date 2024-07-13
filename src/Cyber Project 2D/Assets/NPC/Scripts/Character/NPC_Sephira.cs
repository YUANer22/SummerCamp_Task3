using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Sephira : NPC_Base
{
    public DialogConf aiconf;
    public bool getmessage;
    public NPC_Sephira() : base()
    {
        npcname = "Sephira";
    }

    public override void StartDialog()
    {
        if (Favorability > 90)
            CGManager.Instance.LoadNewScene(0);
        else
            base.StartDialog();
    }
    public override void UpdateQueue()
    {
        if (getmessage && !thresholdsTriggered[0])
        {
            Enqueue(0);
            defaultConf = aiconf;
        }
    }
    public void Buff(string itemName)
    {
        NarratorSystem.Instance.ShowInfo("塞弗拉并不渴。");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Money")
        {
            Favorability += 20;
            NarratorSystem.Instance.SendDialogueInfo("塞弗拉被贿赂了，好感度增加20");
        }
        else if(itemName=="ProofOfIllicitMoney")
        {
            Favorability = 100;
            NarratorSystem.Instance.SendDialogueInfo("塞弗拉得到了医生贪污的证据，好感度达到100");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("塞弗拉并不需要这个");
        }
    }
    #region 单例模式
    private static NPC_Sephira instance;
    public static NPC_Sephira Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Sephira>();
            }
            return instance;
        }
    }
    #endregion
}
