using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Click : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UI_Dialog.Instance.ParseDialogEvent(DialogEventEnum.NextDialog, null);
        }
    }
}
