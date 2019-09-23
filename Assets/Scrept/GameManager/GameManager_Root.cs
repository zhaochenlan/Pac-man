using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Root : GameManager
{

    public List<wayPoint> WPs = new List<wayPoint>();
    // Start is called before the first frame update
    override protected void setUpGame() {

        if (GameObject.Find("KeyCodeCarrier").GetComponent<KeyCodeCarrier>().KeyCode!=null) {

            GameStartTime = Time.time;

            GameObject.Find("KeyCode").GetComponent<Text>().text = GameObject.Find("KeyCodeCarrier").GetComponent<KeyCodeCarrier>().KeyCode;
            GameObject.Find("GameManager").GetComponent<RNgenerator>().setUp();

            pacman_life = GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(1000, 3000) / 1000;
            setupPacmanLife();

            GameObject.Find("MazeBuilder").GetComponent<MazeBuilder>().buildMaze();

            setUpMonsters();
        }

    }

   override protected void setUpMonsters()
    {
        generatMonster(redMonster);

        int noOfMonsters = GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(1000, 6000) / 1000;
        for(int i = 0; i < noOfMonsters; i++)
        {
            int monsterColor = GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(1000, 4000) / 1000;
            if (monsterColor == 1)
                generatMonster(greenMonster);
            if (monsterColor == 2)
                generatMonster(pinkMonster);
            if (monsterColor == 3)
                generatMonster(blueMonster);
        }
    }

    void generatMonster(GameObject monster)
    {
        GameObject monster_new = null;
        monster_new = monster;
        monster_new.GetComponent<monster_movement>().weekUpTime = GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(1000, 8000) / 1000;
        monster_new.GetComponent<monster_movement>().nextWp = StartWP;

        if (monster == greenMonster)
        {
            monster_new.GetComponent<monster_movement_green>().PatrolPath = getPatrolPath();
        }

        Instantiate(monster_new, StartWP.transform.position, Quaternion.identity);
    }

    List<wayPoint> getPatrolPath()//Randomly generate a patrol path from the current map
    {
        List<wayPoint> PatrolPath = new List<wayPoint>();
        int noOfWps = GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(3000, 6000) / 1000;

        for(int i=0 ;i< noOfWps; i++)
        {
            PatrolPath.Add(WPs[GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(0, WPs.Count)]);
        }

        return PatrolPath;
    }

}
