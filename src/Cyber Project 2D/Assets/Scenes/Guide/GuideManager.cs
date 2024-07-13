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

    #region ���̿���
    public void WakeUp()
    {      
        if (state == 0)
        {
            //����
            NarratorSystem.Instance.SendActionInfo("������Ǿ���Ժ�𣿿������ÿ��Լ��ӳ�ȥ�ˡ�");
            NarratorSystem.Instance.SendActionInfo("֤�ݺ��о����ܷ�������ȥ��취���˽�һЩ��Ϣ");
            NarratorSystem.Instance.SendActionInfo("��о��ÿʣ������ˮ��");
            NarratorSystem.Instance.SendActionInfo("�������б��ӣ�ȥ���ҿ��ɡ�");//�����ƶ�������ʾ���ո񻥶�������ʾ
            NarratorSystem.Instance.ShowInfo(2);
            state++;
        }

    }

    public void GetACup()
    {
        if (state == 1)
        {
            //�õ�����
            NarratorSystem.Instance.SendActionInfo("�������һ���ձ��ӡ�");
            NarratorSystem.Instance.SendActionInfo("Ҳ��������ˮ��ȥ���濴���ɡ�");
            NarratorSystem.Instance.ShowInfo(2);

            //���
            playerDoor.SetActive(true);
            guidePlayerDoor.SetActive(false);
            state++;
        }
        
    }

    public void GoToTheCorridor()
    {
        if (state == 2)
        {
            //ȥ������
            NarratorSystem.Instance.ShowInfo(2);
            state++;
        }
        
    }

    public void GetWater()
    {
        if (state == 3)
        {
            //�õ�ˮ
            /*NarratorSystem.Instance.SendActionInfo("��Ȳ���Ҫ��ˮ�ˡ�");*///�����򿪰�����ʾ
            state++;
        }
        
    }

    public void DrinkWater()
    {
        if (state == 4)
        {
            //��ˮ��
            NarratorSystem.Instance.SendActionInfo("��о��ö��ˣ�ȥתת�ɡ�");
            NarratorSystem.Instance.ShowInfo(2);

            //���
            toUpCorridorDoor.SetActive(true);
            guideToUpCorridorDoor.SetActive(false);
            state++;
        }
        
    }

    public void GoToTheSecondFloor()
    {
        if (state == 5)
        {
            //������¥
            NarratorSystem.Instance.SendActionInfo("��ʿ�����������ش��к������ƺ���ʲô�����˵��");
            NarratorSystem.Instance.ShowInfo(2);
            state++;
        }
        
    }

    public void TalkToTheNurse()
    {
        if (state == 6)
        {
            //�����Ի���
            NarratorSystem.Instance.SendDialogueInfo("��о����ۣ���ȥ˯���ɡ�");

            //���
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

    #region ����ģʽ
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
