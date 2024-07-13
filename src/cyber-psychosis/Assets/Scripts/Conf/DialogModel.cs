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
    [Required("û�����öԻ����ݣ�")]
    [VerticalGroup("NPC/NPCField"), HideLabel, Multiline(4)]
    public string NPCContent;

    [LabelText("NPC�¼�")]
    public List<DialogEventModel> events;

    [LabelText("���ѡ��")]
    public List<DialogPlayerSelect> selects;
    private void NPConValueChanged()
    {
        if (NPCConf == null || NPCConf.Head == null) NPCHead = null;
        else NPCHead = NPCConf.Head;
    }
}

public enum DialogEventEnum
{
    [LabelText("��һ���Ի�")]
    NextDialog,
    [LabelText("�˳��Ի�")]
    ExitDialog,
    [LabelText("��ת�Ի�")]
    JumpDialog,
    [LabelText("AI�Ի�")]
    AIDialog,
    [LabelText("��ĻЧ��")]
    ScreenEF
}
[Serializable]
public class DialogEventModel
{
    [EnumPaging]
    [HideLabel, HorizontalGroup("�¼�")]
    public DialogEventEnum DialogEvent;
    
    [HideLabel, HorizontalGroup("�¼�")] 
    public string Args;
}

[Serializable]
public class DialogPlayerSelect
{
    [LabelText("ѡ������"), MultiLineProperty(2), LabelWidth(50)]
    public string Content;
    [LabelText("�¼�")]
    public List<DialogEventModel> DialogEventModels;
}