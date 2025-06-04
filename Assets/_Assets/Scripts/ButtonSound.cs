using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioSource audioSource; // Referencia al AudioSource
    [SerializeField] private AudioClip hoverSound;    // Sonido para hover
    [SerializeField] private AudioClip clickSound;    // Sonido para clic

    // Reproduce el sonido cuando el mouse pasa sobre el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // Reproduce el sonido cuando se hace clic en el botón
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
