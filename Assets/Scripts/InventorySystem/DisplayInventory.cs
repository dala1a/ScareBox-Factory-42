using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InvMenuController invMenuController;
    public MouseItem mouseItem = new MouseItem(); 
    public InventoryObject inventory;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int Y_START;
    public int X_SPACE_BETWEEN_SLOTS;
    public int Y_SPACE_BETWEEN_SLOTS;
    public int Y_START_SLOTS;
    public int NUMBER_OF_ROWS;
    public GameObject InventoryPrefab;


    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createSlots();
    }

    // Update is called once per frame
    void Update()
    {   

        updateSlots();
    }

    public void dropItem()
    {
        Debug.Log("DRIOPIG");
        if (mouseItem.canDrop)
        {
            inventory.dropItem(mouseItem.hoverItem.item);
        }
    }

    public void updateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void createSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPositionSlots(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnExitDrag(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }

    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObject = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            mouseItem.hoverItem = itemsDisplayed[obj];
            if (itemsDisplayed[obj].ID >= 0)
            {
                mouseItem.canDrop = true; 
            }
        }
    }

    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObject = null;
        mouseItem.hoverItem = null;
        mouseItem.canDrop = false; 
    }

    public void OnStartDrag(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(35, 35);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject; 
        mouseItem.item = itemsDisplayed[obj];
    }

    public void OnExitDrag(GameObject obj)
    {
        if (mouseItem.hoverObject)
        {
            inventory.moveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObject]);
        }
        else
        {

        }
        Destroy(mouseItem.obj);
        mouseItem.item = null; 

    }

    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition; 
        }

    }

    public void OnClick(GameObject obj)
    {
        if (itemsDisplayed[obj].ID >= 0)
        {
            if (mouseItem.prevObj == null)
                mouseItem.prevObj = obj;

            if (mouseItem.hoverItem != itemsDisplayed[mouseItem.prevObj] && invMenuController.getMenuState())
            {
                invMenuController.toggleMenu(getItemSlot(mouseItem.prevObj), itemsDisplayed[obj]);
                mouseItem.prevObj = obj;
                invMenuController.toggleMenu(getItemSlot(obj), itemsDisplayed[obj]);
                return;
            }
            invMenuController.toggleMenu(getItemSlot(obj), itemsDisplayed[obj]);
            mouseItem.prevObj = obj;
        }
    }

    public int getItemSlot(GameObject obj)
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID == itemsDisplayed[obj].ID)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 getPositionItems(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_ITEMS, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_ROWS)), 0f);
    }

    public Vector3 getPositionSlots(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_SLOTS, Y_START_SLOTS + (-Y_SPACE_BETWEEN_SLOTS * (i % NUMBER_OF_ROWS)), 0f);
    }
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObject;
    public GameObject prevObj;
    public bool canDrop; 
}
