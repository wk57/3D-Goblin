using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExiit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("El juego se cerrar�."); // Solo se ver� en el editor
        Application.Quit(); // Cierra el juego al ejecutarlo como aplicaci�n
    }
}
