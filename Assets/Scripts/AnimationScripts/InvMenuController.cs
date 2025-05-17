using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InvMenuController : MonoBehaviour
{
    [SerializeField] private Animator menu;
    [SerializeField] private GameObject subMenu;
    [SerializeField] private GameObject deleteObj;
    [SerializeField] private GameObject inspectObj;
    [SerializeField] private bool menuTrigger = false;
    [SerializeField] Canvas inspectCanvas;
    [SerializeField] GameObject inspectPrefab;

    private InventorySlot currentItem;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int Y_START;

    void Start()
    {
        AddEvent(deleteObj, EventTriggerType.PointerClick, delegate { OnClickDelete(); });
        AddEvent(inspectObj, EventTriggerType.PointerClick, delegate { OnClickInspect(); });

    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void toggleMenu(int i, InventorySlot item)
    {
        menuTrigger = !menuTrigger;
        playAnim(i, item);
    }

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

    public bool getMenuState()
    {
        return menuTrigger;
    }

    public void OnClickDelete()
    {

    }

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

    public void exit(GameObject obj) {
        Destroy(obj);
    }



    public Vector3 getPosition(int i)
    {
        return new Vector3(X_SPACE_BETWEEN_ITEMS, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i % 7)), 0f);
    }

    public GameObject getdeleteobject()
    {
        return deleteObj;
    }

    public GameObject getInspectObject()
    {
        return inspectObj;
    }

}
