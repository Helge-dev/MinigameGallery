/*
 * Made By Helge Herrström
 * Contains data that all scenes share
 */
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{
    /*
     * Variables
     */
    static int amountOfGames = 0, //How many games is one round
               gamesLeft = 0; //Games left to play
    static SortedDictionary<int, int> scores = new SortedDictionary<int, int>(); //Stores the players scores. The key is the players personal number (For Example, Player 1 has 1, P2 2 and so on)
    static SortedDictionary<int, Color> playerColors = new SortedDictionary<int, Color>(); //Stores the players color. The key is the players personal number (For Example, Player 1 has 1, P2 2 and so on)
    static SortedDictionary<int, InputManager> controllers = new SortedDictionary<int, InputManager>(); //Stores player controllers, the index is the player priority (ex, player 1, player 2)
    static List<string> playableGames = new List<string>(); // strings of scenes playable (some games can't be played with more than 4 playerss)
    static List<string> filteredGames = new List<string>(); // strings of scenes not to load
    /*
     * Properties
     */
    //Returns how many games are left to play
    public static int GetSetGamesLeft
    {
        get => gamesLeft;
        set => gamesLeft = value;
    }
    //Returns how many games to have played
    public static int AmountOfGames
    {
        get => amountOfGames;
        set => amountOfGames = value;
    }
    //Returns player scores
    public static SortedDictionary<int, int> GetSetScore
    {
        get => scores;
        set => scores = value;
    }
    //Returns player colors
    public static SortedDictionary<int, Color> GetSetPlayerColor
    {
        get => playerColors;
        set => playerColors = value;
    }
    //Returns player controllers
    public static SortedDictionary<int, InputManager> GetSetControllers
    {
        get => controllers;
        set => controllers = value;
    }
    //Returns a list of scene names that can be played with the amount of players connected
    public static List<string> GetSetPlayableGames
    {
        get => playableGames;
        set => playableGames = value;
    }
    //Returns a list of scenes that has been played
    public static List<string> GetSetFilteredGames
    {
        get => filteredGames;
        set => filteredGames = value;
    }
}
