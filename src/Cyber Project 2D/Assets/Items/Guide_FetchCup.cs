using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide_FetchCup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GuideManager.Instance.MouseFadeIn();
        }
    }
}
