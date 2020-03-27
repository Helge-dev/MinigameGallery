/*
 *  Contains data that all scenes share
 */
public static class DataStorage
{
    const int maxPlayerCount = 6; //Max amount of players (Never changes)
    static int amountOfGames = 0, //How many games is one round
               gamesLeft = 0; //Amount of games to play through
    static int[] scores = new int[maxPlayerCount]; //Stores the players scores. The index is the players ID
    public static int GamesLeft
    {
        get => gamesLeft;
        set => gamesLeft = value;
    }
    public static int AmountOfGames
    {
        get => amountOfGames;
        set => amountOfGames = value;
    }
    public static int[] Scores
    {
        get => scores;
        set => scores = value;
    }
}
