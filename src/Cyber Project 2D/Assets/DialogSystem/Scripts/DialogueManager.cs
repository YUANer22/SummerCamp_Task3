using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;
using MoreMountains.InventoryEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private DialogConf[] dialogConfs;
    private InventoryInputManager inventory;


    public GameObject canvas;
    
    private void Awake()
    {
        Instance = this;
        var InventoryCanvas = GameObject.Find("InventoryCanvas");
        inventory = InventoryCanvas ? InventoryCanvas.GetComponent<InventoryInputManager>() : null;
        dialogConfs = Resources.LoadAll<DialogConf>("Conf");
    }
    public void StartDialog(DialogConf conf)
    {
        canvas.SetActive(true);
        ChangeInput(false);
        UI_Dialog.Instance.InitDialog(conf);
    }
    public void ChangeInput(bool flag)
    {
        InputManager.Instance.InputDetectionActive = flag;
        if (inventory)
        if (flag)
        {
            inventory.ToggleInventoryKey = KeyCode.I;
        }
        else
        {
            inventory.ToggleInventoryKey = KeyCode.None;
        }
    }
    public DialogConf GetDialogConf(int index)
    {
        return dialogConfs[index];
    }
}
