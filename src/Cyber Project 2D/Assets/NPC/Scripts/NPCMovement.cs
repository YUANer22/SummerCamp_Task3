using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class NPCMovement : MonoBehaviour
{
    public float speed = 3f;
    public Transform leftPoint, rightPoint;
    private bool isMovingRight = true;
    private bool isTouchedByPlayer = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    Vector2 initialLeftPoint;
    Vector2 initialRightPoint;

    void Start()
    {
        initialLeftPoint = leftPoint.position;
        initialRightPoint = rightPoint.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isTouchedByPlayer)
        {
            anim.SetBool("isWalking", false);
            rb.velocity = Vector2.zero;
            return;
        }

        if (isMovingRight)
        {
            if (transform.position.x >= initialRightPoint.x)
            {
                isMovingRight = false;
                sr.flipX = true;
            }
            else
            {
                rb.velocity = new Vector2(speed, 0);
                anim.SetBool("isWalking", true);
            }
        }
        else
        {
            if (transform.position.x <= initialLeftPoint.x)
            {
                isMovingRight = true;
                sr.flipX = false;
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
                anim.SetBool("isWalking", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchedByPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchedByPlayer = false;
        }
    }
}
