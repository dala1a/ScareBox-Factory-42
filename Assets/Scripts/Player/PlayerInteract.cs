using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private Camera cam; 
    [SerializeField] private float distance = 4f; 
    [SerializeField] private LayerMask layerMask; 
    [SerializeField] private PlayerUI playerUI; 
    [SerializeField] private InputManager inputManager; 
    [SerializeField] Mask mask;

    public static RaycastHit hitInfo; 

    public void Update()
    {
        playerUI.updateText(string.Empty);
        mask.showMaskGraphic = false;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                mask.showMaskGraphic = true;
                playerUI.updateText(interactable.promptMessage);

                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }

    }

}
