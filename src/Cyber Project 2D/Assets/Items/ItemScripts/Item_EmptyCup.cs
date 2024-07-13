using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "EmptyCupConfig", menuName = "MoreMountains/Items/EmptyCup", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_EmptyCup : InventoryItem
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
            NarratorSystem.Instance.SendItemInfo("�����ÿձ��Ӹ�ʲô......");
            NarratorSystem.Instance.ShowInfo(1);
            return false;
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.SendInteractionInfo("�������һ���ձ��ӡ�");
            NarratorSystem.Instance.ShowInfo(4);
            return true;
        }
    }
}