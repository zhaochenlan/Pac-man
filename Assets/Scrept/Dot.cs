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
        if (co2.name == "pacman")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().score +=1;
            Destroy(gameObject);
        }
    }
}
