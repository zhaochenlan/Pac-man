using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    public GameObject dot;
    public List<wayPoint> WPs = new List<wayPoint>();
    public GameObject wall;
    public GameObject startWall;
    public GameObject powerUpPill;
    wayPoint lastWp;

    public void buildMaze()
    {
        GameObject.Find("GameManager").GetComponent<GameManager_Root>().noOfDots = 0;

        lastWp = WPs[3];
        transform.position = (Vector2)WPs[1].transform.position+Vector2.up*3;
        MoveAndBuilde(Vector2.right, 10);
        MoveAndBuilde(Vector2.down, 15);
        MoveAndBuilde(Vector2.left, 20);
        MoveAndBuilde(Vector2.up, 15);
        MoveAndBuilde(Vector2.right, 10);

        transform.position = WPs[1].transform.position;
        lastWp = WPs[1];
        buildPath(20);//random build a path in random direction, repeit 20 times to build maze
        transform.position = WPs[2].transform.position;
        lastWp = WPs[2];
        buildPath(15);

        makeWalls();//if there isn't a path, make walls full of the zone.
        Destroy(gameObject);
    }

    void buildPath(int times) {

        int lastDir = 0;

        for (int i = 0; i < times; i++)
        {
            do
            {
                int dir = GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(1000, 5000)/1000;
                //Randomly choose one direction to advance and establish a road, the direction is not the opposite

                if (dir == 1 && lastDir != 2)
                {
                    MoveAndBuilde(Vector2.up, GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(4, 10));
                    break;
                }

                if (dir == 2 && lastDir != 1)
                {
                    MoveAndBuilde(Vector2.down, GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(4, 10));
                    break;
                }

                if (dir == 3 && lastDir != 4)
                {
                    MoveAndBuilde(Vector2.left, GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(4, 10));
                    break;
                }

                if (dir == 4 && lastDir != 3)
                {
                    MoveAndBuilde(Vector2.right, GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(4, 10));
                    break;
                }

                lastDir = dir;

            } while (true);
            
        }
    }

    void MoveAndBuilde(Vector2 dir,int times) {
        for (int i = 0; i < times; i++) {
            if (DetectFront(dir) != "wall" && DetectFront(dir) != "Dot") {    
            //Create a Dot or power-up pill at the current location when there are no obstacles
                if(GameObject.Find("GameManager").GetComponent<RNgenerator>().getRandom(100, 9000) / 100 == 1)//get a random number to dertenmin instantiate a dot or power-up pill
                {
                    Instantiate(powerUpPill, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(dot, transform.position, Quaternion.identity);
                    GameObject.Find("GameManager").GetComponent<GameManager_Root>().noOfDots++;
                }
                              
                makeWP();
                gameObject.transform.position = (Vector2)transform.position + dir;
            }
            
        }
        
    }

    string DetectFront(Vector2 dir)//Detect the object in front and return
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir*3/2, pos + dir/2);

        if (hit) {

            if (hit.collider.tag == "wall")
            {
                return "wall";
            }

            if (hit.collider.tag == "Dot")
            {
                return "Dot";
            }
        }

        return null;
    }

    void makeWP() {//Create a way point at the current location
        if (!LinkWp())
        { //if the cur position hasn't a way point, creat a new one
            GameObject newWP = new GameObject("wayPoint (" + WPs.Count + ")");
            newWP.transform.position = transform.position;
            newWP.AddComponent<CircleCollider2D>();
            newWP.GetComponent<CircleCollider2D>().radius = 1;
            newWP.AddComponent<wayPoint>();
            newWP.GetComponent<wayPoint>().no = WPs.Count;
            newWP.tag = "wayPoints";

            WPs.Add(newWP.GetComponent<wayPoint>());
            GameObject.Find("GameManager").GetComponent<GameManager_Root>().WPs.Add(newWP.GetComponent<wayPoint>());

            newWP.GetComponent<wayPoint>().neighborWps.Add(lastWp);
            lastWp.GetComponent<wayPoint>().neighborWps.Add(newWP.GetComponent<wayPoint>());
            lastWp = newWP.GetComponent<wayPoint>();
        }

    }

    bool LinkWp()//link the cur waypoint to it's neighbor's waypoint, if the cur position already has a way point, then return false
    {
        float distance;
        for (int i = 0; i < WPs.Count; i++)
        {
            distance = Vector2.Distance(transform.position, WPs[i].transform.position);
            if (distance < 0.5f && WPs[i]!=lastWp)
            {
                if (!WPs[i].neighborWps.Contains(lastWp))
                {
                    WPs[i].neighborWps.Add(lastWp);
                    lastWp.neighborWps.Add(WPs[i]);
                }
                lastWp = WPs[i];
                return true;
            }
        }
        return false;
    }


    void makeWalls()//if there isn't a path, make walls full of the zone.
    {
        transform.position = startWall.transform.position;

        for(int k = 0; k < 24; k++)
        {
            makeOneLayer();
        }
    }

    void makeOneLayer()//make one layer of the walls
    {
        float distance;
        bool overlapping = false;
        int fix=0;

        for (int i = 0; i <= 48; i++)
        {

            for (int j = 0; j < WPs.Count; j++)
            {
                distance = Vector2.Distance(transform.position, WPs[j].transform.position);
                if (distance < 0.9f)
                {
                    overlapping = true;
                    break;
                }
            }

            if (!overlapping)
            {
                if (fix == 2)
                {
                    gameObject.transform.position = (Vector2)transform.position - Vector2.right / 2;
                    Instantiate(wall, transform.position, Quaternion.identity);
                    gameObject.transform.position = (Vector2)transform.position + Vector2.right / 2;
                    //Perform repair
                }
                fix = 1;
                Instantiate(wall, transform.position, Quaternion.identity);
            } else
            {
                if (fix == 1)
                {
                    fix = 2;
                } else
                {
                    fix = 0;
                }

            }
                

            overlapping = false;
            gameObject.transform.position = (Vector2)transform.position + Vector2.right / 2;
        }

        gameObject.transform.position = (Vector2)transform.position + Vector2.down / 2;
        gameObject.transform.position = (Vector2)transform.position + Vector2.left / 2;

        for (int i = 0; i <= 48; i++)
        {
            for (int j = 0; j < WPs.Count; j++)
            {
                distance = Vector2.Distance(transform.position, WPs[j].transform.position);
                if (distance < 0.9f)
                {
                    overlapping = true;
                    break;
                }
            }
            if (!overlapping)
            {
                if (fix == 2)
                {
                    gameObject.transform.position = (Vector2)transform.position - Vector2.left / 2;
                    Instantiate(wall, transform.position, Quaternion.identity);
                    gameObject.transform.position = (Vector2)transform.position + Vector2.left / 2;
                    //Perform repair
                }
                fix = 1;
                Instantiate(wall, transform.position, Quaternion.identity);
            }
            else
            {
                if (fix == 1)
                {
                    fix = 2;
                }
                else
                {
                    fix = 0;
                }

            }

            overlapping = false;
            gameObject.transform.position = (Vector2)transform.position + Vector2.left / 2;
        }

        gameObject.transform.position = (Vector2)transform.position + Vector2.down / 2;
        gameObject.transform.position = (Vector2)transform.position + Vector2.right / 2;
    }


}
