using UnityEngine;

public class CustomerBasket : MonoBehaviour
{
    private CustomersManager customersManager;

    void Start()
    {
        customersManager = FindObjectOfType<CustomersManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        CustomerProp prop = other.GetComponent<CustomerProp>();
        if (prop)
        {
            customersManager.PutPropInBasket(prop);
        }        
    }
    void OnTriggerExit(Collider other)
    {
        CustomerProp prop = other.GetComponent<CustomerProp>();
        if (prop)
        {
            customersManager.RemovePropFromBasket(prop);
        }        
    }
}
