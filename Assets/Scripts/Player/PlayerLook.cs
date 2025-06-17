using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** 
* Contains the behavior for player camera movement.
* @author: Yunseo Jeon
* @since: 2025-05-25
*/
public class PlayerLook : MonoBehaviour
{

    [SerializeField] private Camera cam; // Reference the to player camera

    // Cam sensitivity
    [SerializeField] private float xSens = 10;
    [SerializeField] private float ySens = 10;
    
    private float xRotation = 0f;
    private bool locked = true;

    
    /** 
    * Lock the cursor to the game screen on startup.
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        locked = true;

    }

    
    /** 
    * Toggle the cursor lock
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void ToggleLock()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.None;
            locked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            locked = true;
        }
    }

    
    /** 
    * Controls the camera movement based on mouse input
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    * @param input: The 2d vector derived from mouse movement.
    */
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= mouseY * Time.deltaTime * ySens;
        xRotation = Mathf.Clamp(xRotation, -80, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens);

    }

    
    /** 
    * Get the player camera
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    * @return: Player Camera
    */
    public Camera GetCamera()
    {
        return cam;
    }

}
