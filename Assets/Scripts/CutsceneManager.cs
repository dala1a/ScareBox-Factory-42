using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

/** 
* Class to manage initial cutscene behavior
* @author: Yunseo Jeon
* @since 2025-05-29
*/ 
public class CutsceneManger : MonoBehaviour
{

    [SerializeField] private GameObject cutscenePlayer; // Reference to the cutscene player. 
    [SerializeField] private Camera cam; // Reference to the player camera. 
    public double time = 50.08; // The time it takes for the cutscene to end. 
    public double currentTime; // Current time 


    /** 
    * End the cutscene when it reaches the end by checking the time constantly (Yes couldn't find a better way :( )
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */ 
    void Update()
    {
        currentTime = cutscenePlayer.GetComponent<VideoPlayer>().time; // get the time of the cutscene video 

        // If either time runs out or the user pressed escape end cutscene
        if (currentTime >= time || Input.GetKeyDown(KeyCode.Escape))
        {
            endCutscene();
        }
    }

    /** 
    * Switch scene from the cutscene scene to the main game scene. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */ 
    void endCutscene()
    {
        SceneManager.LoadScene("GameScene");
    }

}