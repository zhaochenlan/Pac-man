using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacmen_proction : MonoBehaviour
{
    private float calTime = 0f;
    private float dispierTime = 100f;
    // Start is called before the first frame update
    void Start()
    {
        calTime = 0f;
        dispierTime = 100f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dispierTime--;
        if (dispierTime < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject.GetComponent<Pacmen_proction>());
        }
    }

    void Update()
    {
        calTime += Time.time;//Accumulate the interval of each frame
        if (calTime % 2 > 0.5)// Flashes once every 1 second
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
