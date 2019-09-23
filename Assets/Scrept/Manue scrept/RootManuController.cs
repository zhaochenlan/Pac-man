using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RootManuController : MonoBehaviour
{
    public InputField keyCodeField;

    // Start is called before the first frame update
    void Start()
    {
       DontDestroyOnLoad(GameObject.Find("KeyCodeCarrier"));
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
            if (EventSystem.current.currentSelectedGameObject.name == "Button_rootMode")
            {
                readKeyCode();
            }
            if (EventSystem.current.currentSelectedGameObject.name == "Button_back")
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }

    void readKeyCode() {
        if (keyCodeField.text == "")
        {
            keyCodeField.text = this.gameObject.GetComponent<KeyCodeManager>().generateKeyCode();

        } else {
            if (this.gameObject.GetComponent<KeyCodeManager>().CheckKeyCode(keyCodeField.text))
            {
                Debug.Log(keyCodeField.text);
                GameObject.Find("KeyCodeCarrier").GetComponent<KeyCodeCarrier>().KeyCode = keyCodeField.text;
                GameObject.Find("KeyCodeCarrier").GetComponent<KeyCodeCarrier>().rootNumber = this.gameObject.GetComponent<KeyCodeManager>().KeyCodeToNumber(keyCodeField.text);
                SceneManager.LoadScene("GameScene(Root)");
            }
            else
            {
                keyCodeField.text = "";
                keyCodeField.placeholder.GetComponent<Text>().text = "wrong KeyCode";
            }
        }
    }

}
