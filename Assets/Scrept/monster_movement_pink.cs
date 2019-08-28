using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement_pink : monster_movement
{
    bool naviOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            naviOn = !naviOn;
            Debug.Log("!");
        }
    }

    override protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
        weekUpTime = 2;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false) {
            //随机在当前路径点相邻点选择一个前进
            int r;
            do
            {
                r = Random.Range(0, nextWp.neighborWps.Length);
            } while (nextWp.neighborWps[r] == lastWp);//防止返回上一个路径点

            return nextWp.neighborWps[r];
        } else
        {
            return naviTo(this.naviWp);
        }
        
    }
}
