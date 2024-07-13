using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "�Ի�����", menuName = "�Ի�����/�����Ի�����")]
public class DialogConf : ScriptableObject
{
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<DialogModel> dialogs;
}
