using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "SedtiveConfig", menuName = "MoreMountains/Items/Sedative", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Sedative : InventoryItem
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
                NPC_Sherylina.Instance.Buff("Sedative");
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Evelyn")
            {
                NPC_Evelyn.Instance.Buff("Sedative");
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Mystique")
            {
                NPC_Mystique.Instance.Buff("Sedative");
                return true;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Sephira")
            {
                NarratorSystem.Instance.ShowInfo("�������뻹�Ǳ�Լ�����ʹ������ȽϺá�");
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Vidora")
            {
                NarratorSystem.Instance.ShowInfo("�������뻹�Ǳ��ҽ��ʹ������ȽϺá�");
                return false;
            }
            else if (ItemSystem.Instance.WhoIsOpeningInventory() == "Aelia")
            {
                NarratorSystem.Instance.ShowInfo("�������뻹�Ǳ����ʿʹ������ȽϺá�");
                return false;
            }

            //TODO:������NPCʹ���򾲼�ʱ�����

            NarratorSystem.Instance.SendItemInfo("����ʲô�أ�������Լ�ʹ���򾲼�����");
            NarratorSystem.Instance.ShowInfo(1);
            return false;
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.SendInteractionInfo("�������һ���򾲼���");
            NarratorSystem.Instance.ShowInfo(4);
            return true;
        }
    }
}