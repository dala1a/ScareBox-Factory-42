using UnityEngine;

public class mapLight : MonoBehaviour {

    public GameObject miniCamLight;

    void OnPreCull()
    {
        if (miniCamLight!= null)             miniCamLight.SetActive(false);
    }

    private void OnPreRender()
    {
        miniCamLight.SetActive(true);
    }

    private void OnPostRender()
    {
        miniCamLight.SetActive(false);
    }
}