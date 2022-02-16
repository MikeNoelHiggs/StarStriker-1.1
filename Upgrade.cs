using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] int levelRequired;
    [SerializeField] string itemName;
    [SerializeField] string description;

    public string GetName()
    {
        return this.itemName;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public int GetPrice()
    {
        return this.price;
    }

    public int GetLevelRequired()
    {
        return this.levelRequired;
    }
}
