using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman_move_animation : MonoBehaviour
{
    public Animator animatorController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animatorController.SetTrigger("right_enter");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animatorController.SetTrigger("left_enter");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animatorController.SetTrigger("up_enter");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animatorController.SetTrigger("down_enter");
        }
    }
}
