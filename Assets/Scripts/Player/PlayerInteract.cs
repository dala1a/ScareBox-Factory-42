using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 
* Contains behavior on filtering out objects with raycasting and updating player ui to pickup items.
* @author: Oliver Thompson
* @since: 2025-05-29
*/
public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private Camera cam; // Reference to the player camera
    [SerializeField] private float distance = 4f; // The max distance you can pick an object up from
    [SerializeField] private LayerMask layerMask; // Reference to the layer mask
    [SerializeField] private PlayerUI playerUI; // Reference to the Player UI
    [SerializeField] private InputManager inputManager; // Reference to the Unity Input Manager
    [SerializeField] Mask mask; // Reference to the

    public static RaycastHit hitInfo; // Initialize Raycast

    public void Update()
    {
        playerUI.updateText(string.Empty); // Update player ui to show nothing
        mask.showMaskGraphic = false; // Dont show the cursor.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); // Generate a raycast in the direction the player is looking
        Debug.DrawRay(ray.origin, ray.direction * distance); // Draw the raycast for debug information
        
        // Check raycast and filter out the objects
        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {   
            // If the object has the interactables tag detect it.,
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>(); // Get the Interactable Script from the object
                mask.showMaskGraphic = true; // Show the cursor
                playerUI.updateText(interactable.promptMessage); // Update the player ui prompmessage.

                // If the player uses the interact keybind trigger the event.
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }

    }

}
