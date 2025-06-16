using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

/** 
* Class containing the functionality of the inventory ui. 
* @author: Yunseo Jeon
* @since 2025-05-23
*/ 
public class DisplayInventory : MonoBehaviour
{
    public InvMenuController invMenuController; // Reference to the inventory animation controller class. 
    public MouseItem mouseItem = new MouseItem(); 
    public InventoryObject inventory;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int Y_START;
    public int X_SPACE_BETWEEN_SLOTS;
    public int Y_SPACE_BETWEEN_SLOTS;
    public int Y_START_SLOTS;
    public int NUMBER_OF_ROWS;
    public GameObject InventoryPrefab; // Reference to the inventory slots ui prfab. 
    private GameObject[] invPrefabList = new GameObject[7]; 
    private bool isFlashlightOn = false;
    public GameObject flashlight;

    [SerializeField] PhoneScript phoneScript;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    /** 
    * Create inventory slots on startup
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */
    void Start()
    {
        createSlots();
    }

    /** 
    * Updates the inventory slots with any changes. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */
    void Update()
    {

        updateSlots();
    }

    /** 
    * Drops an item when you hover over a occupied inventory slot. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void dropItem()
    {
        if (mouseItem.canDrop)
        {
            inventory.dropItem(mouseItem.hoverItem.item);
        }
    }

    /** 
    * Updating each inventory slot by getting item information for the item database and using it if the player has the item. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */
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

    /** 
    * Initially generate the inventory slots and create the inventory ui. 
    * @author: Yunseo Jeon. 
    * @since 2025-05-23
    */ 
    public void createSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {   
            // Create a new Game Object
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPositionSlots(i);
            invPrefabList[i] = obj;

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnExitDrag(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
        unselectAllSlots();

    }

    /** 
    * Reset the active inventory items
    * @author: Yunseo Jeon
    * @since 2025-05-23
    */
    private void resetInventoryItems()
    {
        // PHONE
        phoneScript.turnOffPhone();

        // FLASHLIGHT
        flashlight.SetActive(false);
        isFlashlightOn = false;

        //
    }

    /** 
    * Use an item depending on what item is equipped. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */
    public void useItem()
    {
        InventorySlot equippedItem = getEquippedItem();
        
        if (equippedItem == null)
        {
            return;
        }


        if (equippedItem.ID == 2)
        {
            phoneScript.togglePhone();
        }
        else if (equippedItem.ID == 1)
        {
            toggleLight();
        }
        
    }

    /** 
    * Get the current equipped item in the inventory. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @return InventorySlot: The current item equipped. 
    */ 
    public InventorySlot getEquippedItem()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].isEquipped == true)
            {
                return inventory.Container.Items[i];
            }
        }
        return null;
    }

    /** 
    * Used to add a mouse event to the game. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param obj: The gameobject the mosue event is being added to.. 
    * @param type: The type of mouse event being added. (Mouse Click, Mouse Dragged etc)
    * @param action: The action assigned to that mouse event ie methods. 
    */ 
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    /** 
    * When the mouse enters an area with an item it stores that item information so it can be dragged to a new slot. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param obj: The gameobject the mouse is hovering on.  
    */ 
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

    /** 
    * When the mouse exits an area with an item it unstores the item information. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-23
    * @param obj: The gameobject the mouse is currently hovering on. 
    */ 
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObject = null;
        mouseItem.hoverItem = null;
        mouseItem.canDrop = false;
    }

    /** 
    * The functionality for when the users clicks and starts to drag an item. Stores the information of the gameobject and generates an image
    * wherever the mouse is. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-23
    * @param obj: The gameobject being dragged. 
    */ 
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

    /**
    * Functionality when the user stops dragging an item. Destroy an object if it is not near another inventory slot
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param obj: The gameobject that was dragged. 
    */ 
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
    
    /** 
    * Functuonality when the user is dragging an object. Update the position of the item image with mouse position. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param obj: The item being dragged. 
    */ 
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        resetInventoryItems();

    }

    /**
    * When the user clicks an item toggles the submenu and opens it. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param obj: The item that was clicked. 
    */ 
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

    /** 
    * finding the item slot that an item is in.
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param obj: The item being looked for. 
    * @return: Returns the slot the item is in. 
    */ 
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

    /** 
    * Gets the item's y position to generate a submenu for it. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param i: The current row being looked at.
    * @return: The vector 3 position of the item in the inventory. 
    */ 
    public Vector3 getPositionItems(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_ITEMS, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_ROWS)), 0f);
    }

    /** 
    * Gets the position of the slots when generating them. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    * @param i: The current row being looked at. 
    * @return: The vector 3 position of the inventory slot being generated. 
    */
    public Vector3 getPositionSlots(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_SLOTS, Y_START_SLOTS + (-Y_SPACE_BETWEEN_SLOTS * (i % NUMBER_OF_ROWS)), 0f);
    }

    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void selectSlot1()
    {
        unselectAllSlots();
        invPrefabList[0].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[0].equipItem();
        resetInventoryItems();
    }

    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void selectSlot2()
    {
        unselectAllSlots();
        invPrefabList[1].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[1].equipItem();
        resetInventoryItems();
    }

    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void selectSlot3()
    {
        unselectAllSlots();
        invPrefabList[2].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[2].equipItem();
        resetInventoryItems();
    }

    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void selectSlot4()
    {
        unselectAllSlots();
        invPrefabList[3].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[3].equipItem();
        resetInventoryItems();
    }

    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void selectSlot5()
    {
        unselectAllSlots();
        invPrefabList[4].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[4].equipItem();
        resetInventoryItems();
    }

    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */ 
    public void selectSlot6()
    {
        unselectAllSlots();
        invPrefabList[5].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[5].equipItem();
        resetInventoryItems();
    }
    
    /** 
    * Darken the ui when this slot is selected and equip the item in that slot. Resets everything else
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */
    public void selectSlot7()
    {
        unselectAllSlots();
        invPrefabList[6].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        inventory.Container.Items[6].equipItem();
        resetInventoryItems();
    }
    
    /** 
    * Unselects any selected slot and resets everything. 
    * @author: Yunseo Jeon
    * @since: 2025-05-23
    */
    public void unselectAllSlots()
    {
        for (int i = 0; i < invPrefabList.Length; i++)
        {
            invPrefabList[i].GetComponent<Image>().color = new Color(0, 0, 0, 0.62f);
            inventory.Container.Items[i].unequipItem();
            resetInventoryItems();
        }
    }

    /** 
    * Toggle the flashligh on and off by setting the light gameobject active. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-23
    */ 
    private void toggleLight()
    {
        isFlashlightOn = !isFlashlightOn;

        if (isFlashlightOn)
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }
    }



}

/** 
* Class to store what stuff should be stored when hovering or interacting with an item in the inventory. 
* @author: Yunseo Jeon. 
* @since: 2025-05-23
*/ 
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObject;
    public GameObject prevObj;
    public bool canDrop;
}
