using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacmen_movement : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector2 dest = Vector2.zero;
    private int move = 1;
    private int timer = 30;
    public Animator animatorController;

    private void Start()
    {
        dest = transform.position;
        animatorController.SetTrigger("left_enter");
    }

    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        demoMove();

        /*if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            dest = (Vector2)transform.position + Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            dest = (Vector2)transform.position + Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            dest = (Vector2)transform.position + Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dest = (Vector2)transform.position + Vector2.right;
        }*/
    }

    private void demoMove() {

        timer--;
        if (timer == 0)
        {
            timer = 30;
            move = -move;
            if (move == 1)
            {
                animatorController.SetTrigger("left_enter");
            }
            else
            {
                animatorController.SetTrigger("right_enter");
            }
        }

        if (move == 1)
        {
            dest = (Vector2)transform.position + Vector2.left;
        }
        else
        {
            dest = (Vector2)transform.position + Vector2.right;
        }
    }


}
