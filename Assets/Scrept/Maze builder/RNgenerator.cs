using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNgenerator : MonoBehaviour
{

    int f1 = 1, f2 = 1;
    int fn;

    int Threshold = 100000;

    public void setUp() {
        if (GameObject.Find("KeyCodeCarrier").GetComponent<KeyCodeCarrier>().KeyCode != null)
        {
            Fibonacci(GameObject.Find("KeyCodeCarrier").GetComponent<KeyCodeCarrier>().rootNumber);//gat a start value of Fibonacci sequence based on the key code
        }
    }

    public void Fibonacci(int n)//Get the Nth value of the Fibonacci sequence
    {
        for (int i = 3; i <= n; i++)
        {
            fn = f1 + f2;
            if (fn > Threshold)
                fn -= Threshold;
            f1 = f2;
            f2 = fn;
        }
    }

    public int FibonacciGetNext()//Get the next value of the Fibonacci sequence
    {
            fn = f1 + f2;
            if (fn > Threshold)
                fn -= Threshold;
            f1 = f2;
            f2 = fn;
        return fn;
    }

    public int getRandom(int a,int b)//Get a "random" value between a and b
    {
        if (a < b)
        {
            int range = b - a;
            return (FibonacciGetNext() % range)+a;
        }
        return -1;
    } 


}
