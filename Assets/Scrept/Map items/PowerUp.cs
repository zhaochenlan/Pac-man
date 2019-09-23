using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D co2)
    {
        if (co2.tag == "pacman")
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("monster");
            for (int i=0; i< monsters.Length; i++)
            {
                monsters[i].GetComponent<monster_movement>().inToPurple();
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().powerUp.Play();
            Destroy(gameObject);
        }
    }
}
