using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string itemName;
    public int id, count;
    [Multiline]
    public string description;
    public bool isUniq;

    public ItemData(string name, int id, int count, bool isUniq, string description)
    {
        this.itemName = name;
        this.id = id;
        this.count = count;
        this.isUniq = isUniq;
        this.description = description;
    }

}
