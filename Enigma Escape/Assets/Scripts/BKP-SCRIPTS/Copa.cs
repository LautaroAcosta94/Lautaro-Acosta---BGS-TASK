using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Copa : MonoBehaviour
{
    public float range = 5f;
    public Camera fpsCam;
    public GameObject chispas;
    public GameObject confeti;
    public GameObject poster;

    public AudioSource muchachos;
    public AudioSource pirotecnia;
    public AudioSource musicaAmbiente;

    public GameObject textoCampeones;

    bool campeones = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        copaDelMundo();
    }
    void copaDelMundo()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Copa"))
            {
                if (Input.GetKeyDown(KeyCode.E) && campeones == false)
                {
                    StartCoroutine("Volumen");
                    musicaAmbiente.Pause();
                    muchachos.Play();
                    pirotecnia.Play();
                    chispas.SetActive(true);
                    confeti.SetActive(true);
                    poster.SetActive(true);
                    campeones = true;
                } 
                else if (Input.GetKeyDown(KeyCode.E) && campeones == true && Raycast.textoActivo == false)
                {
                    Raycast.textoActivo = true;
                    textoCampeones.SetActive(true);
                    StartCoroutine("textoOFF");
                }
            }
        }
    }

    IEnumerator textoOFF()
    {
        yield return new WaitForSeconds(3);
        textoCampeones.SetActive(false);
        Raycast.textoActivo = false;
    }

    IEnumerator Volumen()
    {
        yield return new WaitForSeconds(15);
        musicaAmbiente.UnPause();
    }
}
