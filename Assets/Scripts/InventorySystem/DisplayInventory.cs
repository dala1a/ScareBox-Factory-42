using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{

    public InventoryObject inventory;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int Y_START; 
    public int NUMBER_OF_ROWS; 

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createDisplay(); 
    }

    // Update is called once per frame
    void Update()
    {
        updateDisplay(); 
    }

    public void updateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(inventory.Container[i]))
            { 
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = getPosition(i);
                itemsDisplayed.Add(inventory.Container[i], obj); 
            }

        }
    }

    public void createDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            itemsDisplayed.Add(inventory.Container[i], obj); 
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_ITEMS, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_ROWS)), 0f); 
    }
}
