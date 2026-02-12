using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPref;
    public GameObject inventoryPanel, chestPanel;
    public GameObject invContent, chestContent;
    public ItemData[] items;
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> currentChestSlots = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        inventoryPanel = GameObject.Find("CharInventoryPanel");
        chestPanel = GameObject.Find("ChestPanel");
        invContent = GameObject.Find("InventoryContent");
        chestContent = GameObject.Find("ChestContent");
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
    }
    public void CreateItem(int itemId, List<ItemData> itemsList)
    {
        //ItemData item = items[itemId]; так не можна, подумайте, чому?
        ItemData item = new ItemData(items[itemId].itemName, items[itemId].id,
        items[itemId].count, items[itemId].isUniq, items[itemId].description);

        if (!item.isUniq && itemsList.Count > 0)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (item.id == itemsList[i].id)
                {
                    itemsList[i].count += 1;
                    break;
                }
                else if (i == itemsList.Count - 1)
                {
                    itemsList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && itemsList.Count == 0))
        {
            itemsList.Add(item);
        }
    }
    public void InstantiatingItem(ItemData item, Transform parent, List<GameObject> itemsList)
    {
        GameObject go = Instantiate(slotPref);
        go.transform.SetParent(parent.transform);
        go.AddComponent<SlotScript>();

        go.GetComponent<SlotScript>().itemData = item;

        go.transform.Find("ItemNameText").GetComponent<TMP_Text>().text = item.itemName;
        go.transform.Find("ItemImage").GetComponent<Image>().sprite =
            Resources.Load<Sprite>(item.itemName);

        go.transform.Find("ItemCountText").GetComponent<TMP_Text>().text = item.count.ToString();
        go.transform.Find("ItemCountText").GetComponent<TMP_Text>().color =
            item.isUniq ? Color.clear : Color.white; //використовуємо тернарний оператор

        itemsList.Add(go);
    }
}
