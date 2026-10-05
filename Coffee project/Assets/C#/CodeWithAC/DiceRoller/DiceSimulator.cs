using UnityEngine;

public class DiceSimulator : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int randomNumber = UnityEngine.Random.RandomRange(1, 7);

            Debug.Log(randomNumber);
            if (randomNumber == 6) Debug.Log("Critical Hit!");
        }
    }
}
