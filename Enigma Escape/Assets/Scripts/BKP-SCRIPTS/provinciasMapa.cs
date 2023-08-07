using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class provinciasMapa : MonoBehaviour
{
    public GameObject[] provincias;

    //Puerta de salida
    public Animator aperturaPuertaSalida;
    public bool puertaSalidaAbierta = false; 
    public GameObject puertaAbriendo;
    public GameObject bloqueoPuertaSalida;
    public GameObject portalAbierto;

    //booleans para identificar si se colocaron todas las piezas del mapa
    public bool tdfColocada = false;
    public bool scColocada = false;
    public bool chuColocada = false;
    public bool rnColocada = false;
    public bool neuColocada = false;
    public bool lpColocada = false;
    public bool baColocada = false;
    public bool menColocada = false;
    public bool slColocada = false;
    public bool cordColocada = false;
    public bool sfColocada = false;
    public bool erColocada = false;
    public bool corColocada = false;
    public bool misColocada = false;
    public bool sjColocada = false;
    public bool lrColocada = false;
    public bool cataColocada = false;
    public bool tucuColocada = false;
    public bool sdeColocada = false;
    public bool chaColocada = false;
    public bool forColocada = false;
    public bool salColocada = false;
    public bool jujColocada = false;

    void Update()
    {
        PuzzleCuadroMapaCompletado();
    }

    void PuzzleCuadroMapaCompletado()
    {
        if(tdfColocada == true && scColocada == true && chuColocada == true && rnColocada == true && neuColocada == true
           && lpColocada == true && baColocada == true && menColocada == true && slColocada == true && cordColocada == true
           && sfColocada == true && erColocada == true && corColocada == true && misColocada == true && sjColocada == true
           && lrColocada == true && cataColocada == true && tucuColocada == true && sdeColocada == true && chaColocada == true
           && forColocada == true && salColocada == true && jujColocada == true)
        {
            puertaSalidaAbierta = true;
            bloqueoPuertaSalida.SetActive(false);
            portalAbierto.SetActive(true);
            puertaAbriendo.SetActive(true);
            Debug.Log("COMPLETASTE PUZZLE FINAL");
            aperturaPuertaSalida.SetBool("Open", true);        
        }
    }
}
