
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void pickUpItem()
    {
        var hitInfo = PlayerInteract.hitInfo;
        InteractableItems interactable = hitInfo.collider.GetComponent<InteractableItems>();
        inventory.addItem(interactable.item, 1);
        Destroy(hitInfo.transform.gameObject);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[7];
    }

}