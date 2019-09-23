using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement : MonoBehaviour
{
    public float speed = 0.08f;
    protected Vector2 dest = Vector2.zero;
    public Animator animatorController;
    Collider2D cor2D;

    public wayPoint lastWp;
    public wayPoint curWp;
    public wayPoint nextWp;
    public wayPoint naviWp;

    private wayPoint boxWp;

    public enum monsterColor
    {
        red,
        blue,
        green,
        pink,
        black
    }

    public monsterColor myColor;
    public int weekUpTime;

    // Start is called before the first frame update
    protected void Start()
    {
        setUp();
        boxWp = GameObject.Find("GameManager").GetComponent<GameManager>().boxWP[Random.Range(0, 4)];
    }

    virtual protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
    }

    protected void FixedUpdate()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameTime > weekUpTime) {
            moveToNext();
        } else
        {
            moveInBox();
        }
    }

    protected void moveInBox()
    {
        float distance = Vector3.Distance(transform.position, boxWp.transform.position);
        if (distance > 0.1f)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, boxWp.transform.position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else
        {
            boxWp = GameObject.Find("GameManager").GetComponent<GameManager>().boxWP[Random.Range(0, 4)];
            changeDirection();
        }
    }

    void moveToNext() {
        //Determine whether the current position is equal to the way point, if not, continue moving
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

   virtual protected wayPoint findNextWp() {
        return redomToNext();
    }

    protected wayPoint redomToNext()
    {
        //Randomly select a forward at the adjacent point of the current way point
        int r;
        int i = 0;
        do
        {
            r = Random.Range(0, nextWp.neighborWps.Count);
            i++;

            if(i > 100 && nextWp.neighborWps[r] != GameObject.Find("GameManager").GetComponent<GameManager>().StartWP)
            {
                break;
            }

        } while (nextWp.neighborWps[r] == lastWp || nextWp.neighborWps[r] == GameObject.Find("GameManager").GetComponent<GameManager>().StartWP);//防止返回上一个路径点

        return nextWp.neighborWps[r];
    }

    protected wayPoint naviTo(wayPoint aimWp) {
        if(curWp != aimWp)
        {
            return getNavi(this.curWp, aimWp)[1];
        }
        return aimWp;
    }

    protected List<wayPoint> getNavi(wayPoint curWp, wayPoint aimWp) { //Find the shortest path to the target way point
        List<wayPoint> markedWp = new List<wayPoint>();
        List<wayPoint> ShortestPath = new List<wayPoint>();
        List<wayPoint> WaitList = new List<wayPoint>();
        List<int> fatherPoints = new List<int>();
        wayPoint fp = null;
        int a = 0;

        WaitList.Add(curWp);
        markedWp.Add(curWp);

       fatherPoints.Add(-1);//If the parent of the current way point does not exist, it is marked as -1

        for (int i=0; i< WaitList.Count; i++)
        {
            
            for (int j = 0; j < WaitList[i].neighborWps.Count; j++) {

                if (!markedWp.Contains(WaitList[i].neighborWps[j]))
                {
                    markedWp.Add(WaitList[i].neighborWps[j]);
                    if (WaitList[i].neighborWps[j] == aimWp)
                    {
                        fatherPoints.Add(i);
                        WaitList.Add(WaitList[i].neighborWps[j]);
                        //Find the target way point
                        ShortestPath.Add(aimWp);
                        a = fatherPoints[fatherPoints.Count-1];
                        //Get the shortest path through the parent way point
                        while (ShortestPath[0]!=curWp)
                        {
                            fp = WaitList[a];
                            ShortestPath.Insert(0,fp);
                            a = fatherPoints[a];
                        }

                        return ShortestPath;
                    }
                    else
                    {
                        fatherPoints.Add(i);//Record the parent of each element in the Wait List
                        WaitList.Add(WaitList[i].neighborWps[j]);//Add all adjacent way points of the current way point after waiting for the table
                    }
                }
            }
                
        }
            return ShortestPath;

    }

    protected List<wayPoint> getWayPoints() {
        GameObject[] allWpObj;
        List<wayPoint> Wps = new List<wayPoint>();

        allWpObj = GameObject.FindGameObjectsWithTag("wayPoints");
        for (int i = 0; i < allWpObj.Length; i++)
        {
            Wps.Add(allWpObj[i].GetComponent<wayPoint>());
        }

        return Wps;
    }

    public void inToPurple()//Turn purple and start to escape from Pac-Man
    {
        GameObject monster_purple = GameObject.Find("GameManager").GetComponent<GameManager>().purpleMonster;
        monster_purple.GetComponent<monster_movement_purple>().curWp = this.curWp;
        monster_purple.GetComponent<monster_movement_purple>().lastWp = this.lastWp;
        monster_purple.GetComponent<monster_movement_purple>().nextWp = this.nextWp;
        monster_purple.GetComponent<monster_movement_purple>().myColor = this.myColor;

		if (gameObject.GetComponent<monster_movement_green>())
			monster_purple.GetComponent<monster_movement_purple>().PatrolPath = gameObject.GetComponent<monster_movement_green>().PatrolPath;


		Instantiate(monster_purple, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    virtual protected void changeDirection() {
        Vector2 dir = (Vector2)nextWp.transform.position - (Vector2)transform.position;
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

    virtual protected void OnTriggerEnter2D(Collider2D co2)
    {
        if (co2.tag == "pacman")
        {
            if (!co2.GetComponent<Pacmen_proction>())
            {
                Destroy(co2.gameObject);
                GameObject.Find("GameManager").GetComponent<GameManager>().pacmanDeth();
            }
        }

    }

}
