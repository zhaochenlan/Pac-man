using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement_blue : monster_movement
{
    bool naviOn = false;

    void Update()
    {

    }

    override protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
        myColor = monsterColor.blue;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false)
        {
            if (GameObject.FindWithTag("pacman"))//If find Pac-Man, choose one from random moves or run away.
            {
                if (Random.Range(0, 2) == 0)
                {
                    return redomToNext();
                }
                return naviTo(ranAway(GameObject.FindWithTag("pacman")));
            }
            //Otherwise, randomly select a forward at the adjacent point of the current way point.
            return redomToNext();
        }
        else
        {
            return naviTo(this.naviWp);
        }

    }

    private wayPoint ranAway(GameObject aimObj)
    {
        wayPoint aimWp = null;
        List<wayPoint> Wps = getWayPoints();
        float maxDistance = 0;
        //Choose a way point furthest away from Pac-Man and navigate
        for (int i = 0; i < Wps.Count; i++)
        {
            if (Vector3.Distance(aimObj.transform.position, Wps[i].transform.position) > maxDistance)
            {
                aimWp = Wps[i];
                maxDistance = Vector3.Distance(aimObj.transform.position, Wps[i].transform.position);
            }
        }
        return aimWp;
    }
}
