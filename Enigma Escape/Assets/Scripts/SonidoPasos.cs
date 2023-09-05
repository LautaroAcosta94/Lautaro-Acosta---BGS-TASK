using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    public float raycastDistance = 0.2f; // en el inspector es mayor, no me puse a calcular de cuáno es el rayo
    private AudioSource audioSource;
    public AudioClip woodFootstepSound;
    public AudioClip rugFootstepSound;

    private bool isWalking = false;
    private float intervaloDePasos = 0.5f; /* Intervalo entre pasos o tiempo entre pisadas, como se quiera apresiar >.< */
    private float tiempoSiguientePaso = 0f;
    private Vector3 lastPosition;   /*acá guardo la último posicion, teniendo en cuenta que en el start en principio es la transform del player, y después se va a ir actualizando a current cada vez que se deteca un pequeño movimiento ultima*/

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        bool siAlgunaTeclaEsPresionada = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (siAlgunaTeclaEsPresionada && Time.time >= tiempoSiguientePaso)/* en definitiva lo que hace este método es generar un intervalo de una determinada cantida de segundos entre pisada y pisada, para que no se reproduzcan múltiples sonidos*/
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
        else if (!siAlgunaTeclaEsPresionada) /* si no pongo el bool en false, me pasaba que por más que levantaba la tecla, me seguia reproduciendo sonido a veces  */
        {
            isWalking = false;
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
            if (hit.collider.CompareTag("FloorWood"))
            {
                Pasos(woodFootstepSound);
            }
            else if (hit.collider.CompareTag("FloorRug"))
            {
                Pasos(rugFootstepSound);
            }
        }
    }
}