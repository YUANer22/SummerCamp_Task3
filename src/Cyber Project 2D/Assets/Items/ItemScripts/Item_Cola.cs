using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "ColaConfig", menuName = "MoreMountains/Items/Cola", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Cola : InventoryItem
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

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "Sherylina")
            {
                NPC_Sherylina.Instance.Buff("Cola");//Ч�������Ӻøж�
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Evelyn")
            {
                NPC_Evelyn.Instance.Buff("Cola");//Ч���Ǽ��ٺøж�
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Mystique")
            {
                NPC_Mystique.Instance.Buff("Cola");//���඼��Ч��
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Sephira")
            {
                NPC_Sephira.Instance.Buff("Cola");
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Vidora")
            {
                NPC_Vidora.Instance.Buff("Cola");
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Aelia")
            {
                NPC_Aelia.Instance.Buff("Cola");
                return false;
            }

            NarratorSystem.Instance.SendItemInfo("�����ڲ�����ȿ��֡�");
            NarratorSystem.Instance.ShowInfo(1);
            return false;
        }

        public override bool Pick(string playerID)
        {
            InventoryItem[] inventoryItems = GameObject.Find("MainInventory").GetComponent<Inventory>().Content;
            foreach (InventoryItem item in inventoryItems)
            {      
                if (item!=null&&item.ItemID == "EmptyCup")
                {
                    NarratorSystem.Instance.SendInteractionInfo("��װ��һ������");
                    NarratorSystem.Instance.ShowInfo(4);
                    GameObject.Find("MainInventory").GetComponent<Inventory>().RemoveItemByID(item.ItemID, 1);
                    GameObject.Find("GetWater").GetComponent<MMF_Player>().PlayFeedbacks();
                    return true;
                }
            }
            foreach (InventoryItem item in inventoryItems)
            {      
                if (item != null && item.ItemID == "Cola")
                {
                    NarratorSystem.Instance.SendInteractionInfo("��û�пձ��ӣ��޷��ӿ��֡�");
                    NarratorSystem.Instance.ShowInfo(4);
                    GameObject.Find("MainInventory").GetComponent<Inventory>().RemoveItemByID(item.ItemID,1);
                    return false;
                }
            }
            NarratorSystem.Instance.SendInteractionInfo("��û�пձ��ӣ��޷��ӿ��֡�");
            NarratorSystem.Instance.ShowInfo(4);
            return false;           
        }
    }
}