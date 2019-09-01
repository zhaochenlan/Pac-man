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

    public wayPoint naviWp;

    protected int weekUpTime = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        setUp();
    }

    virtual protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
    }

    protected void FixedUpdate()
    {
        if (nextWp != null && Time.time > weekUpTime) {
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

   virtual protected wayPoint findNextWp() {
        //随机在当前路径点相邻点选择一个前进
        int r;
        do
        {
            r = Random.Range(0, nextWp.neighborWps.Length);
        } while (nextWp.neighborWps[r] == lastWp);//防止返回上一个路径点

        return nextWp.neighborWps[r];
    }

    protected wayPoint naviTo(wayPoint aimWp) {
        if(curWp != aimWp)
        {
            return getNavi(this.curWp, aimWp)[1];
        }
        return aimWp;
    }

    protected List<wayPoint> getNavi(wayPoint curWp, wayPoint aimWp) { //寻找前往目标路径点点最短路径
        List<wayPoint> markedWp = new List<wayPoint>();
        List<wayPoint> ShortestPath = new List<wayPoint>();
        List<wayPoint> WaitList = new List<wayPoint>();
        List<int> fatherPoints = new List<int>();
        wayPoint fp = null;
        int a = 0;

        WaitList.Add(curWp);
        markedWp.Add(curWp);

       fatherPoints.Add(-1);//当前路径点的父节点不存在，标记为-1
       // Debug.Log(curWp.no + " to "+ aimWp);

        for (int i=0; i< WaitList.Count; i++)
        {
            
            for (int j = 0; j < WaitList[i].neighborWps.Length; j++) {

                if (!markedWp.Contains(WaitList[i].neighborWps[j]))
                {
                    markedWp.Add(WaitList[i].neighborWps[j]);
                    if (WaitList[i].neighborWps[j] == aimWp)
                    {
                        fatherPoints.Add(i);
                        WaitList.Add(WaitList[i].neighborWps[j]);
                        //找到目标点
                        ShortestPath.Add(aimWp);
                        a = fatherPoints[fatherPoints.Count-1];
                        //通过父节点追溯获取最短路径
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
                        fatherPoints.Add(i);//记录Wait List中每一个元素的父节点
                        WaitList.Add(WaitList[i].neighborWps[j]);//在等待表之后加上当前路径点的所有相邻点
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
