using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] playerImages = new Image[8];
    [SerializeField] Text playerOutText = null,
                          playerOutTextBackground = null;
    const int defaultPlayerImageY = -186,
              defaultPlayerImageSizeY = 75;
    const float playerTextDisplayDuration = 1.5f;
    float playerTextDisplayTimer = 0;
    bool textFadingIn = true;
    static int displayedPlayerOut = 0;
    static public int SetDisplayedPlayerOut{ set => displayedPlayerOut = value; }
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
        if (playerOutText.gameObject.activeInHierarchy)
        {
            if (textFadingIn)
                playerTextDisplayTimer += Time.deltaTime;
            else
                playerTextDisplayTimer -= Time.deltaTime;

            playerOutText.color = new Color(playerOutText.color.r, playerOutText.color.g, playerOutText.color.b, playerTextDisplayTimer/playerTextDisplayDuration);
            playerOutTextBackground.color = new Color(playerOutTextBackground.color.r, playerOutTextBackground.color.g, playerOutTextBackground.color.b, playerTextDisplayTimer / playerTextDisplayDuration);
            if (playerTextDisplayTimer > playerTextDisplayDuration)
            {
                textFadingIn = false;
            } else if (playerTextDisplayTimer < 0) // If the text has faded out
            {
                playerOutText.gameObject.SetActive(false); // Disable text
                playerOutTextBackground.gameObject.SetActive(false);
            }
        }
        if (displayedPlayerOut != 0)
        {
            AnnouncePlayerOut(displayedPlayerOut);
            displayedPlayerOut = 0;
        }
    }
    void AnnouncePlayerOut(int playerID)
    {
        playerTextDisplayTimer = 0;
        playerOutText.text = "Player " + playerID + " Out!";
        playerOutTextBackground.text = "Player " + playerID + " Out!";
        playerOutText.color = new Color(DataStorage.GetSetPlayerColor[playerID].r, DataStorage.GetSetPlayerColor[playerID].g, DataStorage.GetSetPlayerColor[playerID].b, 0);
        playerOutText.gameObject.SetActive(true);
        playerOutTextBackground.gameObject.SetActive(true);
        textFadingIn = true;
    }
}
