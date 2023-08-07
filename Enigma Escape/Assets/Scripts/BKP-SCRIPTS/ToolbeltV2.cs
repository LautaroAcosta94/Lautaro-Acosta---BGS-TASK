using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbeltV2 : MonoBehaviour
{
   //Variables para Raycast

        float range = 8f;
        public Camera fpsCam;

        //Variables Slot Scroll

        int casillaActiva = 1;
        int casillasMax = 4;
        public int n;

    //Variables Item Collector

        public GameObject[] objetosEnInventario;
        public Transform mano;

        int usedSlots = 0;
        int maxSlots = 4;
        bool invActivated = false;

        //Audios

        public AudioSource colocandoObjeto;

    //Variables para canvas

        public GameObject[] canvasCasillas;
        public GameObject[] canvasCasillaSeleccionada;
    
    //VARIABLES PARA MODIFICACION DE VARIABLES DEL SCRIPT "RAYCAST OBJETOS USABLES"

        public GameObject raycastObjetosUsables; 
        GameObject eliminarObjeto;

    //VARIABLES PARA EL FUNCIONAMIENTO DEL PUZZLE FINAL CON EL MAPA DE ARGENTINA

        public GameObject mapaArg;
    
    //VARIABLES PARA EL FUNCIONAMIENTO DEL PUZZLE DEL ESCUDO NACIONAL

        public GameObject escudoPatrio;
    
    //VARIABLES PARA LA APERTURA DE LA PUERTA DEL DORMITORIO

        bool abriendoCuarto;
        public bool llaveColocada = false;
        public GameObject llaveCuarto;
        public Animator animCuartoLlave;
        public Animator animPuertaDormitorio;
        public AudioSource sonidoLlaveCuartoUnlock;
        float timerAnimLlave = 0;
        float timerGiroLlave = 0;
    
    //VARIABLES PARA LA APERTURA DEL CAJON IZQUIERDO DEL ARMARIO DOS 

        bool cajonUnlocked = false;
        bool cajonAbierto = false;
        public bool llaveCajonColocada = false;
        public GameObject llaveCajonAnim;
        public Animator cajonIzqAnim;
        float timerAnimLlaveCajon = 0;

    //VARIABLES PARA ABRIR Y CERRAR CAJONES DEL ARMARIO UNO - ESTO LUEGO DE HABERLOS DESBLOQUEADO LEVANTANDO EL MATE (ESA FUNCION SE ENCUENTRA EN EL SCRIPT "PLACA DE PRESION")

        bool cajonArmUnoIzq = false;
        bool cajonArmUnoDer = false;
        bool desbloqueoDeCajones = false;
        public bool cajonesUnlocked = false;
        public Animator ArmUnoCajonIzqAnim;
        public Animator ArmUnoCajonDerAnim;

    //VARIABLES PARA LA APERTURA DE LA PUERTA SUPERIOR MEDIO DEL ARMARIO GRANDE

        bool puertaUnlocked = false;
        bool puertaAbierta = false;
        public bool llavePuertaColocada = false;
        public GameObject llavePuertaAnim;
        public Animator puertaMedioAnim;
        float timerAnimLlaveMueble = 0;


    void Start()
    {
        objetosEnInventario = new GameObject[4];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //n = casillaActiva;
        eliminarObjeto = objetosEnInventario[n];

        ItemFunction(); 
        ItemCollector();
        ItemSelector();
        DropItem();
        CasillasCanvas();

        AbriendoCuarto();
        AbriendoCajon();

        AperturaCajonesMuebleUno();

        AbriendoPuertaMedioMuebleGrande();
    }

    void CasillasCanvas()
    {
        if(objetosEnInventario[n] != null)
        {
            if(casillaActiva == 1)
            {
                canvasCasillaSeleccionada[0].SetActive(true);
                canvasCasillaSeleccionada[1].SetActive(false);
                canvasCasillaSeleccionada[2].SetActive(false);
                canvasCasillaSeleccionada[3].SetActive(false);
            }

            if(casillaActiva == 2)
            {
                canvasCasillaSeleccionada[0].SetActive(false);
                canvasCasillaSeleccionada[1].SetActive(true);
                canvasCasillaSeleccionada[2].SetActive(false);
                canvasCasillaSeleccionada[3].SetActive(false);
            }

            if(casillaActiva == 3)
            {
                canvasCasillaSeleccionada[0].SetActive(false);
                canvasCasillaSeleccionada[1].SetActive(false);
                canvasCasillaSeleccionada[2].SetActive(true);
                canvasCasillaSeleccionada[3].SetActive(false);
            }

            if(casillaActiva == 4)
            {
                canvasCasillaSeleccionada[0].SetActive(false);
                canvasCasillaSeleccionada[1].SetActive(false);
                canvasCasillaSeleccionada[2].SetActive(false);
                canvasCasillaSeleccionada[3].SetActive(true);
            }
        }
        else
        {
            if(casillaActiva == 1)
            {
                canvasCasillaSeleccionada[0].SetActive(true);
                canvasCasillaSeleccionada[1].SetActive(false);
                canvasCasillaSeleccionada[2].SetActive(false);
                canvasCasillaSeleccionada[3].SetActive(false);
            }

            if(casillaActiva == 2)
            {
                canvasCasillaSeleccionada[0].SetActive(false);
                canvasCasillaSeleccionada[1].SetActive(true);
                canvasCasillaSeleccionada[2].SetActive(false);
                canvasCasillaSeleccionada[3].SetActive(false);
            }

            if(casillaActiva == 3)
            {
                canvasCasillaSeleccionada[0].SetActive(false);
                canvasCasillaSeleccionada[1].SetActive(false);
                canvasCasillaSeleccionada[2].SetActive(true);
                canvasCasillaSeleccionada[3].SetActive(false);
            }

            if(casillaActiva == 4)
            {
                canvasCasillaSeleccionada[0].SetActive(false);
                canvasCasillaSeleccionada[1].SetActive(false);
                canvasCasillaSeleccionada[2].SetActive(false);
                canvasCasillaSeleccionada[3].SetActive(true);
            }
        }
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
                            //Debug.Log("Has Tomado Un Objeto en el slot 1");
                            hit.transform.SetParent(mano);
                            hit.transform.position = mano.position;
                            objetosEnInventario[0] = hit.transform.gameObject;
                            objetosEnInventario[0].GetComponent<Rigidbody>().isKinematic = true;
                            canvasCasillas[0].GetComponent<Casillas>().casillaLlena = true;
                            invActivated = true;
                            
                        }
                        else
                        {
                            if(objetosEnInventario[1] == null)
                            {
                                //Debug.Log("Has Tomado Un Objeto en el slot 2");
                                hit.transform.SetParent(mano);
                                hit.transform.position = mano.position;
                                objetosEnInventario[1] = hit.transform.gameObject;
                                objetosEnInventario[1].GetComponent<Rigidbody>().isKinematic = true;
                                canvasCasillas[1].GetComponent<Casillas>().casillaLlena = true;
                                invActivated = true;
                                
                            }
                            else
                            {
                                if(objetosEnInventario[2] == null)
                                {
                                   //Debug.Log("Has Tomado Un Objeto en el slot 3");
                                   hit.transform.SetParent(mano);
                                   hit.transform.position = mano.position;
                                   objetosEnInventario[2] = hit.transform.gameObject;  
                                   objetosEnInventario[2].GetComponent<Rigidbody>().isKinematic = true;
                                   canvasCasillas[2].GetComponent<Casillas>().casillaLlena = true;
                                   invActivated = true;  
                                }
                                else
                                {
                                    if(objetosEnInventario[3] == null)
                                    {
                                        //Debug.Log("Has Tomado Un Objeto en el slot 4");
                                        hit.transform.SetParent(mano);
                                        hit.transform.position = mano.position;
                                        objetosEnInventario[3] = hit.transform.gameObject; 
                                        objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = true;
                                        canvasCasillas[3].GetComponent<Casillas>().casillaLlena = true;
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
            if(objetosEnInventario[n] != null)
            {
                if(casillaActiva == 1)
                {
                    objetosEnInventario[0].GetComponent<Rigidbody>().isKinematic = false;
                    canvasCasillas[0].GetComponent<Casillas>().casillaLlena = false;
                    //Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[0].transform.SetParent(null);
                    objetosEnInventario[0] = null;                                     
                }

                if(casillaActiva == 2)
                {
                    objetosEnInventario[1].GetComponent<Rigidbody>().isKinematic = false;
                    canvasCasillas[1].GetComponent<Casillas>().casillaLlena = false;
                    //Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[1].transform.SetParent(null);
                    objetosEnInventario[1] = null;                                       
                }

                if(casillaActiva == 3)
                {
                    objetosEnInventario[2].GetComponent<Rigidbody>().isKinematic = false;
                    canvasCasillas[2].GetComponent<Casillas>().casillaLlena = false;
                    //Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[2].transform.SetParent(null);
                    objetosEnInventario[2] = null;                                        
                }

                if(casillaActiva == 4)
                {
                    objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = false;
                    canvasCasillas[3].GetComponent<Casillas>().casillaLlena = false;
                    //Debug.Log("Has Soltado Objeto");
                    objetosEnInventario[3].transform.SetParent(null);
                    objetosEnInventario[3] = null;
                    objetosEnInventario[3].GetComponent<Rigidbody>().isKinematic = false;     
                }
            }
        }

    }

    public void ItemFunction()
    {
        RaycastHit hitFunction;

            if(objetosEnInventario[n] != null)
            {
                if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitFunction, range))
                {
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().gorroEscudo == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                        {
                            //textoInteractuar.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[0].SetActive(true);
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().gorroEscudoColocado = true;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                                colocandoObjeto.Play(); 
                            }
                        } 
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().laurelesEscudo == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                        {
                            //textoInteractuar.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[1].SetActive(true);
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().laurelesEscudoColocado = true;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                                colocandoObjeto.Play(); 
                            }
                        }                                 
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().llaveArmario == true)
                    {
                        if (hitFunction.transform.CompareTag("PuertaSupMedio"))
                        {
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                if(puertaUnlocked == false)
                                {
                                    llavePuertaColocada = true;
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                }
                            }
                        }          
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().llaveCajon == true)
                    {
                        if (hitFunction.transform.CompareTag("ArmarioDosCajonConLlave"))
                        {
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                if(cajonUnlocked == false)
                                {
                                    llaveCajonColocada = true;
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                }
                            }
                        }     
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().llaveDormitorio == true)
                    {

                        if (hitFunction.transform.CompareTag("PuertaDormitorio"))
                        {
                            if(Input.GetKeyDown(KeyCode.E))
                            {   
                                llaveColocada = true;
                                sonidoLlaveCuartoUnlock.Play();
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                            }
                        }                                 
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().manosEscudo == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                        {
                            //textoInteractuar.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[2].SetActive(true);
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().manosEscudoColocado = true;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                                colocandoObjeto.Play(); 
                            }
                        }                                 
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().mate == true)
                    {
                                
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().ovaloEscudo == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                        {
                            //textoInteractuar.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[3].SetActive(true);
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().ovaloEscudoColocado = true;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                                colocandoObjeto.Play(); 
                            }
                        } 
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().picaEscudo == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                        {
                            //textoInteractuar.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[4].SetActive(true);
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().picaEscudoColocado = true;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                                colocandoObjeto.Play(); 
                            }
                        }     
                    }
                    
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().pilas == true)
                    {
                        raycastObjetosUsables.GetComponent<RaycastObjetosUsables>().pilasEnMano = true;

                        if(hitFunction.transform.CompareTag("Radio"))
                        {
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                Destroy(objetosEnInventario[n]);
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                            }
                        }
                    }
                    
                    //PUZZLE CUADRO MAPA ARGENTINA - ESTE PEDAZO DE CODIGO SIRVE PARA EL PUZZLE FINAL DEL MAPA. 
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().provincias == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroMapaArg"))
                        {
                            //textoInteractuar.SetActive(true);
                            Debug.Log("Estas mirando mapa");

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().tierraDelFuego == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[0].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().tdfColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().santaCruz == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[1].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().scColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play(); 
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().chubut == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[2].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().chuColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().rioNegro == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[3].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().rnColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().neuquen == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[4].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().neuColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().laPampa == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[5].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().lpColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().buenosAires == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[6].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().baColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().mendoza == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[7].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().menColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play(); 
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().sanLuis == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[8].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().slColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().cordoba == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[9].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().cordColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();  
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().santaFe == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[10].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().sfColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().entreRios == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[11].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().erColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().corrientes == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[12].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().corColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().misiones == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[13].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().misColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();  
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().sanJuan == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[14].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().sjColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().laRioja == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[15].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().lrColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play(); 
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().catamarca == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[16].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().cataColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().tucuman == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[17].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().tucuColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play(); 
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().santiagoDelEstero == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[18].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().sdeColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();  
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().chaco == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[19].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().chaColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().formosa == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[20].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().forColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play();
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().salta == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[21].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().salColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play(); 
                                }

                                if(Input.GetKeyDown(KeyCode.E) && objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().jujuy == true)
                                {
                                    mapaArg.GetComponent<provinciasMapa>().provincias[22].SetActive(true);
                                    mapaArg.GetComponent<provinciasMapa>().jujColocada = true; 
                                    canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                    Destroy(objetosEnInventario[n]);
                                    colocandoObjeto.Play(); 
                                }
                            
                        }  
                    }
                    if(objetosEnInventario[n].GetComponent<IdentificadorDeObjetos>().solEscudo == true)
                    {
                        if (hitFunction.transform.CompareTag("CuadroEscudoNacional"))
                        {
                            //textoInteractuar.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().piezasEscudo[5].SetActive(true);
                                escudoPatrio.GetComponent<logicaCuadroEscudo>().solEscudoColocado = true;
                                canvasCasillas[n].GetComponent<Casillas>().casillaLlena = false;
                                Destroy(objetosEnInventario[n]);
                                colocandoObjeto.Play(); 
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
            }
            if(objetosEnInventario[3] != null)
            {
                objetosEnInventario[3].SetActive(false);
            }
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
            }
        }
    }

    public void AbriendoCuarto()
    {
        if(llaveColocada == true)
        {
            llaveCuarto.SetActive(true);
            animCuartoLlave.SetBool("Ingresar", true);
            timerAnimLlave += Time.deltaTime;     

            if(timerAnimLlave >= 1)
            {
                animCuartoLlave.SetBool("Girar", true);
                timerGiroLlave += Time.deltaTime;

                if(timerGiroLlave >= 1)
                {
                    abriendoCuarto = true;
                }
                    
            }
        }
        if(abriendoCuarto == true)
        {
            animPuertaDormitorio.SetBool("Open", true);
            llaveCuarto.SetActive(false);
        }
    }

    //METODO PARA ABRIR Y CERRAR EL CAJON UNA VEZ DESBLOQUEADO
    public void AbriendoCajon()
    {
        RaycastHit hitFunction;
        
        if(llaveCajonColocada == true)
        {
            llaveCajonAnim.SetActive(true);
            timerAnimLlaveCajon += Time.deltaTime;

            if(timerAnimLlaveCajon > 1)
            {
                llaveCajonAnim.SetActive(false);
                cajonUnlocked = true;
            }
        }

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitFunction, range))
        {
            if(cajonUnlocked == true)
            {   
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (hitFunction.transform.CompareTag("ArmarioDosCajonConLlave"))
                    {
                        if(cajonAbierto == false)
                        {
                            cajonIzqAnim.SetBool("Open", true);
                            cajonAbierto = true;
                        }
                        else
                        {
                            cajonIzqAnim.SetBool("Open", false);
                            cajonAbierto = false;
                        }
                    }
                }
            }
        }
    }

    public void AbriendoPuertaMedioMuebleGrande()
    {
        RaycastHit hitFunction;
        
        if(llavePuertaColocada == true)
        {
            llavePuertaAnim.SetActive(true);
            timerAnimLlaveMueble += Time.deltaTime;

            if(timerAnimLlaveMueble > 1)
            {
                llavePuertaAnim.SetActive(false);
                puertaUnlocked = true;
            }
        }

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitFunction, range))
        {
            if(puertaUnlocked == true)
            {   
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (hitFunction.transform.CompareTag("PuertaSupMedio"))
                    {
                        if(puertaAbierta == false)
                        {
                            puertaMedioAnim.SetBool("Open", true);
                            puertaAbierta = true;
                        }
                        else
                        {
                            puertaMedioAnim.SetBool("Open", false);
                            puertaAbierta = false;
                        }
                    }
                }
            }
        }
    }

    public void AperturaCajonesMuebleUno()
    {
        RaycastHit hitFunction;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitFunction, range))
        {
            if(cajonesUnlocked == true)
            {
                if(desbloqueoDeCajones == false)
                {
                    ArmUnoCajonIzqAnim.SetBool("Open", true);
                    ArmUnoCajonDerAnim.SetBool("Open", true);
                    desbloqueoDeCajones = true;
                }

                if (hitFunction.transform.CompareTag("ArmarioCajonIzq"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        if(cajonArmUnoIzq == false)
                        {
                            ArmUnoCajonIzqAnim.SetBool("Open", true);
                            cajonArmUnoIzq = true;
                        }
                        else
                        {
                            ArmUnoCajonIzqAnim.SetBool("Open", false);
                            cajonArmUnoIzq = false;
                        }
                    }
                    
                }

                if (hitFunction.transform.CompareTag("ArmarioCajonDerecho"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        if(cajonArmUnoDer == false)
                        {
                            ArmUnoCajonDerAnim.SetBool("Open", true);
                            cajonArmUnoDer = true;
                        }
                        else
                        {
                            ArmUnoCajonDerAnim.SetBool("Open", false);
                            cajonArmUnoDer = false;
                        }
                    }
                }
            }
        }

    }
}


