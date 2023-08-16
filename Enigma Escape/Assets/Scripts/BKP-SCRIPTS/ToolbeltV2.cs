using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbeltV2 : MonoBehaviour
{
    [Header("Configuracion Raycast")]

        public Camera fpsCam;
        public float range = 8f;

    [Space(6)]
    [Header("Datos Publicos de Casillas")]

        public int numeroDeCasillaActiva;
        public int nObjeto;

        int casillaActiva = 1;
        int casillasMax = 4;

    [Space(6)]
    [Header("Configuracion Recolector de Objetos")]

        public GameObject[] objetosEnInventario;
        public Transform mano;

        int usedSlots = 0;
        int maxSlots = 4;
        bool invActivated = false;

    //Variables para canvas (ELIMINAR)

        public GameObject[] canvasCasillas;
        public GameObject[] canvasCasillaSeleccionada;
    
    void Start()
    {
        objetosEnInventario = new GameObject[4];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ItemCollector();
        ItemSelector();
        DropItem();
    }

    void ItemCollector()
    {

        RaycastHit hit;

        if(usedSlots <= maxSlots)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (hit.transform.CompareTag("ObjetoAgarrable"))
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        if(objetosEnInventario[0] == null)
                        {
                            hit.transform.SetParent(mano);
                            hit.transform.position = mano.position;
                            objetosEnInventario[0] = hit.transform.gameObject;
                            objetosEnInventario[0].GetComponent<Rigidbody>().isKinematic = true; 
                            objetosEnInventario[0].GetComponent<canvasObjeto>().objetoEnCasillaUno = true;                            
                            invActivated = true;
                            
                        }
                        else
                        {
                            if(objetosEnInventario[1] == null)
                            {
                                hit.transform.SetParent(mano);
                                hit.transform.position = mano.position;
                                objetosEnInventario[1] = hit.transform.gameObject;
                                objetosEnInventario[1].GetComponent<Rigidbody>().isKinematic = true;
                                objetosEnInventario[1].GetComponent<canvasObjeto>().objetoEnCasillaDos = true;                            
                                invActivated = true;
                                
                            }
                            else
                            {
                                if(objetosEnInventario[2] == null)
                                {
                                   hit.transform.SetParent(mano);
                                   hit.transform.position = mano.position;
                                   objetosEnInventario[2] = hit.transform.gameObject;  
                                   objetosEnInventario[2].GetComponent<Rigidbody>().isKinematic = true; 
                                   objetosEnInventario[2].GetComponent<canvasObjeto>().objetoEnCasillaTres = true;                                  
                                   invActivated = true;  
                                }
                                else
                                {
                                    if(objetosEnInventario[3] == null)
                                    {
                                        hit.transform.SetParent(mano);
                                        hit.transform.position = mano.position;
                                        objetosEnInventario[3] = hit.transform.gameObject; 
                                        objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = true;
                                        objetosEnInventario[3].GetComponent<canvasObjeto>().objetoEnCasillaCuatro = true;                                     
                                        invActivated = true;       
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

    }

    void DropItem()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(objetosEnInventario[numeroDeCasillaActiva] != null)
            {
                if(casillaActiva == 1)
                {
                    objetosEnInventario[0].GetComponent<Rigidbody>().isKinematic = false;
                    objetosEnInventario[0].transform.SetParent(null);
                    objetosEnInventario[0].GetComponent<canvasObjeto>().objetoEnCasillaUno = false;   
                    objetosEnInventario[0] = null;                                     
                }

                if(casillaActiva == 2)
                {
                    objetosEnInventario[1].GetComponent<Rigidbody>().isKinematic = false;
                    objetosEnInventario[1].transform.SetParent(null);
                    objetosEnInventario[1].GetComponent<canvasObjeto>().objetoEnCasillaDos = false;   
                    objetosEnInventario[1] = null;                                       
                }

                if(casillaActiva == 3)
                {
                    objetosEnInventario[2].GetComponent<Rigidbody>().isKinematic = false;
                    objetosEnInventario[2].transform.SetParent(null);
                    objetosEnInventario[2].GetComponent<canvasObjeto>().objetoEnCasillaTres = false;   
                    objetosEnInventario[2] = null;                                        
                }

                if(casillaActiva == 4)
                {
                    objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = false;
                    objetosEnInventario[3].transform.SetParent(null);
                    objetosEnInventario[3].GetComponent<canvasObjeto>().objetoEnCasillaCuatro = false;   
                    objetosEnInventario[3] = null;   
                }
            }
        }

    }

   

    void ItemSelector()
    {
        //FUNCIONAMIENTO MOUSE SCROLL
        if(invActivated == true)
        {
            if(casillaActiva < 4)
            {
                if(Input.GetAxis ("Mouse ScrollWheel") > 0)
                {
                    casillaActiva += 1;
                    Debug.Log("SCROLL UP");
                }
            }
            if(casillaActiva > 1)
            {
                
                if(Input.GetAxis ("Mouse ScrollWheel") < 0)
                {
                    casillaActiva -= 1;
                    Debug.Log("SCROLL DOWN");
                }
            }
        }

        //FUNCIONAMIENTO ITEM SELECTOR 
        if(casillaActiva == 1)
        {
            numeroDeCasillaActiva=0;
            if(objetosEnInventario[0] != null)
            {
                //objetosEnInventario[0].SetActive(true);
                objetosEnInventario[0].GetComponent<MeshRenderer>().enabled = true;
            }
            if(objetosEnInventario[1] != null)
            {
                //objetosEnInventario[1].SetActive(false);
                objetosEnInventario[1].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[2] != null)
            {
                //objetosEnInventario[2].SetActive(false);
                objetosEnInventario[2].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[3] != null)
            {
                //objetosEnInventario[3].SetActive(false);
                objetosEnInventario[3].GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if(casillaActiva == 2)
        {
            numeroDeCasillaActiva=1;
            if(objetosEnInventario[0] != null)
            {
                //objetosEnInventario[0].SetActive(true);
                objetosEnInventario[0].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[1] != null)
            {
                //objetosEnInventario[1].SetActive(false);
                objetosEnInventario[1].GetComponent<MeshRenderer>().enabled = true;
            }
            if(objetosEnInventario[2] != null)
            {
                //objetosEnInventario[2].SetActive(false);
                objetosEnInventario[2].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[3] != null)
            {
                //objetosEnInventario[3].SetActive(false);
                objetosEnInventario[3].GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if(casillaActiva == 3)
        {
            numeroDeCasillaActiva=2;
            if(objetosEnInventario[0] != null)
            {
                //objetosEnInventario[0].SetActive(true);
                objetosEnInventario[0].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[1] != null)
            {
                //objetosEnInventario[1].SetActive(false);
                objetosEnInventario[1].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[2] != null)
            {
                //objetosEnInventario[2].SetActive(false);
                objetosEnInventario[2].GetComponent<MeshRenderer>().enabled = true;
            }
            if(objetosEnInventario[3] != null)
            {
                //objetosEnInventario[3].SetActive(false);
                objetosEnInventario[3].GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if(casillaActiva == 4)
        {
            numeroDeCasillaActiva=3;
            if(objetosEnInventario[0] != null)
            {
                //objetosEnInventario[0].SetActive(true);
                objetosEnInventario[0].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[1] != null)
            {
                //objetosEnInventario[1].SetActive(false);
                objetosEnInventario[1].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[2] != null)
            {
                //objetosEnInventario[2].SetActive(false);
                objetosEnInventario[2].GetComponent<MeshRenderer>().enabled = false;
            }
            if(objetosEnInventario[3] != null)
            {
                //objetosEnInventario[3].SetActive(false);
                objetosEnInventario[3].GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

}