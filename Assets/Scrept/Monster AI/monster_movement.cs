using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement : MonoBehaviour
{
    public float speed = 4f;
    protected Vector2 dest = Vector2.zero;
    public Animator animatorController;
    Collider2D cor2D;

    public wayPoint lastWp;
    public wayPoint curWp;
    public wayPoint nextWp;
    public wayPoint naviWp;

    private wayPoint nextboxWp;
    private wayPoint[] boxWps;

    protected List<wayPoint> Wps;

    public enum monsterColor
    {
        red,
        blue,
        green,
        pink,
    }

    public monsterColor myColor;
    public int weekUpTime;

    // Start is called before the first frame update
    protected void Start()
    {
        setUp();
        getWayPoints();
        boxWps = GameObject.Find("GameManager").GetComponent<GameManager>().boxWP;
        nextboxWp = boxWps[Random.Range(0, 4)];
    }

    virtual protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
    }

    protected void Update()
    {
        if (GameManager.gameTime > weekUpTime) {
            moveToNext();
        } else
        {
            moveInBox();
        }
    }

    protected void moveInBox()//Randomly move in the box before the monster wakes up
    {
        float distance = Vector3.Distance(transform.position, nextboxWp.transform.position);
        if (distance > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextboxWp.transform.position, Time.deltaTime * speed);
        }
        else
        {
            nextboxWp = boxWps[Random.Range(0, 4)];
            changeDirection();
        }
    }

    void moveToNext() {
        //Determine whether the current position is equal to the way point, if not, continue moving
        float distance = Vector3.Distance(transform.position, nextWp.transform.position);

        if (distance > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextWp.transform.position, Time.deltaTime * speed);
        }
        else
        {
            lastWp = curWp;
            curWp = nextWp;
            nextWp = findNextWp();
            changeDirection();        
        }
    }

   virtual protected wayPoint findNextWp() {//get next way point. Will be implement by different monster's AI.
        return redomToNext();
    }

    protected wayPoint redomToNext()
    {
        int r;
        int i = 0;

        do//Randomly select a forward at the adjacent point of the current way point
        {
            r = Random.Range(0, nextWp.neighborWps.Count);
            i++;

            if(i > 100 && nextWp.neighborWps[r] != GameManager.StartWP)
            {
                break;
            }

        } while (nextWp.neighborWps[r] == lastWp || nextWp.neighborWps[r] == GameManager.StartWP);//Prevent returning to the previous path point

        return nextWp.neighborWps[r];
    }

    protected wayPoint naviTo(wayPoint aimWp)//Get the first Way point on the navigation path
    {
        if(curWp != aimWp)
        {
            return getNavi(this.curWp, aimWp)[1];
        }
        return aimWp;
    }

    protected List<wayPoint> getNavi(wayPoint curWp, wayPoint aimWp) { //Find the shortest path from cur way point to the target way point
        List<wayPoint> markedWp = new List<wayPoint>();
        List<wayPoint> ShortestPath = new List<wayPoint>();
        List<wayPoint> WaitList = new List<wayPoint>();
        List<int> fatherPoints = new List<int>();
        wayPoint fp;
        int mark;

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
                        mark = fatherPoints[fatherPoints.Count-1];
                        //Get the shortest path through the parent way point
                        while (ShortestPath[0]!=curWp)
                        {
                            fp = WaitList[mark];
                            ShortestPath.Insert(0,fp);
                            mark = fatherPoints[mark];
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

    protected void getWayPoints() {//get all way points on the map
        GameObject[] allWpObj;
        allWpObj = GameObject.FindGameObjectsWithTag("wayPoints");
        Wps = new List<wayPoint>();

        for (int i = 0; i < allWpObj.Length; i++)
        {
            Wps.Add(allWpObj[i].GetComponent<wayPoint>());
        }

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

        if (gameObject.GetComponent<monster_movement_purple>())
            monster_purple.GetComponent<monster_movement_purple>().PatrolPath = gameObject.GetComponent<monster_movement_purple>().PatrolPath;

        Instantiate(monster_purple, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    virtual protected void changeDirection()//Change the animation by the current direction of movement
    {
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
