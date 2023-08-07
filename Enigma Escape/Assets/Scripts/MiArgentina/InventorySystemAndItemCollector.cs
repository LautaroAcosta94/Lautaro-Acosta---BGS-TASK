using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystemAndItemCollector : MonoBehaviour
{
    //Variables para Outline
    public GameObject outline;

    //Variables para eliminar Colliders
    public GameObject objCol;

    //Variables para Raycast

    float range = 8f;
    public Camera fpsCam;

    //Variables Slot Scroll

    int casillaActiva = 1;
    int casillasMax = 4;
    public int n;
    public GameObject[] canvasCasillaSeleccionada;

    //Variables Item Collector

    public GameObject[] objetosEnInventario;
    public Transform mano;

    int usedSlots = 0;
    int maxSlots = 4;
    bool invActivated = false;

    //Variables para canvas

    public GameObject[] canvasCasillas;

    //Apagar gravedad de items

    bool gravityOneOn = false;
    bool gravityTwoOn = false;
    bool gravityThreeOn = false;
    bool gravityFourOn = false;

    //Variables para interactores

    public GameObject textoInteractuar;
    public GameObject eliminarObj;
    public AudioSource colocandoObjeto;

        //Variables Interactor Mapa Arg

        public GameObject mapaArg;
        
        //Variables Interactor Cuadro Escudo

        public GameObject escudoPatrio;

        //Variables para apertura de Armario
        public Animator aperturaArmarioLlave;

        public GameObject llaveArmario;
        public GameObject candado;

        bool abriendoArmario;
        public bool armarioAbierto;

        float tiempoAnimLlave = 0;

        public AudioSource agarraObjeto;
        public AudioSource sueltaObjeto;
        public AudioSource llaveArmarioUnlock;

        //PROVISORIO !!! CAMBIAR URGENTE !! 

        public GameObject raycastPlayer;



    void Start()
    {
        objetosEnInventario = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        eliminarObj = objetosEnInventario[n];
        

        //n = casillaActiva;
        ItemFunction();
        ItemCollector();
        ItemSelector();
        DropItem();
        CasillasCanvas();

        //Timer Animacion Armario
        AbriendoArmario();
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
                    outline = hit.transform.gameObject;
                    outline.GetComponent<Outline>().enabled = true;
                    if(Input.GetMouseButtonDown(0))
                    {
                        if(objetosEnInventario[0] == null)
                        {
                            Debug.Log("Has Tomado Un Objeto en el slot 1");
                            hit.transform.SetParent(mano);
                            hit.transform.position = mano.position;
                            objetosEnInventario[0] = hit.transform.gameObject;
                            objetosEnInventario[0].GetComponent<Rigidbody>().isKinematic = true;                           
                            invActivated = true;
                            canvasCasillas[0].GetComponent<Casillas>().casillaLlena = true;
                            agarraObjeto.Play();
                        }
                        else
                        {
                            if(objetosEnInventario[1] == null)
                            {
                                Debug.Log("Has Tomado Un Objeto en el slot 2");
                                hit.transform.SetParent(mano);
                                hit.transform.position = mano.position;
                                objetosEnInventario[1] = hit.transform.gameObject;
                                objetosEnInventario[1].GetComponent<Rigidbody>().isKinematic = true;
                                canvasCasillas[1].GetComponent<Casillas>().casillaLlena = true; 
                                invActivated = true;
                                agarraObjeto.Play();
                            }
                            else
                            {
                                if(objetosEnInventario[2] == null)
                                {
                                   Debug.Log("Has Tomado Un Objeto en el slot 3");
                                   hit.transform.SetParent(mano);
                                   hit.transform.position = mano.position;
                                   objetosEnInventario[2] = hit.transform.gameObject;
                                   objetosEnInventario[2].GetComponent<Rigidbody>().isKinematic = true;
                                   canvasCasillas[2].GetComponent<Casillas>().casillaLlena = true; 
                                   invActivated = true;
                                   agarraObjeto.Play();   
                                }
                                else
                                {
                                    if(objetosEnInventario[3] == null)
                                    {
                                        Debug.Log("Has Tomado Un Objeto en el slot 4");
                                        hit.transform.SetParent(mano);
                                        hit.transform.position = mano.position;
                                        objetosEnInventario[3] = hit.transform.gameObject; 
                                        objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = true;
                                        canvasCasillas[3].GetComponent<Casillas>().casillaLlena = true; 
                                        invActivated = true;
                                        agarraObjeto.Play();      
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                outline.GetComponent<Outline>().enabled = false;
            }
        }

    }

    void CasillasCanvas()
    {
        if(objetosEnInventario[n] != null)
        {
            if(casillaActiva == 1)
            {
                canvasCasillaSeleccionada[0].SetActive(true);
            }

            if(casillaActiva == 2)
            {
                canvasCasillaSeleccionada[1].SetActive(true);
            }

            if(casillaActiva == 3)
            {
                canvasCasillaSeleccionada[2].SetActive(true);
            }

            if(casillaActiva == 4)
            {
                canvasCasillaSeleccionada[3].SetActive(true);
            }
        }
    }

    void DropItem()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(objetosEnInventario[n] != null)
            {
                if(casillaActiva == 1)
                {
                        objetosEnInventario[0].GetComponent<Rigidbody>().isKinematic = false;
                        Debug.Log("Has Soltado Objeto");
                        objetosEnInventario[0].transform.SetParent(null);
                        objetosEnInventario[0] = null;
                        canvasCasillas[0].GetComponent<Casillas>().casillaLlena = false;
                        sueltaObjeto.Play();                  
                }

                if(casillaActiva == 2)
                {
                    objetosEnInventario[1].GetComponent<Rigidbody>().isKinematic = false;
                    Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[1].transform.SetParent(null);
                    objetosEnInventario[1] = null;
                    canvasCasillas[1].GetComponent<Casillas>().casillaLlena = false;
                    sueltaObjeto.Play();                    
                }

                if(casillaActiva == 3)
                {
                    objetosEnInventario[2].GetComponent<Rigidbody>().isKinematic = false;
                    Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[2].transform.SetParent(null);
                    objetosEnInventario[2] = null;
                    canvasCasillas[2].GetComponent<Casillas>().casillaLlena = false;
                    sueltaObjeto.Play();                            
                }

                if(casillaActiva == 4)
                {
                    objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = false;
                    Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[3].transform.SetParent(null);
                    objetosEnInventario[3] = null;
                    canvasCasillas[3].GetComponent<Casillas>().casillaLlena = false;
                    sueltaObjeto.Play();                      
                }
            }
        }

    }

    public void ItemFunction()
    {
        RaycastHit hitFunction;
        

        if(objetosEnInventario[n] != null)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitFunction, range))
            {
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().gorroEscudo == true)
                {
                    Debug.Log("Agarraste gorro");

                    if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                    {
                        textoInteractuar.SetActive(true);
                        
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            objetosEnInventario[n] = null;
                            colocandoObjeto.Play();
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[0].SetActive(true);
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().gorroEscudoColocado = true;
                        }
                    }
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().laurelesEscudo == true)
                {
                    Debug.Log("Agarraste laureles");

                    if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                    {
                        textoInteractuar.SetActive(true);
                        
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            colocandoObjeto.Play();
                            objetosEnInventario[n] = null;
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[1].SetActive(true);
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().laurelesEscudoColocado = true;
                        }      
                    }    
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().llaveArmario == true)
                {
                    Debug.Log("Agarraste llave armario");

                    if (hitFunction.transform.CompareTag("PuertaArmario2"))
                    {
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            llaveArmario.SetActive(true);
                            aperturaArmarioLlave.SetBool("Open", true);
                            llaveArmarioUnlock.Play();
                            abriendoArmario = true;
                        }
                    }      
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().llaveCajon == true)
                {
                    raycastPlayer.GetComponent<Raycast>().agarrasteLlaveCajon = true;            
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().llaveDormitorio == true)
                {
                    raycastPlayer.GetComponent<Raycast>().agarrasteLlaveDormitorio = true;        
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().manosEscudo == true)
                {
                    Debug.Log("Agarraste manos");

                    if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                    {
                        textoInteractuar.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            colocandoObjeto.Play();
                            objetosEnInventario[n] = null;
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[2].SetActive(true);
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().manosEscudoColocado = true; 
                        }
                    }            
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().mate == true)
                {
                    Debug.Log("Agarraste Mate");          
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().ovaloEscudo == true)
                {
                    Debug.Log("Agarraste ovalo");

                    if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                    {
                        textoInteractuar.SetActive(true);
                        
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            colocandoObjeto.Play();
                            objetosEnInventario[n] = null;
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[3].SetActive(true);
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().ovaloEscudoColocado = true; 
                        }
                    }
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().picaEscudo == true)
                {
                    Debug.Log("Agarraste pica");

                    if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                    {
                        textoInteractuar.SetActive(true);
                        
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            colocandoObjeto.Play();
                            objetosEnInventario[n] = null;
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[4].SetActive(true);
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().picaEscudoColocado = true;  
                        }    
                    }       
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().pilas == true)
                {
                    raycastPlayer.GetComponent<Raycast>().pilasEnMano = true;            
                }
                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().provincias == true)
                {
                    Debug.Log("Agarraste provincia");
                    //provCol = objetosEnInventario[n];
                    //provCol.GetComponent<MeshCollider>().isTrigger = true;

                    

                    if (hitFunction.transform.CompareTag("CuadroMapaArg"))
                    {
                        textoInteractuar.SetActive(true);
                        Debug.Log("Estas mirando mapa");

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().tierraDelFuego == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[0].SetActive(true);
                                mapaArg.GetComponent<provinciasMapa>().tdfColocada = true;  
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().santaCruz == true)
                            {
                                //Debug.Log("colocaste provincia");
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[1].SetActive(true);
                                mapaArg.GetComponent<provinciasMapa>().scColocada = true;    
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().chubut == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[2].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().chuColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().rioNegro == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[3].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().rnColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().neuquen == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[4].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().neuColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().laPampa == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[5].SetActive(true);
                                mapaArg.GetComponent<provinciasMapa>().lpColocada = true;    
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().buenosAires == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[6].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().baColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().mendoza == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[7].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().menColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().sanLuis == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[8].SetActive(true);
                                mapaArg.GetComponent<provinciasMapa>().slColocada = true;    
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().cordoba == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[9].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().cordColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().santaFe == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[10].SetActive(true);
                                mapaArg.GetComponent<provinciasMapa>().sfColocada = true;    
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().entreRios == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[11].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().erColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().corrientes == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[12].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().corColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().misiones == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[13].SetActive(true);
                                mapaArg.GetComponent<provinciasMapa>().misColocada = true;    
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().sanJuan == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[14].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().sjColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().laRioja == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[15].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().lrColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().catamarca == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[16].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().cataColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().tucuman == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[17].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().tucuColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().santiagoDelEstero == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[18].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().sdeColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().chaco == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[19].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().chaColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().formosa == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[20].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().forColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().salta == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[21].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().salColocada = true;   
                            }

                            if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().jujuy == true)
                            {
                                Destroy(eliminarObj);
                                colocandoObjeto.Play();
                                objetosEnInventario[n] = null;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                mapaArg.GetComponent<provinciasMapa>().provincias[22].SetActive(true); 
                                mapaArg.GetComponent<provinciasMapa>().jujColocada = true;   
                            }
                        
                    }    
                }

                if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().solEscudo == true)
                {
                    Debug.Log("Agarraste sol");

                    if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                    {
                        textoInteractuar.SetActive(true);
                        
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(eliminarObj);
                            colocandoObjeto.Play();
                            objetosEnInventario[n] = null;
                            canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[5].SetActive(true);
                            escudoPatrio.GetComponent<logicaCuadroEscudo>().solEscudoColocado = true;
                        }
                    }          
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
            n=0;
            if(objetosEnInventario[0] != null)
            {
                objetosEnInventario[0].SetActive(true);
                canvasCasillaSeleccionada[0].SetActive(true);
            }
            else
            {
                canvasCasillaSeleccionada[0].SetActive(true);
            }
            if(objetosEnInventario[1] != null)
            {
                objetosEnInventario[1].SetActive(false);
            }
            if(objetosEnInventario[2] != null)
            {
                objetosEnInventario[2].SetActive(false);
            }
            if(objetosEnInventario[3] != null)
            {
                objetosEnInventario[3].SetActive(false);
            }

            ItemFunction(); 
        }
        else
        {
            canvasCasillaSeleccionada[n].SetActive(false);
        }

        if(casillaActiva == 2)
        {
            n=1;
            if(objetosEnInventario[0] != null)
            {
                objetosEnInventario[0].SetActive(false);
            }
            if(objetosEnInventario[1] != null)
            {
                objetosEnInventario[1].SetActive(true);
                canvasCasillaSeleccionada[1].SetActive(true);
            }
            else
            {
                canvasCasillaSeleccionada[1].SetActive(true);
            }
            if(objetosEnInventario[2] != null)
            {
                objetosEnInventario[2].SetActive(false);
            }
            if(objetosEnInventario[3] != null)
            {
                objetosEnInventario[3].SetActive(false);
            }
        }
        else
        {
            canvasCasillaSeleccionada[1].SetActive(false);
        }

        if(casillaActiva == 3)
        {
            n=2;
            if(objetosEnInventario[0] != null)
            {
                objetosEnInventario[0].SetActive(false);
            }
            if(objetosEnInventario[1] != null)
            {
                objetosEnInventario[1].SetActive(false);
            }
            if(objetosEnInventario[2] != null)
            {
                objetosEnInventario[2].SetActive(true);
                canvasCasillaSeleccionada[2].SetActive(true);
            }
            else
            {
                canvasCasillaSeleccionada[2].SetActive(true);
            }
            if(objetosEnInventario[3] != null)
            {
                objetosEnInventario[3].SetActive(false);
            }
        }
        else
        {
            canvasCasillaSeleccionada[2].SetActive(false);
        }

        if(casillaActiva == 4)
        {
            n=3;
            if(objetosEnInventario[0] != null)
            {
                objetosEnInventario[0].SetActive(false);
            }
            if(objetosEnInventario[1] != null)
            {
                objetosEnInventario[1].SetActive(false);
            }
            if(objetosEnInventario[2] != null)
            {
                objetosEnInventario[2].SetActive(false);
            }
            if(objetosEnInventario[3] != null)
            {
                objetosEnInventario[3].SetActive(true);
                canvasCasillaSeleccionada[3].SetActive(true);
            }
            else
            {
                canvasCasillaSeleccionada[3].SetActive(true);
            }
        }
        else
        {
            canvasCasillaSeleccionada[3].SetActive(false);
        }
    }

    void AbriendoArmario()
    {
        if (abriendoArmario == true)
        {
            Debug.Log("COMIENZA TIMER");
            tiempoAnimLlave += Time.deltaTime;
            
            if (tiempoAnimLlave >= 4)
            {
                Destroy(candado);
                Debug.Log("Armario Abierto");
                armarioAbierto = true;
                abriendoArmario = false;
                tiempoAnimLlave = 0;
            }
        }
    }


}