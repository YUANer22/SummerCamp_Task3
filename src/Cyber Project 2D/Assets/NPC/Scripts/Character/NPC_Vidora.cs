using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Vidora : NPC_Base
{
    public DialogConf aiconf;
    public bool getmessage; 
    public NPC_Vidora() : base()
    {
        npcname = "Vidora";
    }
    public override void UpdateQueue()
    {
        if (getmessage && !thresholdsTriggered[0])
        {
            Enqueue(0);
            defaultConf = aiconf;
        }
        if (Favorability >= 70 && !thresholdsTriggered[1])
            Enqueue(1);
    }
    public void Buff(string itemName)
    {
        NarratorSystem.Instance.ShowInfo("Dr.ޱ���������ʡ�");
    }

    #region ����ģʽ
    private static NPC_Vidora instance;
    public static NPC_Vidora Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Vidora>();
            }
            return instance;
        }
    }
    #endregion
}
