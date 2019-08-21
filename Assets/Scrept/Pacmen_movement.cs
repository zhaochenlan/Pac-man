using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacmen_movement : MonoBehaviour
{
    //吃豆人的移动速度
    public float speed = 0.2f;
    //吃豆人下一次移动将要去的目的地
    private Vector2 dest = Vector2.zero;

    private void Start()
    {
        //保证吃豆人在游戏刚开始的时候不会动
        dest = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
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
        }
    }


}
