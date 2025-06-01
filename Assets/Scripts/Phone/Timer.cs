using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        String timeString; 

        if (DateTime.Now.Minute < 10)
        {
            timeString = $"{DateTime.Now.Hour} : 0{DateTime.Now.Minute}";
        }
        else
        { 
            timeString = $"{DateTime.Now.Hour} : {DateTime.Now.Minute}";
        }
        
        text.text = timeString; 
    }
}
