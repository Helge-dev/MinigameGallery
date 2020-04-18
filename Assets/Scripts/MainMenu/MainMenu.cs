using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/*
 * Made by Helge Herrström
 */
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] selectableUIMenuObjects = new GameObject[3]; // Selectable UI Components (Buttons, etc)
    [SerializeField] GameObject[] selectableUIOptionsObjects = new GameObject[2]; // Selectable UI Components (Buttons, etc)
    [SerializeField] Text amountOfRoundsText;
    const int amountOfRoundsMaxValue = 10;
    readonly string eightPlayerScenesPath = "/Scenes/EightPlayerGames";
    readonly string fourPlayerScenesPath = "/Scenes/FourPlayerGames";
    bool displayOptions = false;
    PlayerColorManager colorManager;
    private void Start()
    {
        colorManager = GetComponent<PlayerColorManager>();
        HideOptions();
    }
    void Update()
    {
        HandlePlayerInputs();
    }
    void HandlePlayerInputs()
    {
        for (int count = 1; count < DataStorage.GetSetControllers.Count + 1; count++)
        {
            if (DataStorage.GetSetControllers[count].GetButtonEastPressed) //If button is pressed, change the players color
            {
                colorManager.ChangePlayerColor(count);
            }
            if (DataStorage.GetSetControllers[count].GetMovement != Vector3.zero) //If a player attempts to move in the ui, verify the ui is in focus
            {
                VerifyPlayersFocusOnUI();
            }
        }
    }
    //Makes sure player has the ui in focus. If not, set it in focus
    void VerifyPlayersFocusOnUI()
    {
        bool isUIInFocus = false;
        List<GameObject> go = new List<GameObject>();
        go.AddRange(selectableUIMenuObjects);
        go.AddRange(selectableUIOptionsObjects);
        foreach (GameObject g in go)
        {
            if (EventSystem.current.currentSelectedGameObject == g)
            {
                isUIInFocus = true;
                break;
            }
        }
        go.Clear();
        if (!isUIInFocus)
        {
            if (displayOptions) //If options is toggled, put the first item in options on focus
            {
                EventSystem.current.SetSelectedGameObject(selectableUIOptionsObjects[0]);
            } else //If options is not toggled, put the first item in menu on focus
            {
                EventSystem.current.SetSelectedGameObject(selectableUIMenuObjects[0]);
            }
        }
    }
    /*
     * UI Interaction Actions
     */
    //Add one to the amount of games. If above 10 games, set to 1.
    public void ChangeAmountOfGames()
    {
        amountOfRoundsText.text = (1 + ((int.Parse(amountOfRoundsText.text)) % (amountOfRoundsMaxValue))).ToString();
    }
    //Starts the first game
    public void StartGame()
    {
        if (DataStorage.GetSetControllers.Count > 1) //If more than one player is connected
        {
            //Prepare the game loop
            DataStorage.AmountOfGames = DataStorage.GetSetGamesLeft = int.Parse(amountOfRoundsText.text);
            FindPlayableScenes();
            //Load the first game
            CommonCommands.LoadNextScene();
        }
    }
    //Find scenes that can be played with the amount of players connected 
    void FindPlayableScenes()
    {
        FindEightPlayerScenes();
        if (DataStorage.GetSetControllers.Count <= 4) //Find scenes that can be played up to four players
        {
            FindFourPlayerScenes();
        }
    }
    //Find scenes that can be played up to four players and put them into the game loop as a minigame
    void FindFourPlayerScenes() => AddPlayableScenesFromDirectory(fourPlayerScenesPath);
    //Find scenes that can be played up to eight players and put the into the game loop as a minigame
    void FindEightPlayerScenes() => AddPlayableScenesFromDirectory(eightPlayerScenesPath);
    //Add scenes inside a path into playable scenes that may be picked by the game loop as a minigame
    void AddPlayableScenesFromDirectory(string foldierPath)
    {
        foreach (string path in System.IO.Directory.GetFiles(Application.dataPath + foldierPath, "*.unity"))
        {
            string directory = path.Replace(".unity", "").Replace('\\', '/').Replace(Application.dataPath, "").TrimStart('/');
            if (!DataStorage.GetSetPlayableGames.Contains(directory))
            {
                DataStorage.GetSetPlayableGames.Add(directory); //Add the scene (And make it readable for unity)
            }
        }
    }
    //Hide Option Items and show Menu
    public void HideOptions()
    {
        displayOptions = false;
        ShowAndHideGameObjects(selectableUIOptionsObjects, selectableUIMenuObjects);
        EventSystem.current.SetSelectedGameObject(selectableUIMenuObjects[0]);
    }
    //Hide Menu Items and show Options
    public void ShowOptions()
    {
        displayOptions = true;
        ShowAndHideGameObjects(selectableUIMenuObjects, selectableUIOptionsObjects);
        EventSystem.current.SetSelectedGameObject(selectableUIOptionsObjects[0]);

    }
    //Hide and show gameobjects in the game scene
    void ShowAndHideGameObjects(GameObject[] toHide, GameObject[] toShow)
    {
        foreach (GameObject go in toShow)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in toHide)
        {
            go.SetActive(false);
        }
    }
    //Toggles the game into and from fullscreen
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
