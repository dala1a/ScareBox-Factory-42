using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManger : MonoBehaviour
{

    [SerializeField] private GameObject cutscenePlayer;
    [SerializeField] private Camera cam;
    public double time = 50.08;
    public double currentTime;


    void Update () {

        

            currentTime = cutscenePlayer.GetComponent<VideoPlayer> ().time;
            if (currentTime >= time || Input.GetKeyDown(KeyCode.Escape)) {
                endCutscene(); 
            }
    }

    void endCutscene()
    {
        SceneManager.LoadScene("GameScene");
        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex); 
    }

}