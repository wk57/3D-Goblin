using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleFloating : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.5f; // Amplitud del movimiento (cuánto sube y baja)
    [SerializeField] private float floatSpeed = 1f; // Velocidad del movimiento

    private Vector3 startPosition;

    private void Start()
    {
        // Guarda la posición inicial del objeto
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcula el desplazamiento vertical usando una onda sinusoidal
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Actualiza la posición del objeto
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
