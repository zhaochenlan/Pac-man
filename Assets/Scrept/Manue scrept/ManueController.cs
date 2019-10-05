using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManueController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
            buttonInput();
        }
    }

    void buttonInput()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name== "Button_play")
            {
                SceneManager.LoadScene("GameScene");
            }
            if (EventSystem.current.currentSelectedGameObject.name == "Button_rootMode")
            {
                SceneManager.LoadScene("RootMenu");
            }
            if (EventSystem.current.currentSelectedGameObject.name == "Button_exit")
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

}
