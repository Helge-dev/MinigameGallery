using UnityEngine;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;
/*
* Made By Helge Herrström
*/
public class PlayerJoinManager : MonoBehaviour
{
    string keyboardKeySchemeName = "Keyboard";
    [SerializeField] PlayerColorManager colorManager = null;
    static InputManager keyboardManager = null;
    void OnPlayerJoined(PlayerInput input)
    {
        SetupNewPlayers(input); // When a new controller is connected, Set the name of the controller and other player specific data
    } 
    // Set the name of new players and other related data.
    void SetupNewPlayers(PlayerInput input)
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("GameController");
        // Set the name of new player, (Example names, P1, P2...)
        foreach (GameObject g in go)
        {
            if (g.name == "Controller(Clone)") // If it's a new player
            {
                if (input.controlScheme == keyboardKeySchemeName)
                {
                    g.name = "KeyBoardManager";                    
                    keyboardManager = g.GetComponent<InputManager>();
                    keyboardManager.ListenToKeyboard();
                    SetNewPlayerData(DataStorage.GetSetControllers.Count + 1, InputManager.GetKeyboards[0]);
                }
                else
                {
                    SetNewPlayerData(DataStorage.GetSetControllers.Count + 1, g.GetComponent<InputManager>());
                    g.name = "P" + DataStorage.GetSetControllers.Count + 1; // Set name
                }
                DontDestroyOnLoad(g); // Prevent the new player to be destroyed on future scene loads
            }
        }
    }
    private void Update()
    {
        if (keyboardManager != null)
        {
            InputManager im = keyboardManager.CheckForNewKeyboard();
            if (im != null)
            {
                GameObject[] go = GameObject.FindGameObjectsWithTag("GameController");
                SetNewPlayerData(DataStorage.GetSetControllers.Count + 1, im);
            }
        }

    }
    void SetNewPlayerData(int playerIndex, InputManager inputManager)
    {
        DataStorage.GetSetControllers.Add(playerIndex, inputManager); // Store a reference of controller
        DataStorage.GetSetScore.Add(playerIndex, 0); // Set the players scores
        // Add color to the player
        DataStorage.GetSetPlayerColor.Add(playerIndex, Color.black);
        colorManager.UpdateToNewPlayerImage(playerIndex); // Changes the design in the UI
        colorManager.ChangePlayerColor(playerIndex); 
        Debug.Log("PLAYER " + playerIndex + " JOINED!\n" + inputManager.GetRequiredKeyForMovement() + "\n"+ inputManager.GetRequiredKeyForActions());
    }
}
