using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "ProofOfIllicitMoneyConfig", menuName = "MoreMountains/Items/ProofOfIllicitMoney", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_ProofOfIllicitMoney : InventoryItem
    {
        [Header("testItem")]
        [MMInformation("�����", MMInformationAttribute.InformationType.Info, false)]

        /// the amount of health to add to the player when the item is used
        [Tooltip("����")]
        public float HealthBonus;
        /// <summary>
        /// When the item is used, we try to grab our character's Health component, and if it exists, we add our health bonus amount of health
        /// </summary>
        /// 
        public override bool Use(string playerID)
        {
            base.Use(playerID);

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "Sephira")
            {
                NPC_Sephira.Instance.ItemJudge("ProofOfIllicitMoney");
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Player")
            {
                NarratorSystem.Instance.ShowInfo("Ҳ������վ���ʲô��");
                return false;
            }
            else if(ItemSystem.Instance.WhoIsOpeningInventory()== "Vidora")
            {
                NarratorSystem.Instance.ShowInfo("��ҽ����������Ծ�֮ǰ����վݲ���������");
                return false;
            }
            else
            {
                NarratorSystem.Instance.ShowInfo("����˲�����Ҫ�߿��վݡ�");
                return false;
            }
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.ShowInfo("��������߿��վݡ�");
            return true;
        }
    }
}