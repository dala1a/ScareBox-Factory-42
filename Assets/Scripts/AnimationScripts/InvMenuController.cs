using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;

/**
* Inventory animations and submenu controllers. 
* @author: Yunseo Jeon
* @since: 2025-05-15
*/
public class InvMenuController : MonoBehaviour
{   
    [SerializeField] private Animator menu; // Menu Animator Controller
    [SerializeField] private GameObject subMenu; // Reference to the submenu
    [SerializeField] private GameObject deleteObj; // Reference to the deleteButton
    [SerializeField] private GameObject inspectObj; // Reference to the inspectButton
    [SerializeField] private bool menuTrigger = false; 
    [SerializeField] Canvas inspectCanvas; // Reference to the inspect ui folder. 
    [SerializeField] GameObject inspectPrefab; // Reference to the inspect menu

    private InventorySlot currentItem;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int Y_START;

    /**
    * Start() runs once when the script loads in
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    */ 
    void Start()
    {
        AddEvent(deleteObj, EventTriggerType.PointerClick, delegate { OnClickDelete(); });
        AddEvent(inspectObj, EventTriggerType.PointerClick, delegate { OnClickInspect(); });

    }

    /**
    * Adds a unity mouse event to a gameobject. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @param obj: The gameobject the event is being assigned to. 
    * @param type: The type of event being assigned (Mouse click, Mouse Drag). 
    * @param action: The outcome to the event. 
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
    * Open and closes the item submenu and plays the popup animation. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @param i: The y position of the item being accessed used to figure out where to place the animation. 
    * @param item: The item the menu is being shown for. 
    */
    public void toggleMenu(int i, InventorySlot item)
    {
        menuTrigger = !menuTrigger;
        playAnim(i, item);
    }

    /**
    * Plays the animation for opening and closing item submenu. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @param i: The y position of the item being accessed used to figure out where to place the animation. 
    * @param item: The item the menu is being shown for. 
    */
    public void playAnim(int i, InventorySlot item)
    {
        if (menuTrigger)
        {
            menu.Play("OpenMenu", 0, 0.0f);
            currentItem = item;
            subMenu.GetComponent<RectTransform>().localPosition = getPosition(i);
        }
        else if (!menuTrigger)
        {
            menu.Play("CloseMenu", 0, 0.0f);
            subMenu.GetComponent<RectTransform>().localPosition = getPosition(i);
        }
    }

    /** 
    * Gets whether the menu is open or not. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @return bool: Returns true if the menu is open, false otherwise.  
    *
    */ 
    public bool getMenuState()
    {
        return menuTrigger;
    }

    /** 
    * Deletes the item from the inventory. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    */ 
    public void OnClickDelete()
    {

    }

    /**
    * Opens up the inspect menu and displays the information about the items. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-15
    */ 
    public void OnClickInspect()
    {
        if (inspectCanvas.transform.childCount != 0)
        {
            exit(inspectCanvas.transform.GetChild(0).gameObject);
        }
        var obj = Instantiate(inspectPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(inspectCanvas.transform, false);
        obj.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = currentItem.item.description;
        obj.transform.GetChild(1).GetComponentInChildren<Image>().sprite = currentItem.item.uiDisplay;
        obj.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = currentItem.item.name;

        var exitObj = obj.transform.GetChild(3).gameObject;
        AddEvent(exitObj, EventTriggerType.PointerClick, delegate { exit(obj); });

    }

    /**
    * Destroys the gameobject when exiting a menu. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    */ 
    public void exit(GameObject obj)
    {
        Destroy(obj);
    }


    /** 
    * Gets the y position of the item in the inventory slot. 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @return Vector3: The position of the item. 
    *
    */ 
    public Vector3 getPosition(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_ITEMS, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i % 7)), 0f);
    }

    /** 
    * Gets the delete game object
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @return GameObject: The delete gameobject. 
    *
    */ 
    public GameObject getdeleteobject()
    {
        return deleteObj;
    }

    /** 
    * Gets the inspect game object 
    * @author: Yunseo Jeon
    * @since: 2025-05-15
    * @return GameObject: The Inspect gameobject. 
    */ 
    public GameObject getInspectObject()
    {
        return inspectObj;
    }

}
