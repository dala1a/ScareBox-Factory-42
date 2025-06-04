
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    [SerializeField] Animator leverAnimator;
    private bool toggleLever = false; 

    public void pickUpItem()
    {
        // Debug.Log("INTERACTED");
        var hitInfo = PlayerInteract.hitInfo;
        InteractableItems interactable = hitInfo.collider.GetComponent<InteractableItems>();
        inventory.addItem(interactable.item, 1);
        Destroy(hitInfo.transform.gameObject);
    }

    public void useLever()
    {
        Debug.Log("LEVER BNEING USED");
        toggleLever = !toggleLever;
        playLeverAnimation(); 
    }

    private void playLeverAnimation()
    {
        if (toggleLever == true)
        {
            leverAnimator.Play("LeverClose", 0, 0.0f);
        }
        else
        { 
            leverAnimator.Play("LeverOpen", 0, 0.0f);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[7];
    }

}