using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "CoffeeConfig", menuName = "MoreMountains/Items/Coffee", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Coffee : InventoryItem
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

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "Sherylina")
            {
                NPC_Sherylina.Instance.Buff("Coffee");//效果是减少好感度
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Evelyn")
            {
                NPC_Evelyn.Instance.Buff("Coffee");//效果是增加好感度
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Mystique")
            {
                NPC_Mystique.Instance.Buff("Coffee");//其余都无效果
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Sephira")
            {
                NPC_Sephira.Instance.Buff("Coffee");
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Vidora")
            {
                NPC_Vidora.Instance.Buff("Coffee");
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Aelia")
            {
                NPC_Aelia.Instance.Buff("Coffee");
                return false;
            }

            NarratorSystem.Instance.SendItemInfo("你现在并不想喝咖啡。");
            NarratorSystem.Instance.ShowInfo(1);
            return false;
        }

        public override bool Pick(string playerID)
        {
            InventoryItem[] inventoryItems = GameObject.Find("MainInventory").GetComponent<Inventory>().Content;
            foreach (InventoryItem item in inventoryItems)
            {
                if (item != null && item.ItemID == "EmptyCup")
                {
                    NarratorSystem.Instance.SendInteractionInfo("你装了一杯咖啡");
                    NarratorSystem.Instance.ShowInfo(4);
                    GameObject.Find("MainInventory").GetComponent<Inventory>().RemoveItemByID(item.ItemID, 1);
                    GameObject.Find("GetWater").GetComponent<MMF_Player>().PlayFeedbacks();
                    return true;
                }
            }
            foreach (InventoryItem item in inventoryItems)
            {
                if (item != null && item.ItemID == "Coffee")
                {
                    NarratorSystem.Instance.SendInteractionInfo("你没有空杯子，无法接咖啡。");
                    NarratorSystem.Instance.ShowInfo(4);
                    GameObject.Find("MainInventory").GetComponent<Inventory>().RemoveItemByID(item.ItemID, 1);
                    return false;
                }
            }
            NarratorSystem.Instance.SendInteractionInfo("你没有空杯子，无法接咖啡。");
            NarratorSystem.Instance.ShowInfo(4);
            return false;
        }
    }
}