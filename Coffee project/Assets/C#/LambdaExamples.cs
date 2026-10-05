using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LambdaExamples : MonoBehaviour
{
    [ContextMenu("Output")]
    void Output()
    {
        List<int> numbers = new List<int>{1 , 34, 3, 24,77};

        List<int> test = numbers.FindAll(x => x > 2);

        string text = string.Join(" ", test.Where(x => x > 10)); 

        Debug.Log(text);
    }

    
}
