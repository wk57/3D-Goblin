using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int totalBottles; // Total de botellas en el nivel
    private int collectedBottles = 0; // Contador de botellas recogidas

    void Start()
    {
        // Encuentra todas las botellas en la escena
        totalBottles = GameObject.FindGameObjectsWithTag("Collectible").Length;
        Debug.Log("Total de botellas: " + totalBottles);
    }

    public void BottleCollected()
    {
        collectedBottles++;
        Debug.Log("Botellas recogidas: " + collectedBottles);

        // Verifica si todas las botellas fueron recogidas
        if (collectedBottles >= totalBottles)
        {
            Debug.Log("¡Todas las botellas recogidas!");
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // Cargar la siguiente escena
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¡No hay más niveles!");
        }
    }
}
