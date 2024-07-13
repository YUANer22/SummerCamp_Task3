using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC配置", menuName = "角色配置/新增角色")]
public class NPCConf : ScriptableObject
{
    [HorizontalGroup("NPC", 75, LabelWidth = 50), HideLabel, PreviewField(75)]
    public Sprite Head;
    [VerticalGroup("NPC/NPCField"), LabelText("名称")]
    public string Name;
}
