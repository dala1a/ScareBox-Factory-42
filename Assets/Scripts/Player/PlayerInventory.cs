
using UnityEngine;


/** 
* Contains the behaviors for picking up items and storing them in the inventory
* @author: Oliver Thompson
* @since: 2025-05-30
*/
public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory; // Reference to the player inventory

    /** 
    * Filter items based on the raycast and store interactable items in your inventory.
    * @author: Oliver Thompson
    * @since: 2025-05-30
    */
    public void pickUpItem()
    {
        var hitInfo = PlayerInteract.hitInfo;
        InteractableItems interactable = hitInfo.collider.GetComponent<InteractableItems>();
        inventory.addItem(interactable.item, 1);
        Destroy(hitInfo.transform.gameObject);
    }

    /** 
    * When the game gets reset empty out the inventory.
    * @author: Oliver Thompson
    * @since: 2025-05-30
    */
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[7];
    }

}