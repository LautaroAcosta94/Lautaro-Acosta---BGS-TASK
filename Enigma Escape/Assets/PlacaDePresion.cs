using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaDePresion : MonoBehaviour
{
    bool mateLevantado = false;
    bool sonidosEjecutados = false;

    float timer;

    public AudioSource placaDesactivada;
    public AudioSource CajonAbierto;

    public Animator animCajonUnoArmUno;
    public Animator animCajonDosArmUno;
    public GameObject objPlacaDesactivada;

    public GameObject cajonesArmUno;

    void Update()
    {
        MateLevantado();
    }

    void OnTriggerExit(Collider col)
    {
        if(!col.transform.gameObject.CompareTag("PlacaDePresion"))
        {
            mateLevantado = true;
            objPlacaDesactivada.SetActive(false);
            cajonesArmUno.GetComponent<ToolbeltV2>().cajonesUnlocked = true;
        }
    }

    public void MateLevantado()
    {
        
        if(sonidosEjecutados == false)
        {
            if(mateLevantado == true)
            {
                placaDesactivada.Play();
                //animCajonUnoArmUno.SetBool("Open", true);
                //animCajonDosArmUno.SetBool("Open", true);
                timer += Time.deltaTime;
                if(timer >= 0.5)
                {
                    sonidosEjecutados = true;
                    CajonAbierto.Play();
                }
            }
        }
    }
}
