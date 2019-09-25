using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement_red : monster_movement
{
    bool naviOn = false;

    void FixedUpdate()
    {
        destroyNearPacman();
    }

    override protected void setUp()
    {
        animatorController.SetTrigger("down");
        dest = transform.position;
        myColor = monsterColor.red;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false)
        {
            if(GameManager.pacman)//If find Pac-Man, then chase
            {
                return naviTo(chase(GameManager.pacman));
            }
            //Otherwise, randomly select a forward at the adjacent point of the current way point.
            return redomToNext();
        }
        else
        {
            return naviTo(this.naviWp);
        }

    }

    private wayPoint chase(GameObject aimObj) {
        wayPoint aimWp = null;
        float minDistance = 999;
        //Choose a way point that is the shortest distance from Pac-Man and navigate
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

    void destroyNearPacman()//When pacman is too close to the monster, destroy it
    {
        if (GameManager.pacman) {
            if (Vector3.Distance(transform.position, GameManager.pacman.transform.position) < 0.2f)
            {
                if (!GameManager.pacman.GetComponent<Pacmen_proction>())
                {
                    Destroy(GameManager.pacman);
                    GameObject.Find("GameManager").GetComponent<GameManager>().pacmanDeth();
                }
            }
        }
    }

}
