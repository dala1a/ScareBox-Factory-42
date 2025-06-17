using UnityEngine;

public class Dooropener : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;
    bool isDoorOpen = true;
    bool door = false; 
    float doorTimer = 0;
    float doorTimerEnd = 3f;

    void Update()
    {
        doorTimer += Time.deltaTime;
        if (isDoorOpen == false)
        {
            if (doorTimer >= doorTimerEnd )
            {
                doorAnimator.Play("CloseDoor", 0, 0.0f);
                isDoorOpen = true;
                door = false;
            }
        }
    }

    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && door == false)
        {
            doorTimer = 0;
            doorAnimator.Play("OpenDoor", 0, 0.0f);
            door = true; 
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && door == true)
        {
            isDoorOpen = false; 
        }
    }
}
