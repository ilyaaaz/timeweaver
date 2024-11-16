using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRevealManager : MonoBehaviour
{
    public Tilemap[] hiddenTilemaps; // Array to hold all the hidden Tilemaps
    public float fadeDuration = 1.0f; // Optional fade duration

    private void Start()
    {
        // Set all hidden tilemaps active initially
        foreach (Tilemap tilemap in hiddenTilemaps)
        {
            tilemap.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTilemapsActive(false); // Reveal the area by disabling tilemaps
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTilemapsActive(true); // Re-hide the area when player leaves (optional)
        }
    }

    private void SetTilemapsActive(bool isActive)
    {
        foreach (Tilemap tilemap in hiddenTilemaps)
        {
            tilemap.gameObject.SetActive(isActive);
        }
    }
}
