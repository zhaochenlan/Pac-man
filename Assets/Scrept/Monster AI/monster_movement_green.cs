using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement_green : monster_movement
{
    bool naviOn = false;
    public List<wayPoint> PatrolPath = new List<wayPoint>();
    private int wayMark = 0;

    void Update()
    {

    }

    override protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
        myColor = monsterColor.green;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false)
        {
            if (curWp == PatrolPath[wayMark]) wayMark += 1;

            if (wayMark == PatrolPath.Count) wayMark = 0;

            return naviTo(PatrolPath[wayMark]);
        }
        else
        {
            return naviTo(this.naviWp);
        }

    }
}
