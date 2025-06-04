using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollectible : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab; // Prefab para las part�culas
    [SerializeField] private AudioClip collectSound; // Sonido de recolecci�n
    private AudioSource audioSource; // Fuente de audio

    private void Start()
    {
        // Obtiene o agrega un AudioSource al objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configuraci�n opcional del AudioSource
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Sonido 3D (opcional)
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCollect playerCollect = other.GetComponent<PlayerCollect>();

        if (playerCollect != null)
        {
            // Notifica al GameManager que se recogi� el objeto (si es necesario)
            FindObjectOfType<GameManager>().BottleCollected();

            // Reproduce el sonido de recolecci�n
            PlayCollectSound();

            // Genera part�culas
            if (particlePrefab != null)
            {
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
            }

            // Desactiva el objeto despu�s de que termine el sonido
            StartCoroutine(DisableAfterSound());
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    private IEnumerator DisableAfterSound()
    {
        // Espera hasta que el sonido termine antes de desactivar el objeto
        if (collectSound != null)
        {
            yield return new WaitForSeconds(collectSound.length);
        }
        gameObject.SetActive(false);
    }
}
