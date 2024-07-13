using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "ScannerConfig", menuName = "MoreMountains/Items/Scanner", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Scanner : InventoryItem
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

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "DestroyedRobot")
            {
                if (NPC_Sephira.Instance.Favorability>90 &&  NPC_Aelia.Instance.Favorability >= 70 && NPC_Evelyn.Instance.Favorability >= 70 && NPC_Mystique.Instance.Favorability >= 70 && NPC_Sherylina.Instance.Favorability >= 70 && NPC_Vidora.Instance.Favorability >= 70)
                {
                    CGManager.Instance.LoadNewScene(4);
                }
                // NarratorSystem.Instance.ShowInfo("解锁了特殊结局。");
                return true;
            }
            else
            {
                NarratorSystem.Instance.ShowInfo("扫描了一下，没什么反应。");
                return false;
            }
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.ShowInfo("你获得了扫描仪。");
            return true;
        }
    }
}