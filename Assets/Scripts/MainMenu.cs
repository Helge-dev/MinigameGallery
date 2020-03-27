using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InputField amountOfRoundInputField;
    void SetAmountOfGames()
    {
        DataStorage.AmountOfGames = DataStorage.GamesLeft = int.Parse(amountOfRoundInputField.text);
    }
    public void StartGame()
    {
        SetAmountOfGames();
        CommonCommands.LoadNextScene();
    }
}
