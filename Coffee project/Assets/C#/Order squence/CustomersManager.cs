using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class CustomersManager : MonoBehaviour
{ 
    [HideInInspector]public Customer CurrentCustomer;
    public PropsListData PropsData;
    private UIManager uimanager;
    [NonSerialized]public Order CurrentOrder;
    public Transform DestinationForCustomers;
    public Transform ReturnPoint;
    public Transform MiddlePoint;

    
    private List<CustomerProp> currentPropsInBasket = new List<CustomerProp>();
    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
       
    }
    public Order GenerateRandomOrder()
    {
        List<CustomerProp> DrinkItems = new List<CustomerProp>(PropsData.AllItems.Where(item => item.IsItFood == false)); 
        List<CustomerProp> FoodItems = new List<CustomerProp>(PropsData.AllItems.Where(item => item.IsItFood == true));


        Debug.Log(DrinkItems.Count +""+ ""+ FoodItems.Count);
        CustomerProp randomDrink = DrinkItems[UnityEngine.Random.RandomRange(0, DrinkItems.Count)];
     
        CustomerProp randomFood = FoodItems[UnityEngine.Random.RandomRange(0, FoodItems.Count)];

        

        Order order = new Order
        {
            ItemDrink = randomDrink.ItemType,
            ItemFood = randomFood.ItemType,
            OrderFullName = "Can i have please " + randomDrink.ItemFullName + " and can i have " + randomFood.ItemFullName

        };

        return order;
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
   
    
    public void PutPropInBasket(CustomerProp prop)
    {
        currentPropsInBasket.Add(prop);
        if (CheckPropValidity(prop))
        {
            Debug.Log("Correct Item");
        }
        else
        {
             Debug.Log("Wrong Item");
        }

        CheckOrderValidity();
    }

    public void RemovePropFromBasket(CustomerProp prop)
    {
        currentPropsInBasket.Remove(prop);
        CheckOrderValidity();
    }
   
    private void CheckOrderValidity()
    {
        if (currentPropsInBasket.Count == 2 && 
            CheckPropValidity(currentPropsInBasket[0]) && 
             CheckPropValidity(currentPropsInBasket[1]) &&
              currentPropsInBasket[0].ItemType !=currentPropsInBasket[1].ItemType ) // correct order
        {
            StartCoroutine(OrderCompletedCor());
        }
        else // wrong order
        {
            
        }
    }

    IEnumerator OrderCompletedCor()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(DisplayTextCor("Thank you!", true));

     
        foreach(CustomerProp prop in currentPropsInBasket)
        {
            Destroy(prop.gameObject);
        }

        currentPropsInBasket.Clear();
        CurrentOrder = null;
        CurrentCustomer.OrderCompleted();
        CurrentCustomer =null;
    }

    private bool CheckPropValidity(CustomerProp prop)
    {
        if (prop.ItemType == CurrentOrder.ItemDrink || prop.ItemType == CurrentOrder.ItemFood)
        {
            return true;
        }
        else
        {
            return false;
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

