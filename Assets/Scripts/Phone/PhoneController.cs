using System;
using System.Collections.Generic;
using UnityEngine;

/** 
* Class containing all the behaviors for the phone item. 
* @author: Oliver Thompson
* @since: 2025-05-27
*/ 
public class PhoneController : MonoBehaviour
{

    public GameObject HomeScreen; // Refernence to home screen. 
    public List<GameObject> apps; // List of phone apps. 

    /**
     * show the homescreen on startup
     * @author: Oliver Thompson
     * @since: 2025-06-01
     */
    void Start()
    {
        homeScreen();
    }

    /**
     * Enables and disables gameobjects to display the proper homescreen. 
     * @author: Oliver Thompson
     * @since: 2025-06-01
     */
    public void homeScreen()
    {
        for (int i = 0; i < apps.Count; i++)
        {
            apps[i].SetActive(false);
        }
        HomeScreen.SetActive(true);
    }

    
    /**
     * Opens an app from the phone object. 
     * @author: Oliver Thompson
     * @since: 2025-06-01
     * @param app: Reference to the App being openened
     */
    public void openApp(GameObject app)
    {   
        if (app.tag == "Map")
        {   
            switchApps($"{app.tag}Canvas");
        }
    }

    /**
     * Switch apps by changing the state of the gameobjects (multiple) 
     * @author: Oliver Thompson
     * @since: 2025-06-01
     * @param tag: The tag of the app (MapApp, PhoneApp). 
     */
    private void switchApps(String tag)
    {
        for (int i = 0; i < apps.Count; i++)
        {
            if (apps[i].tag == tag)
            {
                apps[i].SetActive(true);
            }
        }
        HomeScreen.SetActive(false);
    }

}
