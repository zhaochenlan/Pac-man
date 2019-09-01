using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement_red : monster_movement
{
    bool naviOn = false;

    void Update()
    {
        
    }

    override protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
        weekUpTime = 5;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false)
        {
            if (GameObject.Find("pacman"))//如果发现吃豆人就追击
            {
                return naviTo(chase(GameObject.Find("pacman")));
            }
            //否则随机在当前路径点相邻点选择一个前进
            int r;
            do
            {
                r = Random.Range(0, nextWp.neighborWps.Length);
            } while (nextWp.neighborWps[r] == lastWp);//防止返回上一个路径点

            return nextWp.neighborWps[r];
        }
        else
        {
            return naviTo(this.naviWp);
        }

    }

    private wayPoint chase(GameObject aimObj) {
        wayPoint aimWp = null;
        List<wayPoint> Wps = getWayPoints();
        float minDistance = 999;
        //选择与吃豆人距离最短的一个路径点并导航
        for (int i = 0; i < Wps.Count; i++)
        {
            if (Vector3.Distance(aimObj.transform.position, Wps[i].transform.position) < minDistance)
            {
                aimWp = Wps[i];
                minDistance = Vector3.Distance(aimObj.transform.position, Wps[i].transform.position);
            }
        }
        return aimWp;
    }

}
