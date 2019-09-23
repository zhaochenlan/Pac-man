using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
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
            GameObject.Find("GameManager").GetComponent<GameManager>().score +=1;
            if(!GameObject.Find("GameManager").GetComponent<GameManager>().eatDots.isPlaying)
                GameObject.Find("GameManager").GetComponent<GameManager>().eatDots.Play();
            Destroy(gameObject);
        }
    }
}
