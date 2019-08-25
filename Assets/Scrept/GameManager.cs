using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int gameTime = 60;
    public int gameTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        gameTimeLeft = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Score").GetComponent<Text>().text = score+"";
    }
}
