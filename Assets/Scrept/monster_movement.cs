using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement : MonoBehaviour
{
    public float speed = 0.15f;
    protected Vector2 dest = Vector2.zero;
    public Animator animatorController;
    Collider2D cor2D;
    enum direction
    {
        right,
        left,
        up,
        down,
        stop,
        no
    }
    direction myDirection = direction.stop;
    int timer = 30;

    // Start is called before the first frame update
    void Start()
    {
        myDirection = direction.right;
        animatorController.SetTrigger("right");
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AI();
        AutoMove();
        timer--;
    }

    protected void AutoMove()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        if (myDirection == direction.up && validMove(Vector2.up))
        {
            dest = (Vector2)transform.position + Vector2.up;
        }
        if (myDirection == direction.down && validMove(Vector2.down))
        {
            dest = (Vector2)transform.position + Vector2.down;
        }
        if (myDirection == direction.left && validMove(Vector2.left))
        {
            dest = (Vector2)transform.position + Vector2.left;
        }
        if (myDirection == direction.right && validMove(Vector2.right))
        {
            dest = (Vector2)transform.position + Vector2.right;
        }

    }

    protected void AI() {
        
        if(myDirection == direction.up && !validMove(Vector2.up))
        {
            changeDirection(Random.Range(0, 4));
        }
        if (myDirection == direction.down && !validMove(Vector2.down))
        {
            changeDirection(Random.Range(0, 4));
        }
        if (myDirection == direction.left && !validMove(Vector2.left))
        {
            changeDirection(Random.Range(0, 4));
        }
        if (myDirection == direction.right && !validMove(Vector2.right))
        {
            changeDirection(Random.Range(0, 4));
        }

        if (directionCheck() != 0) {        
            if (timer <= 0) {
                timer = 30;
                print(directionCheck());
                // if (Random.Range(0, 2) == 1)
                    AutoMove();
                    changeDirection(directionCheck());
                
            }
            
        }
    }

    protected int directionCheck() {
        if ((myDirection != direction.up && myDirection != direction.down) && validMove(Vector2.up * 10))
        {
            return 1;
        }
        if ((myDirection != direction.up && myDirection != direction.down) && validMove(Vector2.down * 10))
        {
            return 2;
        }
        if ((myDirection != direction.left && myDirection != direction.right) && validMove(Vector2.left * 10))
        {
            return 3;
        }
        if ((myDirection != direction.left && myDirection != direction.right) && validMove(Vector2.right * 10))
        {
            return 4;
        }
        return 0;
    }

    protected void changeDirection(int d)
    {
        if (d==1)
        {
            animatorController.SetTrigger("up");
            this.myDirection = direction.up;
        }
        if (d == 2)
        {
            animatorController.SetTrigger("down");
            this.myDirection = direction.down;
        }
        if (d == 3)
        {
            animatorController.SetTrigger("left");
            this.myDirection = direction.left;
        }
        if (d == 4)
        {
            animatorController.SetTrigger("right");
            this.myDirection = direction.right;
        }
    }



    protected bool validMove(Vector2 dir)
    {
        //获得自身位置
        Vector2 pos = transform.position;
        //发射射线从pos+dir到pos    
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        if (hit.collider != null)
        {         
            if (hit.collider.tag != "wall") {
                return true;
            }
            return false;
        }
        else {
            return true;
        }
        
    }

    protected void OnTriggerEnter2D(Collider2D co2)
    {
        if (co2.name == "pacman")
        {
            Destroy(co2.gameObject);
        }

    }

}
