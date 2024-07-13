using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Click : MonoBehaviour, IPointerClickHandler
{
    public bool end = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (end)
            {
                end = false;
                UI_Dialog.Instance.ExitDialogEvent();
            }
            else
            {
                if (UI_Dialog.Instance.running)
                {
                    UI_Dialog.Instance.StopEffect();
                }
                else 
                    UI_Dialog.Instance.ParseDialogEvent(DialogEventEnum.NextDialog, null);
            }
        }
    }
}
