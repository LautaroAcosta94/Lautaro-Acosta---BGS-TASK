using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform spawnPoint; // Referencia al objeto Spawn

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger es el personaje
        if (other.CompareTag("Player"))
        {
            // Teletransporta al personaje a la ubicación del objeto Spawn
            other.transform.position = spawnPoint.position;
        }
    }
}
