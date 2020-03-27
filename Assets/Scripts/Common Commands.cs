using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CommonCommands
{
    /*
     * Call this in the minigame scene when the game is done
     * firstPlace - The players that came first (Index is the players ID)
     * secondPlace - The players that came second (Index is the players ID)
     */
    public static void NextGame(List<int> firstPlace, List<int> secondPlace)
    {
        AddPlayersScores(firstPlace, secondPlace);
        DataStorage.GamesLeft -= 1;
        if (GameDone())
        {
            LoadEndingScene();
        }
        else
        {
            LoadNextScene();
        }
    }
    /*
     * Add the relevant scores to the players depending on their placing in the game
     */
    static void AddPlayersScores(List<int> firstPlace, List<int> secondPlace)
    {
        if (firstPlace != null)
        {
            foreach (int i in firstPlace)
            {
                DataStorage.Scores[i] += 3;
            }
        }
        if (secondPlace != null)
        {
            foreach (int i in secondPlace)
            {
                DataStorage.Scores[i] += 2;
            }
        }
    }
    /*
     * Returns true if the players have played through the whole round
     */
    static bool GameDone()
    {
        return DataStorage.GamesLeft <= 0;
    }
    /*
     * Called when the Game is Done 
     */
    static void LoadEndingScene()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    /*
     * Start the Next Game
     * Loads a random scene that isn't main menu or Ending scene (main menu is 0 and Ending 1 in "Build Settings" at file tab)
     * How to allow your scene to load with the game: While the scene is open, Press "Add Open Scenes" at File/Build Settings
     * NOT RECOMMENDED TO CALL. USE NextGame() WHICH KEEPS TRACK OF THE GAME COUNT AND PROCESS SCORES
     */
    public static void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(2 + (int)Random.value * (SceneManager.sceneCountInBuildSettings - 2), LoadSceneMode.Single);
    }
    /*
     * Go back to Main Menu
     */
    static void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
