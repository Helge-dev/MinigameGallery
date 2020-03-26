using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
     * Call this method to start the game. Will store all settings made by the player into the "data storage" class and load a new scene.
     */
    public void StartGame()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartRandomMinigameScene();
        }
    }
    /*
     * Loads a random scene that isn't main menu (main menu is 0 in "Build Settings" at file tab)
     * How to allow your scene to load with the game. While the scene is open, Press "Add Open Scenes" at File/Build Settings
     */
    void StartRandomMinigameScene()
    {
        SceneManager.LoadSceneAsync(1 + (int)Random.value * (SceneManager.sceneCountInBuildSettings - 1), LoadSceneMode.Single);
    }
}
