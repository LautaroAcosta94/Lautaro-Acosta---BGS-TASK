using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagadoDeGavedad : MonoBehaviour
{

    public float range = 5f;
    public Camera fpsCam;

    public int n;

    public GameObject player;
    public GameObject prov;


    GameObject provinciaAgarrada;

    bool provinciaEnMano = false;

    // Update is called once per frame
    void Update()
    {
        ApagarGravedad();
        EncendidoGravedad();
    }

    void ApagarGravedad()
    {
        RaycastHit hit;
        n = player.GetComponent<InventorySystemAndItemCollector>().n;
        prov = player.GetComponent<InventorySystemAndItemCollector>().objetosEnInventario[n];

            if(prov.GetComponent<IdentificadorDeObjetos>().provincias == true)
            {

                prov.GetComponent<Rigidbody>().isKinematic = true;
                prov.GetComponent<MeshCollider>().enabled = false;
                prov.transform.localScale = new Vector3(0.14782f, 0.14782f, 0.14782f);
                provinciaEnMano = true;

            }
            else
            {
                prov = null;
            }
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if(hit.transform.CompareTag("CuadroMapaArg") && provinciaEnMano == true)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        provinciaEnMano = false;
                    }
                }
            }    
        
        
    }

    void EncendidoGravedad()
    {   
        if(provinciaEnMano == true)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                prov.GetComponent<Rigidbody>().isKinematic = false;
                prov.GetComponent<MeshCollider>().enabled = true;
                prov.transform.SetParent(null);
                prov.transform.localScale = new Vector3(0.14782f, 0.14782f, 0.14782f);
                //provinciaAgarrada = null;
                provinciaEnMano = false;
            }
            if(provinciaEnMano == false)
            {
                prov = null;
            }
        }

    }
}
