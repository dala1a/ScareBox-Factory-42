using System;
using TMPro;
using UnityEngine;

/** 
* Timer for the phone. 
* @author: Oliver Thompson
* @since: 2025-05-27
*/ 
public class Timer : MonoBehaviour
{
    TextMeshProUGUI text; // Reference to the textMesh that displays the time.
    [SerializeField] bool formatOn = false; // To change the format of the time.


    /** 
    * On startup retrieve the TextMesh from the gameobject. 
    * @author: Oliver Thompson
    * @since: 2025-05-27
    */ 
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    /** 
    * Display the date and time, Update it constantly 
    * @author: Oliver Thompson
    * @since: 2025-05-27
    */ 
    void Update()
    {
        String timeString;

        if (!formatOn)
        {
            if (DateTime.Now.Minute < 10)
            {
                timeString = $"{DateTime.Now.Hour} : 0{DateTime.Now.Minute}";
            }
            else
            {
                timeString = $"{DateTime.Now.Hour} : {DateTime.Now.Minute}";
            }
        }
        else
        {
            timeString = DateTime.Now.ToString();
        }

        text.text = timeString;
    }
}
