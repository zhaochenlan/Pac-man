using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacmen_movement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 dest;
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
        dest = Vector2.zero;
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

    private void AutoMove()//If the direction does not change, Pac-Man will continue to move forward in the current direction.
    {

        if (myDirection == Direction.up && validMove(Vector2.up)) {
            dest = Vector2.up;
            Move();
        }
        if (myDirection == Direction.down && validMove(Vector2.down)) {
            dest = Vector2.down;
            Move();
        }
        if (myDirection == Direction.left && validMove(Vector2.left)) {
            dest = Vector2.left;
            Move();
        }
        if (myDirection == Direction.right && validMove(Vector2.right)) {
            dest = Vector2.right;
            Move();
        }
        
    }

    private void Move()//Continuous movement and independent of the frame
    {
        transform.Translate(dest * Time.deltaTime * speed, Space.World);
    }


    bool validMove(Vector2 dir)//Determine if there is a wall in front
    {
        Vector2 pos = transform.position;
        for (int i = 5; i > 0; i--)//Check many times to prevent detection of monsters and ignore walls
        {
            RaycastHit2D hit = Physics2D.Linecast(pos + dir * i / 5, pos);

            if (hit.collider.tag == "wall")
            {
                return false;
            }
        }

        return true;
    }


}
