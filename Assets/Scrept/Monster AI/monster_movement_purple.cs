using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement_purple : monster_movement
{
    bool naviOn = false;
    int Timer = 5;
    int startTime;
    public List<wayPoint> PatrolPath;

    void FixedUpdate()
    {
        recoverTimer();
        if (curWp == GameManager.StartWP)
        {
            recover(5);
        }
    }

    override protected void setUp()
    {
        dest = transform.position;
        weekUpTime = 0;
        startTime = (int)Time.time;
    }

    override protected wayPoint findNextWp()
    {
        if (naviOn == false)
        {
            if (GameManager.pacman)//If find Pac-Man, choose one from random moves or run away.
            {
                if (Random.Range(0,2) == 0)
                {
                    return redomToNext();
                }
                return naviTo(ranAway(GameManager.pacman));
            }
            //Otherwise, randomly select a forward at the adjacent point of the current way point.
            return redomToNext();
        }
        else
        {
            return naviTo(this.naviWp);
        }

    }

    override protected void changeDirection()
    {

    }

    private wayPoint ranAway(GameObject aimObj)
    {
        wayPoint aimWp = null;
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

    private void recoverTimer()
    {
        if((int)Time.time - startTime == Timer && !naviOn)
        {
            recover(0);
        }
    }

    public void recover(int weekUpTime)//Revert to original color
    {
        GameObject monster_new = null;
        switch (this.myColor)
        {
            case monsterColor.red:
                monster_new = GameObject.Find("GameManager").GetComponent<GameManager>().redMonster;
            break;
            case monsterColor.blue:
                monster_new = GameObject.Find("GameManager").GetComponent<GameManager>().blueMonster;
            break;
            case monsterColor.pink:
                monster_new = GameObject.Find("GameManager").GetComponent<GameManager>().pinkMonster;
            break;
            case monsterColor.green:
                monster_new = GameObject.Find("GameManager").GetComponent<GameManager>().greenMonster;
                monster_new.GetComponent<monster_movement_green>().PatrolPath = this.PatrolPath;
            break;
        }

        monster_new.GetComponent<monster_movement>().curWp = this.curWp;
        monster_new.GetComponent<monster_movement>().lastWp = this.lastWp;
        monster_new.GetComponent<monster_movement>().nextWp = this.nextWp;
        monster_new.GetComponent<monster_movement>().weekUpTime = (int)GameManager.gameTime + weekUpTime;

        Instantiate(monster_new, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    override protected void OnTriggerEnter2D(Collider2D co2)
    {
        if (co2.tag == "pacman")
        {
            //When the purple monster meets the Pac-Man, it immediately accelerates back to the monster box.
            naviOn = true;
            this.naviWp = GameManager.StartWP;
            this.speed = 0.3f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
            GameObject.Find("GameManager").GetComponent<GameManager>().eatMonster.Play();
        }

    }
}
