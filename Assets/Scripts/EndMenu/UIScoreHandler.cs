using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreHandler : MonoBehaviour
{
    [SerializeField] Text[] scoreFields = new Text[8];
    void Start()
    {
        AddPlayerScoresToText();
        ResetGameData();
    }
    //Adds all the players scores to the text in UI 
    void AddPlayerScoresToText()
    {
        for (int count = 0; count < scoreFields.Length; count++)
        {
            if (DataStorage.GetSetControllers.Count - 1 < count)
            {
                scoreFields[count].text = "";
            }
            else
            {
                scoreFields[count].text += DataStorage.GetSetScore[count + 1];
            }
        }
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
