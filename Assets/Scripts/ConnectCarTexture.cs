using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCarTexture : MonoBehaviour
{
    [SerializeField] private Texture2D textureCabin;
    [SerializeField] private Texture2D textureSides;
    [SerializeField] private Texture2D textureFront;
    [SerializeField] private Texture2D textureBack;
    
    void Start()
    {
        // Crear una nueva textura de destino de 2048x2048.
        Texture2D combinedTexture = new Texture2D(2048, 2048);

        // Iterar a través de cada píxel de las texturas individuales y copiarlos a la textura de destino.
        for (int y = 0; y < 1024; y++)
        {
            for (int x = 0; x < 1024; x++)
            {
                // Calcula las coordenadas en la textura de destino.
                int destX = x;
                int destY = y;
        
                // Copia el píxel de las texturas individuales a la textura de destino.
                combinedTexture.SetPixel(destX, destY, textureCabin.GetPixel(x, y));
                combinedTexture.SetPixel(destX + 1024, destY, textureSides.GetPixel(x, y));
                combinedTexture.SetPixel(destX, destY + 1024, textureFront.GetPixel(x, y));
                combinedTexture.SetPixel(destX + 1024, destY + 1024, textureBack.GetPixel(x, y));
            }
        }

        // Aplica los cambios a la textura de destino.
        combinedTexture.Apply();

        // Asigna la textura combinada al material de este objeto.
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = combinedTexture;
        }
    }
}
