using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "对话配置", menuName = "对话配置/新增对话数据")]
public class DialogConf : ScriptableObject
{
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<DialogModel> dialogs;
}
