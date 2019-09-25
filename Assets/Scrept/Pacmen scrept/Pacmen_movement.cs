using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacmen_movement : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector2 dest = Vector2.zero;
    public Animator animatorController;
    enum Direction {
        stop,
        right,
        left,
        up,
        down
    }

    Direction myDirection = Direction.stop;
    bool onPress = false;

    private void Start()
    {
        dest = transform.position;
    }

    private void FixedUpdate()
    {
        AutoMove();
        CheckButtonState();
        if (onPress == false)
        {
            changeDirection();
        }
    }

    private void CheckButtonState() {
        if(!Input.GetKey(KeyCode.UpArrow)&&this.myDirection == Direction.up) {
            onPress = false;
        }
        if (!Input.GetKey(KeyCode.DownArrow) && this.myDirection == Direction.down)
        {
            onPress = false;
        }
        if (!Input.GetKey(KeyCode.LeftArrow) && this.myDirection == Direction.left)
        {
            onPress = false;
        }
        if (!Input.GetKey(KeyCode.RightArrow) && this.myDirection == Direction.right)
        {
            onPress = false;
        }
    }

    private void changeDirection() {
        if (Input.GetKey(KeyCode.UpArrow) && this.myDirection != Direction.up)
        {
            animatorController.SetTrigger("up_enter");
            this.myDirection = Direction.up;
            onPress = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && this.myDirection != Direction.down)
        {
            animatorController.SetTrigger("down_enter");
            this.myDirection = Direction.down;
            onPress = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && this.myDirection != Direction.left)
        {
            animatorController.SetTrigger("left_enter");
            this.myDirection = Direction.left;
            onPress = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && this.myDirection != Direction.right)
        {
            animatorController.SetTrigger("right_enter");
            this.myDirection = Direction.right;
            onPress = true;
        }
    }

    private void AutoMove() {

        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        if (myDirection == Direction.up && validMove(Vector2.up)) {
            dest = (Vector2)transform.position + Vector2.up;
        }
        if (myDirection == Direction.down && validMove(Vector2.down)) {
            dest = (Vector2)transform.position + Vector2.down;
        }
        if (myDirection == Direction.left && validMove(Vector2.left)) {
            dest = (Vector2)transform.position + Vector2.left;
        }
        if (myDirection == Direction.right && validMove(Vector2.right)) {
            dest = (Vector2)transform.position + Vector2.right;
        }

    }


    //Determine if there is a wall in front
    bool validMove(Vector2 dir)
    {
        Vector2 pos = transform.position;
        for (int i = 10; i > 0; i--)
        {
            RaycastHit2D hit = Physics2D.Linecast(pos + dir * i / 8, pos);

            if (hit.collider.tag == "wall")
            {
                return false;
            }
        }

        return true;
    }


}
