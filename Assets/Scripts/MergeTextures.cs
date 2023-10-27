using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTextures : MonoBehaviour
{
    [SerializeField] Texture2D[] textures; // Array of textures to merge
    [SerializeField] Material material;

    private void Start()
    {
        int width = textures[0].width;
        int height = textures[0].height;

        // Create a new texture to hold the merged result
        Texture2D mergedTexture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color mergedColor = Color.black; // Initialize with a default color

                // Merge colors from all textures (you can implement your own merging logic)
                foreach (Texture2D texture in textures)
                {
                    Color color = texture.GetPixel(i, j);
                    mergedColor += color;
                }

                // Average the merged color
                mergedColor /= textures.Length;

                mergedTexture.SetPixel(i, j, mergedColor);
            }
        }

        // Apply changes to the merged texture
        mergedTexture.Apply();

        // Assign the merged texture to the material's main texture
        material.mainTexture = mergedTexture;
    }
}
