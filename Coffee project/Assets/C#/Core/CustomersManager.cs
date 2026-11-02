using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
public class CustomersManager : MonoBehaviour
{
    public Order GenerateRandomOrder()
    {
        List<CustomerProp> DrinkItems = new List<CustomerProp>(PropsData.AllItems.Where(item => item.IsItFood == false));
        List<CustomerProp> FoodItems = new List<CustomerProp>(PropsData.AllItems.Where(item => item.IsItFood == true));

        Debug.Log(DrinkItems.Count + "" + "" + FoodItems.Count);
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
        CheckForWrongItems();
        CheckOrderValidity();
    }
    void CheckForWrongItems()
    {
        bool allCorrect = true;

        foreach (CustomerProp prop in currentPropsInBasket)
        {
            if (CheckPropValidity(prop) == false)
            {
                allCorrect = false;
                break;

            }
        }
        uimanager.WrongItemText.gameObject.SetActive(!allCorrect);
    }
    public void RemovePropFromBasket(CustomerProp prop)
    {
        currentPropsInBasket.Remove(prop);
        CheckForWrongItems();
        CheckOrderValidity();
    }

    private void CheckOrderValidity()
    {
        if (currentPropsInBasket.Count == 2 &&
            CheckPropValidity(currentPropsInBasket[0]) &&
             CheckPropValidity(currentPropsInBasket[1]) &&
              currentPropsInBasket[0].ItemType != currentPropsInBasket[1].ItemType) // correct order
        {
            StartCoroutine(OrderCompletedCor());
        }
        else // wrong order
        {

        }
    }
    private bool CheckPropValidity(CustomerProp prop)
    {
        if (CurrentOrder == null) return false;

        if (prop.ItemType == CurrentOrder.ItemDrink || prop.ItemType == CurrentOrder.ItemFood)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator OrderCompletedCor()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(DisplayTextCor("Thank you!", true));


        foreach (CustomerProp prop in currentPropsInBasket)
        {
            Destroy(prop.gameObject);
        }

        currentPropsInBasket.Clear();
        completedOrders++;
        uimanager.CustomersCountText.text = completedOrders.ToString() + "/3 Orders completed";
      
        CurrentOrder = null;
        CurrentCustomer.OrderCompleted();
        CurrentCustomer = null;
    }




    public Transform DestinationForCustomers;
    public Transform ReturnPoint;
    public Transform MiddlePoint;
    public Transform SpawnPoint;

    public Customer CustomerPrefab;
    private int CustomersCount = 3;
    private int spawnedCount;

    public PropsListData PropsData;
    [NonSerialized] public Order CurrentOrder;

    [HideInInspector] public Customer CurrentCustomer;
    private UIManager uimanager;
    private List<CustomerProp> currentPropsInBasket = new List<CustomerProp>();
    private int completedOrders;
    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
        SpawnNextCustomer();
    }

    public void SpawnNextCustomer()
    {
        if (spawnedCount >= CustomersCount) return;

        spawnedCount++;
        Instantiate(CustomerPrefab, SpawnPoint.position, SpawnPoint.rotation);
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

        foreach (char letter in text)
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