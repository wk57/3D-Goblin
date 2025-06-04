using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    private CharacterController player;
    private Transform currentPlatform = null;

    private Vector3 lastPlatformPosition;
    private Quaternion lastPlatformRotation;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (currentPlatform != null)
        {
            // Movimiento y rotaci�n de la plataforma
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            Quaternion platformRotationChange = currentPlatform.rotation * Quaternion.Inverse(lastPlatformRotation);

            // Mueve al jugador seg�n el movimiento y rotaci�n de la plataforma
            player.Move(platformMovement);

            // Actualiza la posici�n y rotaci�n de la plataforma para la siguiente comprobaci�n
            lastPlatformPosition = currentPlatform.position;
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Verifica si el jugador est� sobre una plataforma
        if (hit.moveDirection.y < -0.3f && Vector3.Angle(hit.normal, Vector3.up) < 45 && hit.collider.CompareTag("Platform"))
        {
            currentPlatform = hit.collider.transform;
            lastPlatformPosition = currentPlatform.position;
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    private void LateUpdate()
    {
        // Si el jugador ya no est� en contacto con la plataforma, resetea currentPlatform
        if (currentPlatform != null && !player.isGrounded)
        {
            currentPlatform = null;
        }
    }
}
