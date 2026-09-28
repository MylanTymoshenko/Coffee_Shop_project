using UnityEngine;using System;using System.Collections;using System.Collections.Generic;
public class TestA : MonoBehaviour
{
    [ContextMenu("get name")]
    void GetName()
    {
        TestB testB = new TestB();
        Debug.Log(testB.Name);
    }

    [ContextMenu("Get Number")]
    void GetNumber()
    {
        TestB testB = new TestB();
        Debug.Log(testB.Number);
    }

    [ContextMenu("Set and get number")]
    void SetAndGet()
    {
        TestB testB = new TestB();
        testB.Number = 30;
        Debug.Log(testB.Number);
    }
}
public class TestB
{
    private int number = 50;
    private string name = "test";

    public string Name // no () brackets
    {
        get { return name; } // returns value
        set { name = value; } // value is key word
    }

    public int Number // property 2
    {
        get 
        { 
            if (number > 30)
            {
                number -= 10;
            }
            return number; // return
        }
        set
        {  // if its empty inside nothing will happen

            if (value > 20)
            {
                value = 10;
            }
            number = value;

        } 
    }
}
