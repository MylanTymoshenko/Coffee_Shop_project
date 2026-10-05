using UnityEngine;

public class LightOffOn : MonoBehaviour
{
    public Light LightSource;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) LightSource.enabled = !LightSource.enabled;
    }
}
