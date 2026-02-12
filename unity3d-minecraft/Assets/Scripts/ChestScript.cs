using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
public List<ItemData> chestItems = new List<ItemData>();

void Awake() {
    InventoryManagerScript inventoryManager = 
        GameObject.Find("InventoryManager").GetComponent<InventoryManagerScript>();

    int itemCountInChest = Random.Range(3, 7);
    for (int i = 0; i < itemCountInChest; i++) {
        inventoryManager.CreateItem(
            Random.Range(0, inventoryManager.items.Length),
            chestItems);
    }
}
}
