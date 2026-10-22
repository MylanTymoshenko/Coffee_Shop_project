using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CustomersManager : MonoBehaviour
{ 

    public Transform DestinationForCustomers;
    public Transform ReturnPoint;
    public Transform MiddlePoint;

    public PropsListData PropsData;
    [NonSerialized]public Order CurrentOrder;

    [HideInInspector]public Customer CurrentCustomer;
    private UIManager uimanager;
    private List<CustomerProp> currentPropsInBasket = new List<CustomerProp>();

    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
    }

    public void SetCurrentOrder(Order order, Customer customer)
    {
        CurrentOrder = order;
        CurrentCustomer = customer;
        StartCoroutine(DisplayTextCor(CurrentOrder.OrderFullName));
    }

    IEnumerator DisplayTextCor(string text, bool hideAfterMessage = false)
    {
        uimanager.OrderRequestTextAnimator.gameObject.SetActive(true);
        string runText = "";

        foreach(char letter in text)
        {
            runText += letter;
            uimanager.OrderNameText.text = runText;

            yield return new WaitForSeconds(0.05f);
        }

        uimanager.OrderNameText.text = text;

        if (hideAfterMessage)
        {
            yield return new WaitForSeconds(2);
            uimanager.OrderRequestTextAnimator.gameObject.SetActive(false);
        }
       
    }
}

[System.Serializable]
public class Order
{
    public string OrderFullName;
    public CustomerProp.ItemTypeEnum ItemDrink;
    public CustomerProp.ItemTypeEnum ItemFood;
}



