using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairTexture;
    [SerializeField] private Color crosshairColor = Color.white;
    [SerializeField] private Vector2 crosshairSize = new Vector2(32, 32);
    [SerializeField] private bool hideCursor = true;

    private void Start()
    {
        if (hideCursor)
        {
            Cursor.visible = false;
        }

        // If no custom texture assigned, create a simple crosshair cursor
        if (crosshairTexture == null)
        {
            crosshairTexture = CreateDefaultCrosshair();
        }
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.Repaint)
        {
            Vector2 mousePosition = Event.current.mousePosition;
            Rect position = new Rect(
                mousePosition.x - crosshairSize.x / 2,
                mousePosition.y - crosshairSize.y / 2,
                crosshairSize.x,
                crosshairSize.y
            );

            GUI.color = crosshairColor;
            GUI.DrawTexture(position, crosshairTexture);
        }
    }

    private Texture2D CreateDefaultCrosshair()
    {
        int width = 32;
        int height = 32;
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Color[] colors = new Color[width * height];

        // Fill with transparent pixels
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }

        // Draw crosshair lines
        for (int i = 0; i < width; i++)
        {
            // Horizontal line
            if (i >= width / 3 && i <= width * 2 / 3)
            {
                colors[height / 2 * width + i] = Color.white;
            }
            // Vertical line
            if (i >= height / 3 && i <= height * 2 / 3)
            {
                colors[i * width + width / 2] = Color.white;
            }
        }

        texture.SetPixels(colors);
        texture.Apply();
        return texture;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}