using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class TestC : MonoBehaviour
{
    [ContextMenu("Get + set")]
    void GetAndSet()
    {
        TestD testD = new TestD();
        testD.Name = "abc";
        Debug.Log(testD.Name);
    }

    [ContextMenu("Get tag only")]
    void GetTagOnly()
    {
        TestD testD = new TestD();

        // testD.Tag = "1243"; - this line will cause error, because property tag only can be read
        Debug.Log(testD.Name);
    }
}
public class TestD
{
    private string tag;
    private string name = "test";
    public string Name // in this property both can be accesed.
    {
        get 
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public string Tag // iether get or set can be private, if both are private it will cause an error 
    {
        private get  
        {
            return tag;            
        }
        set // b
        {
            tag = value;
        } 
    }
}