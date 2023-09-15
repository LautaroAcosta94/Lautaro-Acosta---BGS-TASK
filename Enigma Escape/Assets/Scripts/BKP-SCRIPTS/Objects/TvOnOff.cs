using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TvOnOff : MonoBehaviour, IInteractable
{
    public GameObject TV_Encendida;
    public GameObject TV_Apagada;
    public bool encender = false;
    public AudioSource TV_ON;
    public AudioSource TV_OFF;
    public AudioSource musicaAmbiente;

    public void Interact()
    {

        //textoInteractuar.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("INTENTASTE ACTIVAR LA TV");
            if (!encender)
            {
                musicaAmbiente.volume = 0.1f;
                TV_ON.Play();
                TV_Encendida.SetActive(true);
                TV_Apagada.SetActive(false);
                encender = true;
                //Tele.GetComponent<VideoPlayer>().enabled = true;
            }
            else
            {
                musicaAmbiente.volume = 0.3f;
                TV_OFF.Play();
                TV_Apagada.SetActive(true);
                TV_Encendida.SetActive(false);
                encender = false;
                //Tele.GetComponent<VideoPlayer>().enabled = false;
            }
        }
    }
}
