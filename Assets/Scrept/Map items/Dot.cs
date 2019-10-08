using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D co2)//pac-man eat dot and earn score
    {
        if (co2.tag == "pacman")
        {
            GameManager.score +=1;
            if(!GameManager.eatDots.isPlaying)
                GameManager.eatDots.Play();
            Destroy(gameObject);
        }
    }
}
