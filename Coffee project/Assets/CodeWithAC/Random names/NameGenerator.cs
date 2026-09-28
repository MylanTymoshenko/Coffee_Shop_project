using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    private string[] names = { "John" , "Tom", "Alice"};

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (names.Length != 0)
            {
                Debug.Log(names[UnityEngine.Random.RandomRange(0, names.Length)]);
            }
            else
            {
                Debug.LogWarning("Array has no items!");
            }
        }
    }
}
