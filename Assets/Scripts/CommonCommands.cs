using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Made By Helge Herrström
 */
public static class CommonCommands
{
    static readonly string sceneMainMenu = "MainMenu";
    static readonly string sceneEndScene = "EndScene";
    /// <summary>
    /// Call this in the minigame scene when the game is done. Score is set automatically
    /// </summary>
    /// <param name="firstPlace">The players that won (Stored int is the players personal number (For Example, Player 1 has 1, P2 2 and so on))</param>
    /// <param name="secondPlace">The players in second place (Stored int is the players personal number (For Example, Player 1 has 1, P2 2 and so on))</param>
    public static void NextGame(List<int> firstPlace, List<int> secondPlace)
    {
        AddPlayersScores(firstPlace, secondPlace);
        firstPlace.Clear();
        secondPlace.Clear();
        DataStorage.GetSetGamesLeft--;
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
                DataStorage.GetSetScore[i] += 3;
            }
        }
        if (secondPlace != null)
        {
            foreach (int i in secondPlace)
            {
                DataStorage.GetSetScore[i] += 2;
            }
        }
    }
    /*
     * Returns true if the players have played through the whole round
     */
    static bool GameDone()
    {
        return DataStorage.GetSetGamesLeft <= 0;
    }
    /*
     * Called when the Game is Done 
     */
    static void LoadEndingScene()
    {
        LoadGameScene(sceneEndScene);
    }
    //Opens the main menu scene
    public static void LoadMainMenu()
    {
        LoadGameScene(sceneMainMenu);
    }
     /// <summary>
     /// Start the Next Game
     /// Loads a random scene that isn't main menu or Ending scene (main menu is 0 and Ending 1 in "Build Settings" at file tab)
     /// How to allow your scene to load with the game: While the scene is open, Press "Add Open Scenes" at File/Build Settings
     /// NOT RECOMMENDED TO CALL.USE NextGame() WHICH KEEPS TRACK OF THE GAME COUNT AND PROCESS SCORES
     /// </summary>
    public static void LoadNextScene()
    {
        //Sort out games that has already been played
        List<string> gamesLeftToPlay = new List<string>();
        // If all games has been played once. Clear the list of already played games 
        if (DataStorage.GetSetPlayableGames.Count == DataStorage.GetSetFilteredGames.Count) 
        {
            DataStorage.GetSetFilteredGames.Clear();
        }
        //Find games that hasn't been played
        foreach (string s in DataStorage.GetSetPlayableGames)
        {
            if (!DataStorage.GetSetFilteredGames.Contains(s))
            {
                gamesLeftToPlay.Add(s);
            }
        }
        string gameToPlay = gamesLeftToPlay[Mathf.FloorToInt(Random.Range(0, gamesLeftToPlay.Count))]; // Pick a random game
        gamesLeftToPlay.Clear(); // Clear list
        DataStorage.GetSetFilteredGames.Add(gameToPlay); // Mark the game as played before starting it
        LoadGameScene(gameToPlay); //Load the game found
    }

    //Loads into the a new scene
    static void LoadGameScene(string scene)
    {
        //Find and Play a Animation between the scenes
        GameObject go = GameObject.FindWithTag("SceneTransitionCanvas");
        if (go != null) //If there's animation to play
        {
            go.GetComponent<SceneTransitionHandler>().DoAnimationIntoScene(scene);
        }
        else // Start the scene without animation
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        }
    }
}
