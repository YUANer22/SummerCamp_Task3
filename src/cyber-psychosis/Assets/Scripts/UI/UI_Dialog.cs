using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UI_Dialog : MonoBehaviour
{
    public static UI_Dialog Instance;
    private Image head;
    private Text nameText;
    private Text mainText;
    private RectTransform content;
    private Transform Options;
    private GameObject prefab_OptionItem;
    private GameObject input;

    private DialogConf currconf;
    private int currindex;

    private UI_Click ui_Click;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        head = transform.Find("Main/Head").GetComponent<Image>();
        nameText = transform.Find("Main/Name").GetComponent<Text>();
        ui_Click = transform.Find("Main/Scroll View").GetComponent<UI_Click>();
        input = transform.Find("Main/Input").gameObject;
        //mainText = transform.Find("Main/MainText").GetComponent<Text>();
        mainText = transform.Find("Main/Scroll View/Viewport/Content/MainText").GetComponent<Text>();
        content = transform.Find("Main/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        Options = transform.Find("Options");
        prefab_OptionItem = Resources.Load<GameObject>("Options_Item");
        TestDialog();
    }

   
    public void SaySth(string txt)
    {
        StartCoroutine(DoMainTextEF(txt));
    }
    private void TestDialog()
    {
        // StartCoroutine(DoMainTextEF("这是一个测试文字~~~~~~~~~~~~~~~~"));
        currconf = GameManager.Instance.GetDialogConf(0);

        currindex = 0;

        StartDialog(currconf, currindex);
    }

    private void StartDialog(DialogConf conf, int index)
    {
        DialogModel model = conf.dialogs[index];
        
        // 修改图像和名字
        head.sprite = model.NPCConf.Head;
        nameText.text = model.NPCConf.Name;
        // 说话
        StartCoroutine(DoMainTextEF(model.NPCContent));

        // 删除已有的玩家选项
        Transform[] items = Options.GetComponentsInChildren<Transform>();
        for(int i = 1; i < items.Length; i++)
        {
            Destroy(items[i].gameObject);
        }

        if (model.selects.Count == 0)
        {
            ui_Click.enabled = true;
        }
        else
        {
            ui_Click.enabled = false;
            // 根据配置生成选项
            for (int i = 0; i < model.selects.Count; i++)
            {
                UI_Options_Item item = GameObject.Instantiate<GameObject>
                    (prefab_OptionItem, Options).GetComponent<UI_Options_Item>();
                item.Init(model.selects[i]);
            }
        }

        // NPC事件
        for (int i = 0; i < model.events.Count; i++)
        {
            ParseDialogEvent(model.events[i].DialogEvent, model.events[i].Args);
        }
    }

    public void ParseDialogEvent(DialogEventEnum dialogEvent, string args)
    {
        input.SetActive(false);
        switch (dialogEvent)
        {
            case DialogEventEnum.NextDialog:
                NextDialogEvent();
                break;
            case DialogEventEnum.ExitDialog:
                ExitDialogEvent();
                break;
            case DialogEventEnum.JumpDialog:
                JumpDialogEvent(int.Parse(args));
                break;
            case DialogEventEnum.AIDialog:
                AIDialogEvent();
                break;
            case DialogEventEnum.ScreenEF:
                GameManager.Instance.ScreenEF(float.Parse(args));
                break;
        }
    }

    private void AIDialogEvent()
    {
        input.SetActive(true);
    }
    private void NextDialogEvent()
    {
        currindex += 1;
        StartDialog(currconf, currindex);
    }

    private void ExitDialogEvent()
    {
        ui_Click.enabled = false;
        Debug.Log("对话结束");
    }

    private void JumpDialogEvent(int index)
    {
        currindex = index;
        StartDialog(currconf, currindex);
    }
    IEnumerator DoMainTextEF(string txt)
    {

        // 字符数量决定了 conteng的高 每23个字符增加25的高
        float addHeight = txt.Length / 23 + 1;
        content.sizeDelta = new Vector2(content.sizeDelta.x, addHeight*25);

        string currStr ="";
        for (int i = 0; i < txt.Length; i++)
        {
            currStr += txt[i];
            yield return new WaitForSeconds(0.08f);
            mainText.text = currStr;
            // 每满23个字，下移一个距离 25
            if (i>23*3&&i % 23 == 0)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.anchoredPosition.y+25);
            }
        }
    }
   
}
