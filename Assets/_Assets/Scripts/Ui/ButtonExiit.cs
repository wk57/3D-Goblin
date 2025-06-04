using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExiit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("El juego se cerrará."); // Solo se verá en el editor
        Application.Quit(); // Cierra el juego al ejecutarlo como aplicación
    }
}
