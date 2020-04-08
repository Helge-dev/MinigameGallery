/*
 * Made By Helge Herrström
 * Contains data that all scenes share
 */
using System.Collections.Generic;
public static class DataStorage
{
    /*
     * Variables
     */
    static int amountOfGames = 0, //How many games is one round
               gamesLeft = 0; //Amount of games to play through
    static SortedDictionary<string,int> scores = new SortedDictionary<string,int>(); //Stores the players scores. The key is the players name (For Example, P1, P2...)
    /*
     * Properties
     */
    //Returns how many games are left to play
    public static int GamesLeft
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
    public static SortedDictionary<string,int> GetSetScore
    {
        get => scores;
        set => scores = value;
    }
}
