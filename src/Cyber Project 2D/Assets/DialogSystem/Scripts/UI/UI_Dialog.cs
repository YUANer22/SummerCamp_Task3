using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;


public class UI_Dialog : MonoBehaviour
{
    public static UI_Dialog Instance;
    private Image head;
    private Text nameText;
    private Text mainText;
    private RectTransform content;
    private Transform Options;
    private Transform Main;
    private GameObject prefab_OptionItem;
    public GameObject input;
    public GameObject items;
    private Image BG;

    private DialogConf currconf;
    private int currindex;
    public NPC_Base Currnpc { set; get; }

    private UI_Click ui_Click;
    public DialogConf aiend;
    public Character player;
    public string nowstr;
    private void OnEnable()
    {
        if (player)
        {
            player.Freeze();
            player.MovementState.ChangeState(CharacterStates.MovementStates.Idle);
        }
    }

    private void OnDisable()
    {
        if (player)
            player.UnFreeze();
    }

    private void Awake()
    {
        Instance = this;
        var playergb = GameObject.FindWithTag("Player");
        player = playergb ? playergb.GetComponent<Character>() : null;
        BG = transform.Find("Image").GetComponent<Image>();
        head = transform.Find("Main/Head").GetComponent<Image>();
        nameText = transform.Find("Main/Name").GetComponent<Text>();
        ui_Click = transform.Find("Main/Scroll View").GetComponent<UI_Click>();
        input = transform.Find("Main/Input").gameObject;
        //mainText = transform.Find("Main/MainText").GetComponent<Text>();
        mainText = transform.Find("Main/Scroll View/Viewport/Content/MainText").GetComponent<Text>();
        content = transform.Find("Main/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        Options = transform.Find("Options");
        Main = transform.Find("Main");
        prefab_OptionItem = Resources.Load<GameObject>("Options_Item");
        aiend = Resources.Load<DialogConf>("AIEnd");
    }

    public void SaySth(string txt)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(DoMainTextEF(txt));
    }
    public void InitDialog(DialogConf conf)
    {
        input.SetActive(false);
        currconf = conf;
        currindex = 0;
        StartDialog(currconf, currindex);
    }
    public Coroutine coroutine;
    private void StartDialog(DialogConf conf, int index)
    {
        DialogModel model = conf.dialogs[index];

        // 修改图像和名字
        head.sprite = model.NPCConf.Head;
        nameText.text = model.NPCConf.Name;
        mainText.text = "";
        // 删除已有的玩家选项
        Transform[] items = Options.GetComponentsInChildren<Transform>();
        for (int i = 1; i < items.Length; i++)
        {
            Destroy(items[i].gameObject);
        }
        // 说话
        if (coroutine != null)
            StopCoroutine(coroutine);
        nowstr = model.NPCContent;
        coroutine = StartCoroutine(DoMainTextEF(model.NPCContent));

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
                ui_Click.end = true;
                if (args.Length > 0)
                {
                    if (args == "Vidora")
                        NPC_Vidora.Instance.getmessage = true;
                    else if (args == "Sephira")
                        NPC_Sephira.Instance.getmessage = true;
                    else if(args == "SafeBox")
                        SafeController.getKey = true;
                    else
                    {
                        items.transform.Find(args).gameObject.SetActive(true);
                    }
                }
                break;
            case DialogEventEnum.JumpDialog:
                JumpDialogEvent(int.Parse(args));
                break;
            case DialogEventEnum.AIDialog:
                AIDialogEvent();
                break;
            case DialogEventEnum.UpdateScore:
                UpdateScore(int.Parse(args));
                break;
            case DialogEventEnum.ChangeImage:
                StartCoroutine(ChangeImageEvent(Resources.Load<Sprite>("CGs/" + args), 0.5f));
                break;
        }
    }

    private IEnumerator ChangeImageEvent(Sprite newSprite, float fadeDuration)
    {
        Options.gameObject.SetActive(false);
        Main.gameObject.SetActive(false);
        // 渐变到黑色
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            Color newColor = Color.Lerp(Color.white, Color.black, t);
            BG.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 设置新图片
        BG.sprite = newSprite;
        Options.gameObject.SetActive(true);
        Main.gameObject.SetActive(true);
        
        // 渐变到初始颜色
        elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            Color newColor = Color.Lerp(Color.black, Color.white, t);
            BG.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public void UpdateScore(int num)
    {
        Debug.Log(nameText.text + num.ToString());
        Currnpc.Favorability += num;
    }
    private void AIDialogEvent()
    {
        if (NewDaySystem.Instance.AItimes > 0)
        {
            input.SetActive(true);
            ui_Click.enabled = false;
        }
        else
        {
            StopCoroutine(nameof(DoMainTextEF));
            InitDialog(aiend);
        }

    }
    private void NextDialogEvent()
    {
        currindex += 1;
        StartDialog(currconf, currindex);
    }
    public static DialogEndEvent dialogEnd = new DialogEndEvent();
    public class DialogEndEvent: UnityEvent { }
    public void ExitDialogEvent()
    {
        ui_Click.enabled = false;
        gameObject.SetActive(false);
        DialogueManager.Instance.ChangeInput(true);
        if (NarratorSystem.Instance)
            NarratorSystem.Instance.ShowInfo(3);
        dialogEnd.Invoke();
    }

    private void JumpDialogEvent(int index)
    {
        currindex = index;
        StartDialog(currconf, currindex);
    }
    public bool running = false;
    IEnumerator DoMainTextEF(string txt)
    {
        // 字符数量决定了 content的高 每23个字符增加25的高
        float addHeight = txt.Length / 23 + 1;
        running = true;
        content.sizeDelta = new Vector2(content.sizeDelta.x, addHeight*25);

        string currStr ="";
        for (int i = 0; i < txt.Length; i++)
        {
            currStr += txt[i];
            yield return new WaitForSeconds(0.03f);
            mainText.text = currStr;
        }
        running = false;
    }
    public void StopEffect()
    {
        running = false;
        StopCoroutine(coroutine);
        mainText.text = nowstr;
    }
}
