using UnityEngine;

public class TillManager : MonoBehaviour
{
    void Start()
    {
        ProcessOrder();
    }

    void ProcessOrder()
    {
        Debug.Log(Customer.drinkType);
    }
}
