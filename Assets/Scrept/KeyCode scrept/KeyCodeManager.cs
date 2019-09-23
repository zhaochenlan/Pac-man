using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeManager : MonoBehaviour
{
    List<char> Acceptable = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
    List<char> letters = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f' };
    List<char> numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    void Start()
    {
        
    }

    public string generateKeyCode() { //generate a random KeyCode
        string KeyCode = "";

        KeyCode += numbers[Random.Range(1, numbers.Count-1)];
        KeyCode += letters[Random.Range(0, letters.Count - 1)];
        KeyCode += Acceptable[Random.Range(0, Acceptable.Count - 1)];
        KeyCode += Acceptable[Random.Range(0, Acceptable.Count - 1)];

        return KeyCode;
    }

    public bool CheckKeyCode(string KeyCode) {//Check if keyCode is legal

        char[] keys = KeyCode.ToCharArray();

        if (keys.Length != 4)
            return false;
        if (keys[0] == '0')
            return false;

        for (int i = 0; i < keys.Length; i++)
        {
            if (!Acceptable.Contains(keys[i]))
                return false;
        }

        bool value = false;
        for (int i = 0; i < keys.Length; i++)
        {
            if (letters.Contains(keys[i]))
                value = true;
        }

        if (value == false)
            return false;

        value = false;
        for (int i = 0; i < keys.Length; i++)
        {
            if (!letters.Contains(keys[i]))
                value = true;
        }

        if (value == false)
            return false;

        return true;
    }

    public int KeyCodeToNumber(string KeyCode) {
        int result = 0;
        char[] keys = KeyCode.ToCharArray();
        int Nod = 1;

        for (int i = 0; i < keys.Length; i++) {
            if (!Acceptable.Contains(keys[i]))
                return -1;
        }

        for (int i=keys.Length-1; i>=0 ;i--) {
            result += (charToInt(keys[i]) * Nod);
            Nod *= 17;
        }

        return result;
    }

    int charToInt(char a) {
        switch (a)
        {
            case '0': return 0;
            case '1': return 1;
            case '2': return 2;
            case '3': return 3;
            case '4': return 4;
            case '5': return 5;
            case '6': return 6;
            case '7': return 7;
            case '8': return 8;
            case '9': return 9;
            case 'a': return 11;
            case 'b': return 12;
            case 'c': return 13;
            case 'd': return 14;
            case 'e': return 15;
            case 'f': return 16;
        }
        return 0;
    }


}
