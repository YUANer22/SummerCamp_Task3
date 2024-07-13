using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogModel
{
    [OnValueChanged("NPConValueChanged")]
    [HideLabel]
    public NPCConf NPCConf;
    [HorizontalGroup("NPC", 75, LabelWidth = 50), ReadOnly, HideLabel, PreviewField(75)]
    public Sprite NPCHead;
    [Required("没有配置对话内容！")]
    [VerticalGroup("NPC/NPCField"), HideLabel, Multiline(4)]
    public string NPCContent;

    [LabelText("NPC事件")]
    public List<DialogEventModel> events;

    [LabelText("玩家选择")]
    public List<DialogPlayerSelect> selects;
    private void NPConValueChanged()
    {
        if (NPCConf == null || NPCConf.Head == null) NPCHead = null;
        else NPCHead = NPCConf.Head;
    }
}

public enum DialogEventEnum
{
    [LabelText("下一条对话")]
    NextDialog,
    [LabelText("退出对话")]
    ExitDialog,
    [LabelText("跳转对话")]
    JumpDialog,
    [LabelText("AI对话")]
    AIDialog,
    [LabelText("屏幕效果")]
    ScreenEF
}
[Serializable]
public class DialogEventModel
{
    [EnumPaging]
    [HideLabel, HorizontalGroup("事件")]
    public DialogEventEnum DialogEvent;
    
    [HideLabel, HorizontalGroup("事件")] 
    public string Args;
}

[Serializable]
public class DialogPlayerSelect
{
    [LabelText("选项文字"), MultiLineProperty(2), LabelWidth(50)]
    public string Content;
    [LabelText("事件")]
    public List<DialogEventModel> DialogEventModels;
}