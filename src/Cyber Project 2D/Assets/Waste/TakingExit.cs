using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingExit : MonoBehaviour
{
    bool canGive = false;
    bool hasGiven =false;
    public MMF_Player feedback;
    private void Start()
    {
        feedback=GetComponent<MMF_Player>();
        canGive = false;
    }
    public void GiveItem()
    {
        if (canGive&&!hasGiven)
        {
            Pick();
            hasGiven = true;
        }
    }

    public void Activation()
    {
        canGive = true;
    }

    void Pick()
    {
        feedback.PlayFeedbacks();
    }
}
