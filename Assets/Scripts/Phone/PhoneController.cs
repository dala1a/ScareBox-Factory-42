using System;
using System.Collections.Generic;
using UnityEngine;


public class PhoneController : MonoBehaviour
{

    public GameObject HomeScreen;
    public List<GameObject> apps;

    void Start()
    {
        homeScreen();
    }

    public void homeScreen()
    {
        for (int i = 0; i < apps.Count; i++)
        {
            apps[i].SetActive(false);
        }
        HomeScreen.SetActive(true);
    }

    public void openApp(GameObject app)
    {
        if (app.tag == "Map")
        {   
            switchApps($"{app.tag}Canvas");
        }
    }

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
