using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField]GameObject PlayerBody;
    [SerializeField]GameObject PlayerCamera;
    public float lookSensitivity = 1.0f;
    private float minXRotation = -55.0f; // Looking up limit
    private float maxXRotation = 55.0f;  // Looking down limit
    private float CurrRotx = 0f;
    private float CurrRoty = 0f;
   
    void OnLook(InputValue RotInput)
    {   

        Vector2 inputRot = RotInput.Get<Vector2>() * lookSensitivity;

        transform.Rotate(Vector3.up, inputRot.x);
        transform.Rotate(Vector3.left, inputRot.y);

        CurrRotx -= inputRot.y;
        CurrRoty += inputRot.x;

        CurrRotx = Mathf.Clamp(CurrRotx, minXRotation, maxXRotation);     

        PlayerCamera.transform.rotation = Quaternion.Euler(CurrRotx , CurrRoty , 0);

        PlayerBody.transform.rotation = Quaternion.Euler(0, CurrRoty, 0);
    }
}
