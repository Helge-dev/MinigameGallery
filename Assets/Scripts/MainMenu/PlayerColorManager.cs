using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorManager : MonoBehaviour
{
    Color[] availableColors = { Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow, Color.black, Color.white}; // Colors possible for players to use
    [SerializeField] Image[] images = new Image[8]; // Player images in the user interface
    void Start()
    {
        //Sets the images color at the start of game (If any players already registered)
        for (int count = 0; count < DataStorage.GetSetControllers.Count; count++)
        {
            images[count].color = DataStorage.GetSetPlayerColor[count + 1];
        }
    }
    //Changes a players color to a random color
    public void ChangePlayerColor(int playerIndex)
    {
        Color color = GetRandomColorNotUsed(playerIndex); // Get a random color not in use
        DataStorage.GetSetPlayerColor[playerIndex] = color;
        images[playerIndex-1].color = color;
    }
    //Returns a random color that is not used by other players
    Color GetRandomColorNotUsed(int playerToChangeIndex)
    {
        bool colorFound = false;
        Color color = Color.white;
        while (!colorFound)
        {
            //Make color a random Color from the avilableColors array
            color = availableColors[Mathf.FloorToInt(Random.Range(0, availableColors.Length))];
            //colorFound is set to true if other players doesn't have the color
            bool colorExists = false;
            foreach (Color c in DataStorage.GetSetPlayerColor.Values)
            {
                if (c == color)
                {
                    colorExists = true;
                    break;
                }
            }
            if (!colorExists)
            {
                colorFound = true;
            }
        }
        return color;
    }
    //Adjust text in the image in the UI of player with the playerIndex
    public void UpdateToNewPlayerImage(int playerIndex)
    {
        Text[] text = images[playerIndex - 1].GetComponentsInChildren<Text>();
        foreach (Text t in text)
        {
            if (t.name == "Text (Join)")
            {
                t.text = ""; //Remove text
            } else if (t.name == "Text (Player)")
            {
                t.color = Color.white; //Change text color
            }
        }
    }
}
