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
            Debug.Log(getWayPoints().Count);
        }
    }

    override protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
        myColor = monsterColor.pink;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false) {
            //Randomly select a forward at the adjacent point of the current path point
            return redomToNext();
        } else
        {
            return naviTo(this.naviWp);
        }
        
    }
}
