using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasObjeto : MonoBehaviour
{
    [Header("Configuracion")]
        
        public GameObject toolBeltPlayer;
        public GameObject[] iconoDelCanvas;

        public bool objetoEnCasillaUno;
        public bool objetoEnCasillaDos;
        public bool objetoEnCasillaTres;
        public bool objetoEnCasillaCuatro;

    // Update is called once per frame
    void Update()
    {
        //Analiza si el objeto esta en la casilla uno
        if(objetoEnCasillaUno)
        {
            iconoDelCanvas[0].SetActive(true);
        }
        else
        {
            iconoDelCanvas[0].SetActive(false);
        }

        //Analiza si el objeto esta en la casilla dos
        if(objetoEnCasillaDos)
        {
            iconoDelCanvas[1].SetActive(true);
        }
        else
        {
            iconoDelCanvas[1].SetActive(false);
        }

        //Analiza si el objeto esta en la casilla tres
        if(objetoEnCasillaTres)
        {
            iconoDelCanvas[2].SetActive(true);
        }
        else
        {
            iconoDelCanvas[2].SetActive(false);
        }

        //Analiza si el objeto esta en la casilla cuatro
        if(objetoEnCasillaCuatro)
        {
            iconoDelCanvas[3].SetActive(true);
        }
        else
        {
            iconoDelCanvas[3].SetActive(false);
        }
    }
}
