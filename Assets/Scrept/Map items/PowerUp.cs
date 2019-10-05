using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D co2)//pac-man eat power-up pill, and all monsters transfer into purple monster
    {
        if (co2.tag == "pacman")
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("monster");
            for (int i=0; i< monsters.Length; i++)
            {
                monsters[i].GetComponent<monster_movement>().inToPurple();
            }
            GameManager.powerUp.Play();
            Destroy(gameObject);
        }
    }
}
