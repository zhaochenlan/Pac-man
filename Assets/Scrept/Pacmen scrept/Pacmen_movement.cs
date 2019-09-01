using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacmen_movement : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector2 dest = Vector2.zero;
    public Animator animatorController;
    enum direction {
        right,
        left,
        up,
        down
    }

    direction myDirection = direction.right;
    bool onPress = false;

    private void Start()
    {
        dest = transform.position;
        animatorController.SetTrigger("right_enter");
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
        if(!Input.GetKey(KeyCode.UpArrow)&&this.myDirection == direction.up) {
            onPress = false;
        }
        if (!Input.GetKey(KeyCode.DownArrow) && this.myDirection == direction.down)
        {
            onPress = false;
        }
        if (!Input.GetKey(KeyCode.LeftArrow) && this.myDirection == direction.left)
        {
            onPress = false;
        }
        if (!Input.GetKey(KeyCode.RightArrow) && this.myDirection == direction.right)
        {
            onPress = false;
        }
    }

    private void changeDirection() {
        if (Input.GetKey(KeyCode.UpArrow) && this.myDirection != direction.up)
        {
            animatorController.SetTrigger("up_enter");
            this.myDirection = direction.up;
            onPress = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && this.myDirection != direction.down)
        {
            animatorController.SetTrigger("down_enter");
            this.myDirection = direction.down;
            onPress = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && this.myDirection != direction.left)
        {
            animatorController.SetTrigger("left_enter");
            this.myDirection = direction.left;
            onPress = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && this.myDirection != direction.right)
        {
            animatorController.SetTrigger("right_enter");
            this.myDirection = direction.right;
            onPress = true;
        }
    }

    private void AutoMove() {

        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        if (myDirection == direction.up) {
            dest = (Vector2)transform.position + Vector2.up;
        }
        if (myDirection == direction.down) {
            dest = (Vector2)transform.position + Vector2.down;
        }
        if (myDirection == direction.left) {
            dest = (Vector2)transform.position + Vector2.left;
        }
        if (myDirection == direction.right) {
            dest = (Vector2)transform.position + Vector2.right;
        }

    }


    private void demoMove() {

    }


}
