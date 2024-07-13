using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HealingTime : MonoBehaviour
{
    public PlayableDirector healingTime_In;
    public PlayableDirector healingTime_Out;
    static bool hasIn;
    static bool hasOut;
    public void StartHealingTime_In()
    {
        healingTime_In.Play();
        DaySystemManager.Instance.PartUI("÷Œ¡∆ ±º‰");
        hasIn = true;
        Debug.Log("5");
    }

    public void StartHealingTime_Out()
    {
        healingTime_Out.Play();
        hasOut = true;
        Debug.Log("6");
    }

    public static void ResetStates()
    {
        hasIn = false;
        hasOut = false;
    }

    private void Update()
    {
        if (!hasIn && DaySystemManager.Instance.transition && !DaySystemManager.Instance.isDayStart && !DaySystemManager.Instance.isFreeTime && DaySystemManager.Instance.isHealingTime && !DaySystemManager.Instance.isSleepingTime)
        {
            if (healingTime_In != null)
                StartHealingTime_In();
        }
        if (!hasOut && !DaySystemManager.Instance.transition && !DaySystemManager.Instance.isDayStart &&!DaySystemManager.Instance.isFreeTime && DaySystemManager.Instance.isHealingTime && !DaySystemManager.Instance.isSleepingTime)
        {
            if (healingTime_Out != null)
                StartHealingTime_Out();
            DaySystemManager.Instance.isHealingTime = false;
            DaySystemManager.Instance.transition = true;
        }
    }
}
