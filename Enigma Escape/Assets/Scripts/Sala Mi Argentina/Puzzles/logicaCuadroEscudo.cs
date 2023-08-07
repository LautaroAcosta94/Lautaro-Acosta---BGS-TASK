using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicaCuadroEscudo : MonoBehaviour
{
    public GameObject[] piezasEscudo;
    public AudioSource armarioAbierto;
    public Animator puertaIzq;
    public Animator puerdaDer;

    public bool ovaloEscudoColocado = false;
    public bool picaEscudoColocado = false;
    public bool gorroEscudoColocado = false;
    public bool manosEscudoColocado = false;
    public bool laurelesEscudoColocado = false;
    public bool solEscudoColocado = false;

    void Update()
    {
        PuzzleCuadroEscudo();
    }

    void PuzzleCuadroEscudo()
    {
        if(ovaloEscudoColocado == true && picaEscudoColocado == true && gorroEscudoColocado == true && 
            manosEscudoColocado == true && laurelesEscudoColocado == true && solEscudoColocado == true)
            {
                puertaIzq.SetBool("Open", true);
                puerdaDer.SetBool("Open", true); 
                Debug.Log("PUZZLE ESCUDO RESUELTO");
                //armarioAbierto.Play();
            }
    }
}
