using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "TestItemConfig", menuName = "MoreMountains/Items/testItem", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class TestItem : InventoryItem
    {
        [Header("testItem")]
        [MMInformation("≤‚≤‚ ‘", MMInformationAttribute.InformationType.Info, false)]

        /// the amount of health to add to the player when the item is used
        [Tooltip("≤‚ ‘")]
        public float HealthBonus;
        /// <summary>
        /// When the item is used, we try to grab our character's Health component, and if it exists, we add our health bonus amount of health
        /// </summary>
        /// 
        public override bool Use(string playerID)
        {
            base.Use(playerID);

            if (JudgeArea.isEntered)
            {
                Debug.Log("use success");
                Show.use = true;
                return true;
            }else
            {
                Debug.Log("use false");
                return false;
            }
        }
    }
}