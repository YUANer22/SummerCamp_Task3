using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "MoneyConfig", menuName = "MoreMountains/Items/Money", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Money : InventoryItem
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
                NPC_Sephira.Instance.ItemJudge("Money");
                return true;
            }
            else if(ItemSystem.Instance.WhoIsOpeningInventory() == "Vidora")
            {
                NarratorSystem.Instance.ShowInfo("�������뻹�Ǳ��ҽ���������Ǯ�ȽϺá�");
                return false;
            }
            else
            {
                NarratorSystem.Instance.ShowInfo("����˶����Ǯ��������Ȥ��");
                return false;
            }
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.ShowInfo("������һ���Ǯ");
            return true;
        }
    }
}