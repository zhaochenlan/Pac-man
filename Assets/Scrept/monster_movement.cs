using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement : MonoBehaviour
{
    public float speed = 0.15f;
    protected Vector2 dest = Vector2.zero;
    public Animator animatorController;
    Collider2D cor2D;

    public wayPoint lastWp;
    public wayPoint curWp;
    public wayPoint nextWp;

    // Start is called before the first frame update
    void Start()
    {
        animatorController.SetTrigger("left");
        dest = transform.position;
    }

    void FixedUpdate()
    {
        if (nextWp != null) {
            moveToNext();
        }
    }

    void moveToNext() {
        //判断当前位置是否不等于路经点，不等于就继续移动等于就转向下一个路径点
        float distance = Vector3.Distance(transform.position, nextWp.transform.position);

        if (distance > 0.1f)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, nextWp.transform.position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else
        {
            lastWp = curWp;
            curWp = nextWp;
            nextWp = findNextWp();
            changeDirection();
        }
    }

    protected wayPoint findNextWp() {
        int r;
        do
        {
            r = Random.Range(0, nextWp.neighborWps.Length);

        } while (nextWp.neighborWps[r] == lastWp);

        return nextWp.neighborWps[r];
    }

    protected void changeDirection() {
        Vector2 dir = (Vector2)nextWp.transform.position - (Vector2)transform.position;
        //Debug.Log(dir.x+" "+ dir.y);
        if (dir.x > 0.5) {
            animatorController.SetTrigger("right");
        }
        if (dir.x < -0.5)
        {
            animatorController.SetTrigger("left");
        }
        if (dir.y > 0.5)
        {
            animatorController.SetTrigger("up");
        }
        if (dir.y < -0.5)
        {
            animatorController.SetTrigger("down");
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
