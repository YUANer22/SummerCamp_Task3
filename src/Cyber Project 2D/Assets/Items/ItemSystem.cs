using MoreMountains.CorgiEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour, MMEventListener<MMInventoryEvent>
{
    public bool AeliaOpenInventory = false;
    public bool EvelynOpenInventory = false;
    public bool SherylinaOpenInventory = false;
    public bool MystiqueOpenInventory = false;
    public bool VidoraOpenInventory = false;
    public bool SephiraOpenInventory = false;
    public bool PlayerOpenInventory = false;
    public bool DestroyedRobotOpenInventory = false;
    public List<GameObject> uniqueItems = new List<GameObject>();
    void OnEnable()
    {
        this.MMEventStartListening<MMInventoryEvent>();
    }

    void OnDisable()
    {
        this.MMEventStopListening<MMInventoryEvent>();
    }
    public void OnMMEvent(MMInventoryEvent inventoryEvent)
    {
        switch (inventoryEvent.InventoryEventType)
        {
            case MMInventoryEventType.InventoryCloses:
                AeliaOpenInventory = false;
                EvelynOpenInventory = false;
                SherylinaOpenInventory = false;
                MystiqueOpenInventory = false;
                VidoraOpenInventory = false;
                PlayerOpenInventory = true;
                SephiraOpenInventory = false;
                break;
        }
    }
    public void RespawnAllUniqueItems()
    {
        foreach (GameObject item in uniqueItems)
        {
            item.SetActive(true);
            item.GetComponent<InventoryPickableItem>().ResetQuantity();
        }
    }
    public void RespawnTargetItem(string itemName)
    {
        foreach (GameObject item in uniqueItems)
        {
            if (item.name == itemName)
            {
                item.GetComponent<InventoryPickableItem>().ResetQuantity();
                return;
            }
        }
    }

    public void TryOpenTheInventory(string name)
    {
        if (name == "Aelia")
        {
            AeliaOpenInventory = true;
            SephiraOpenInventory = false;
            EvelynOpenInventory = false;
            SherylinaOpenInventory = false;
            MystiqueOpenInventory = false;
            VidoraOpenInventory = false;
            PlayerOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "Evelyn")
        {
            EvelynOpenInventory = true;
            AeliaOpenInventory = false;
            SephiraOpenInventory = false;
            SherylinaOpenInventory = false;
            MystiqueOpenInventory = false;
            VidoraOpenInventory = false;
            PlayerOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "Sherylina")
        {
            SherylinaOpenInventory = true;
            AeliaOpenInventory = false;
            EvelynOpenInventory = false;
            SephiraOpenInventory = false;
            MystiqueOpenInventory = false;
            VidoraOpenInventory = false;
            PlayerOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "Mystique")
        {
            MystiqueOpenInventory = true;
            AeliaOpenInventory = false;
            EvelynOpenInventory = false;
            SherylinaOpenInventory = false;
            SephiraOpenInventory = false;
            VidoraOpenInventory = false;
            PlayerOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "Vidora")
        {
            VidoraOpenInventory = true;
            AeliaOpenInventory = false;
            EvelynOpenInventory = false;
            SherylinaOpenInventory = false;
            MystiqueOpenInventory = false;
            SephiraOpenInventory = false;
            PlayerOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "Sephira")
        {
            SephiraOpenInventory = true;
            AeliaOpenInventory = false;
            EvelynOpenInventory = false;
            SherylinaOpenInventory = false;
            MystiqueOpenInventory = false;
            VidoraOpenInventory = false;
            PlayerOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "Player")
        {
            PlayerOpenInventory = true;
            AeliaOpenInventory = false;
            EvelynOpenInventory = false;
            SherylinaOpenInventory = false;
            MystiqueOpenInventory = false;
            VidoraOpenInventory = false;
            SephiraOpenInventory = false;
            DestroyedRobotOpenInventory = false;
        }
        else if (name == "DestroyedRobot")
        {
            DestroyedRobotOpenInventory = true;
            AeliaOpenInventory = false;
            EvelynOpenInventory = false;
            SherylinaOpenInventory = false;
            MystiqueOpenInventory = false;
            VidoraOpenInventory = false;
            SephiraOpenInventory = false;
            PlayerOpenInventory = false;
        }

        //其他NPC打开背包的情况
    }
    public string WhoIsOpeningInventory()
    {
        if (AeliaOpenInventory)
        {
            return "Aelia";
        }
        else if (EvelynOpenInventory)
        {
            return "Evelyn";
        }
        else if (SherylinaOpenInventory)
        {
            return "Sherylina";
        }
        else if (MystiqueOpenInventory)
        {
            return "Mystique";
        }
        else if (VidoraOpenInventory)
        {
            return "Vidora";
        }
        else if (SephiraOpenInventory)
        {
            return "Sephira";
        }
        else if (PlayerOpenInventory)
        {
            return "Player";
        }
        else if (DestroyedRobotOpenInventory)
        {
            return "DestroyedRobot";
        }
        return "Nobody";
    }
    #region 单例模式
    private static ItemSystem instance;
    public static ItemSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ItemSystem>();
            }
            return instance;
        }
    }
    #endregion
}
