using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PropsListData", menuName = "Scriptable Objects/PropsListData")]
public class PropsListData : ScriptableObject
{
    public List<CustomerProp> AllItems;
}
