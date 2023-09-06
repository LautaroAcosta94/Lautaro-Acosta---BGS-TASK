using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    public string[] suelos = new string[4];

    public AudioClip[] sonidosPisadas = new AudioClip[4];
    AudioSource audioSource;

    public float raycastDistance = 0.2f; // en el inspector es mayor, no me puse a calcular de cu�no es el rayo

    private bool isWalking = false;
    private float intervaloDePasos = 0.5f; /* Intervalo entre pasos o tiempo entre pisadas, como se quiera apresiar >.< */
    private float tiempoSiguientePaso = 0f;
    private Vector3 lastPosition;   /*ac� guardo la �ltimo posicion, teniendo en cuenta que en el start en principio es la transform del player, y despu�s se va a ir actualizando a current cada vez que se deteca un peque�o movimiento ultima*/

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        bool siAlgunaTeclaEsPresionada = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (siAlgunaTeclaEsPresionada && Time.time >= tiempoSiguientePaso)/* en definitiva lo que hace este m�todo es generar un intervalo de una determinada cantida de segundos entre pisada y pisada, para que no se reproduzcan m�ltiples sonidos*/
        {
            Vector3 currentPosition = transform.position;
            float distanciaRecorrida = Vector3.Distance(currentPosition, lastPosition);
            if (distanciaRecorrida > 0.01f)  /*   esto de distancia recorrida fue necesario para que, si camino contra la pared por ej, no haga pasos, aunque es charlable  */
            {
                PlayFootstepSound();
                tiempoSiguientePaso = Time.time + intervaloDePasos;
            }
            lastPosition = currentPosition;
        }
        else if (!siAlgunaTeclaEsPresionada) /* si no pongo el bool en false, me pasaba que por m�s que levantaba la tecla, me seguia reproduciendo sonido a veces  */
        {

        }
    }

    private void Pasos(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlayFootstepSound()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag(suelos[0]) && suelos[0] != null)
            {
                Pasos(sonidosPisadas[0]);
            }

            if (hit.collider.CompareTag(suelos[1]) && suelos[1] != null)
            {
                Pasos(sonidosPisadas[1]);
            }

            if (hit.collider.CompareTag(suelos[2]) && suelos[2] != null)
            {
                Pasos(sonidosPisadas[2]);
            }

            if (hit.collider.CompareTag(suelos[3]) && suelos[3] != null)
            {
                Pasos(sonidosPisadas[3]);
            }
        }
    }
}