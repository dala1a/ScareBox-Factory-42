using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private Camera cam; 
    [SerializeField] private float xSens = 10; 
    [SerializeField] private float ySens = 10; 

    private float xRotation = 0f; 
    
    public void ProcessLook(Vector2 input)
    { 
        float mouseX = input.x;
        float mouseY = input.y; 

        xRotation -= mouseY * Time.deltaTime * ySens; 
        xRotation = Mathf.Clamp(xRotation, -80, 80f);  

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); 
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens); 

    }

}
