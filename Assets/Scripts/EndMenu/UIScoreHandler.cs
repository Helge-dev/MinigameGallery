using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreHandler : MonoBehaviour
{
    [SerializeField] Text[] scoreFields = new Text[8];
    const string firstPlace = "1st",
                secondPlace = "2nd",
                thirdPlace = "3rd",
                fourthPlace = "4th",
                fifthPlace = "5th",
                sixthPlace = "6th",
                seventhPlace = "7th",
                eightPlace = "8th";

    const int textYPadding = 20;
    const int defaultY = 120;
    void Start()
    {
        EditPlayerScoresText();
        ResetGameData();
    }
    //Adds all the players scores to the text in UI 
    void EditPlayerScoresText()
    {
        List<int> sortedScores = GetDifferentScoresSorted();
        int place = 0; //Keeps track of which position the player is in (first place, second...)
        int scoreTextPlaced = 0; //Keeps track of all the score Text placed
        //Edit text in UI
        foreach (int score in sortedScores) //Foreach score (score is sorted)
        {
            place++; // Keep track of what place the player is in (first, second...)
            string textToPlace = GetScoreTextFromPosition(place); // Get the string to use
            foreach (int player in DataStorage.GetSetScore.Keys) //Foreach player
            {
                if (DataStorage.GetSetScore[player] == score)
                {
                    //Add the players score to the text field
                    scoreFields[player - 1].text = textToPlace + " " + DataStorage.GetSetControllers[player].name + "       " + score; //Add text to label
                    scoreFields[player - 1].transform.localPosition = new Vector3(0,defaultY - (scoreFields[player - 1].preferredHeight)*scoreTextPlaced); //Move label
                    scoreTextPlaced++;
                } 
            }
        }
        //Remove unused text
        for (int count = DataStorage.GetSetControllers.Count; count < scoreFields.Length; count++ )
        {
            scoreFields[count].gameObject.SetActive(false);
        }
    }
    string GetScoreTextFromPosition(int position)
    {
        switch (position)
        {
            case 1:
                return firstPlace;
            case 2:
                return secondPlace;
            case 3:
                return thirdPlace;
            case 4:
                return fourthPlace;
            case 5:
                return fifthPlace;
            case 6:
                return sixthPlace;
            case 7:
                return seventhPlace;
            case 8:
                return eightPlace;
            default:
                return "";
        }
    }
    //Returns scores sorted. No duplicates of scores.
    List<int> GetDifferentScoresSorted()
    {
        List<int> scores = new List<int>();
        foreach (int score in DataStorage.GetSetScore.Values)
        {
            if (!scores.Contains(score))
            {
                scores.Add(score);
            }
        }
        scores.Sort();
        scores.Reverse();
        return scores;
    }
    void ResetGameData()
    {
        //Reset Player Scores
        foreach (int i in DataStorage.GetSetControllers.Keys)
        {
            DataStorage.GetSetScore[i] = 0;
        }
        //Reset progress data
        DataStorage.GetSetFilteredGames.Clear();
    }
    void Update()
    {
        foreach (InputManager im in DataStorage.GetSetControllers.Values)
        {
            if (im.GetButtonStartPressed)
            {
                CommonCommands.LoadMainMenu();
            }
        }
    }
}
