using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; 
    [SerializeField] float remainingTime; 
    [SerializeField] GameObject endGamePanel; 
    
    private bool isTimerRunning = true;

    private void Start()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false); //panel esté oculto al inicio
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime; //  tiempo
                remainingTime = Mathf.Max(remainingTime, 0); 
                UpdateTimerText();
            }
            else
            {
                isTimerRunning = false; // etener el temporizador
                TimeUp();
            }
        }
    }

    // Actualizar el texto del temporizador
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Lógica cuando el tiempo se acaba
    private void TimeUp()
    {

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true); // Mostrar el anuncio (opcional)
        }

        Time.timeScale = 0; // Congelar el juego
        StartCoroutine(RestartLevelAfterDelay(2f)); // Reiniciar después de 2 segundos
    }

    // Coroutine para reiniciar el nivel después de un pequeño retraso
    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Esperar en tiempo real (ignora Time.timeScale)
        Time.timeScale = 1; // Restablecer el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar el nivel actual
    }
}
