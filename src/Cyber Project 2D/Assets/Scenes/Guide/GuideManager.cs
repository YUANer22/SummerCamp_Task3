using DG.Tweening;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public GameObject bed;
    public GameObject guideBed;

    public GameObject playerDoor;
    public GameObject guidePlayerDoor;

    public GameObject toUpCorridorDoor;
    public GameObject guideToUpCorridorDoor;

    public GameObject toDownCorridorDoor;
    public GameObject guideToDownCorridorDoor;

    public float fadeTime = 0.5f;
    public float duration = 8f;
    public CanvasGroup horizontalCanvasGroup;
    bool isHorizontalMove = true;
    public CanvasGroup mouseCanvasGroup;
    bool isMouse = true;
    public CanvasGroup bagCanvasGroup;
    bool isBag = true;
    int state = 0;
    public static bool GuideMode= true;

    #region 流程控制
    public void WakeUp()
    {      
        if (state == 0)
        {
            //苏醒
            NarratorSystem.Instance.SendActionInfo("这里就是精神病院吗？看来，得靠自己逃出去了。");
            NarratorSystem.Instance.SendActionInfo("证据和判决都很反常，得去想办法多了解一些信息");
            NarratorSystem.Instance.SendActionInfo("你感觉好渴，好想喝水。");
            NarratorSystem.Instance.SendActionInfo("房间里有杯子，去找找看吧。");//左右移动按键提示、空格互动按键提示
            NarratorSystem.Instance.ShowInfo(2);
            state++;
        }

    }

    public void GetACup()
    {
        if (state == 1)
        {
            //拿到杯子
            NarratorSystem.Instance.SendActionInfo("你捡起了一个空杯子。");
            NarratorSystem.Instance.SendActionInfo("也许房间外有水，去外面看看吧。");
            NarratorSystem.Instance.ShowInfo(2);

            //解除
            playerDoor.SetActive(true);
            guidePlayerDoor.SetActive(false);
            state++;
        }
        
    }

    public void GoToTheCorridor()
    {
        if (state == 2)
        {
            //去到走廊
            NarratorSystem.Instance.ShowInfo(2);
            state++;
        }
        
    }

    public void GetWater()
    {
        if (state == 3)
        {
            //拿到水
            /*NarratorSystem.Instance.SendActionInfo("你等不及要喝水了。");*///背包打开按键提示
            state++;
        }
        
    }

    public void DrinkWater()
    {
        if (state == 4)
        {
            //喝水后
            NarratorSystem.Instance.SendActionInfo("你感觉好多了，去转转吧。");
            NarratorSystem.Instance.ShowInfo(2);

            //解除
            toUpCorridorDoor.SetActive(true);
            guideToUpCorridorDoor.SetActive(false);
            state++;
        }
        
    }

    public void GoToTheSecondFloor()
    {
        if (state == 5)
        {
            //来到二楼
            NarratorSystem.Instance.SendActionInfo("护士姐姐向你热情地打招呼，她似乎有什么想跟你说。");
            NarratorSystem.Instance.ShowInfo(2);
            state++;
        }
        
    }

    public void TalkToTheNurse()
    {
        if (state == 6)
        {
            //触发对话后
            NarratorSystem.Instance.SendDialogueInfo("你感觉好累，回去睡觉吧。");

            //解除
            toDownCorridorDoor.SetActive(true);
            guideToDownCorridorDoor.SetActive(false);
            bed.SetActive(true);
            guideBed.SetActive(false);
            state++;
        }       
    }

    #endregion
    public void LoadScene()
    {
        MMSceneLoadingManager.LoadScene("InventoryTest");
    }

    public void HorizontalMoveFadeIn()
    {
        if (isHorizontalMove)
        {
            horizontalCanvasGroup.alpha = 0;
            horizontalCanvasGroup.DOFade(1, fadeTime);
            Invoke("HorizontalMoveFadeOut", duration);
            isHorizontalMove = false;
        }      
    }
    public void HorizontalMoveFadeOut()
    {
        horizontalCanvasGroup.DOFade(0, fadeTime+5);
    }

    public void MouseFadeIn()
    {
        if (isMouse)
        {
            mouseCanvasGroup.alpha = 0;
            mouseCanvasGroup.DOFade(1, fadeTime);
            Invoke("MouseFadeOut", duration);
            isMouse = false;
            HorizontalMoveFadeOut();
        }
        
    }
    public void MouseFadeOut()
    {
        mouseCanvasGroup.DOFade(0, fadeTime);
    }

    public void BagFadeIn()
    {
        if (isBag)
        {
            bagCanvasGroup.alpha = 0;
            bagCanvasGroup.DOFade(1, fadeTime);
            Invoke("BagFadeOut", duration);
            isBag = false;
            MouseFadeOut();
        }        
    }

    public void BagFadeOut()
    {
        bagCanvasGroup.DOFade(0, fadeTime);
    }

    #region 单例模式
    private static GuideManager instance;
    public static GuideManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GuideManager>();
            }
            return instance;
        }
    }
    #endregion
}
