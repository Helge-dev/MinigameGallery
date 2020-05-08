using UnityEngine;

public class ColorCustomizer : MonoBehaviour
{
    [SerializeField] Renderer[] teamColorRenderers = null;
    [SerializeField] Renderer[] playerColorRenderers = null;

    /// <summary>
    /// Add a team color to the player
    /// </summary>
    /// <param name="color"></param>
    public void SetTeamColor(Color color) => SetColorToRenderers(color, teamColorRenderers);
    /// <summary>
    /// Add a player color to the player
    /// </summary>
    /// <param name="color"></param>
    public void SetPlayerColor(Color color) => SetColorToRenderers(color, playerColorRenderers);
    void SetColorToRenderers(Color color, Renderer[] renderers)
    {
        foreach (Renderer r in renderers)
            r.material.color = color;
    }
}
