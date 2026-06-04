using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralTexture
{
    public static Color32[] CreatePalette(int numColors)
    {
        Color32[] palette = new Color32[numColors];
        for (int i = 0; i < numColors; i++)
        {
            Color32 color = new Color32((byte)Random.Range(0, 255), // r
                                        (byte)Random.Range(0, 255), // g
                                        (byte)Random.Range(0, 255), // b
                                        255); // a
            palette[i] = color;
        }
        return palette;
    }

    public static Texture2D CreateTexture()
    {
        Color32[] palette = CreatePalette(3);
        Debug.Log($"Color palette sample: {palette[0].r},{palette[0].g},{palette[0].b}   {palette[1].r},{palette[1].g},{palette[1].b}");

        Texture2D texture = new Texture2D(16, 16); // size x y
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, palette[Random.Range(0, palette.Length)]);
            }
        }

        texture.Apply();
        return texture;
    }
}
