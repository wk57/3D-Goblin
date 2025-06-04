using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText; // El componente TMP
    [SerializeField] private float startSpacing = -100f; // Espaciado inicial
    [SerializeField] private float endSpacing = 0f; // Espaciado final
    [SerializeField] private float animationDuration = 2f; // Duración de la animación en segundos

    private float currentSpacing;
    private float elapsedTime = 0f;

    private void Start()
    {
        // Inicializar espaciado
        if (titleText != null)
        {
            currentSpacing = startSpacing;
            titleText.characterSpacing = currentSpacing;

            // Iniciar la animación
            StartCoroutine(AnimateCharacterSpacing());
        }
        else
        {
            Debug.LogError("El componente TextMeshProUGUI no está asignado.");
        }
    }

    private IEnumerator AnimateCharacterSpacing()
    {
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            // Interpolación lineal entre startSpacing y endSpacing
            currentSpacing = Mathf.Lerp(startSpacing, endSpacing, elapsedTime / animationDuration);
            titleText.characterSpacing = currentSpacing; // Asignar el valor interpolado
            yield return null; // Esperar un frame
        }

        // Asegurarse de que termine exactamente en el valor final
        titleText.characterSpacing = endSpacing;
    }
}

