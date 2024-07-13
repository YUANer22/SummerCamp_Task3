using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeArea : MonoBehaviour
{
    public static bool isEntered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isEntered = false;
        }
    }
}
