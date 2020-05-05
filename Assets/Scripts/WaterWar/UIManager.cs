using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] playerImages = new Image[8];
    const int defaultPlayerImageY = -186,
              defaultPlayerImageSizeY = 75;
    /// <summary>
    /// Call this in Start() for UI to setup.
    /// </summary>
    /// <param name="playerManager"></param>
    public void SetupUI(PlayerSpawnMananger playerManager)
    {
        foreach (int i in playerManager.GetSetPlayers.Keys)
        {
            playerImages[i - 1].color = DataStorage.GetSetPlayerColor[i];
        }
    }
    /// <summary>
    /// Updates ui information
    /// </summary>
    /// <param name="playerManager"></param>
    public void UpdateUI(PlayerSpawnMananger playerManager)
    { 
        foreach (int i in playerManager.GetSetPlayers.Keys)
        {
            playerImages[i - 1].transform.localPosition = new Vector3()
            {
                x = playerImages[i - 1].transform.localPosition.x,
                y = defaultPlayerImageY - defaultPlayerImageSizeY * (1 - (playerManager.GetSetPlayers[i].GetComponent<PlayerBehaviour>().GetWaterMeter / (float)PlayerBehaviour.GetWaterMeterMAX)),
                z = playerImages[i - 1].transform.localPosition.z
            };
        }
    }
}
