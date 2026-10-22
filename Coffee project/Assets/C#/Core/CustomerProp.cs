using UnityEditor.Build;
using UnityEngine;

public class CustomerProp : MonoBehaviour
{
    public ItemTypeEnum ItemType;
    public bool IsItFood = true;
    public string ItemFullName;
    public enum ItemTypeEnum
    {
        BlueDonut,
        PinkDonut,
        ChocolateDonut,
        WhiteCoffee,
        DarkCoffee
    }
    
}

