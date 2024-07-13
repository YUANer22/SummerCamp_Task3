using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "ScrewConfig", menuName = "MoreMountains/Items/Screw", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Screw : InventoryItem
    {
        [Header("testItem")]
        [MMInformation("测测试", MMInformationAttribute.InformationType.Info, false)]

        /// the amount of health to add to the player when the item is used
        [Tooltip("测试")]
        public float HealthBonus;
        /// <summary>
        /// When the item is used, we try to grab our character's Health component, and if it exists, we add our health bonus amount of health
        /// </summary>
        /// 
        public override bool Use(string playerID)
        {
            base.Use(playerID);

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "Mystique")
            {
                NPC_Mystique.Instance.ItemJudge("Screw");
                return true;
            }
            else if(ItemSystem.Instance.WhoIsOpeningInventory() == "Player")
            {
                NarratorSystem.Instance.ShowInfo("螺丝钉没什么用");
                return false;
            }
            else
            {
                NarratorSystem.Instance.ShowInfo("这个人并不需要螺丝钉。");
                return false;
            }
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.ShowInfo("你捡起了两个螺丝钉。");
            return true;
        }
    }
}