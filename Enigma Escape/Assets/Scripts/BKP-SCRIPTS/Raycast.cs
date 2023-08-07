using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{   
    //Variables para Outline
    public GameObject outline;

    public float range = 5f;
    public Camera fpsCam;
    public GameObject textoInteractuar;

    //VARIABLES NUEVAS

    public GameObject inventoryPlayer;
    public GameObject eliminarObjeto;
    public GameObject casillas;
    int n;

        //mate
        public GameObject mate;

        //puerta salida
        public GameObject mapaProvincias;

    //Radio y pilas

    public bool pilasEnMano = false;
    public GameObject pilasMano;
    public GameObject pilasRadio;

    //Variables para apertura de Armario 2
    public Animator aperturaPertaIzqArmario2;
    public Animator aperturaPertaDerArmario2;
    bool armario2Abierto = false;

    //Hit CuadroCataratas
    public Animator aperturaCuadroCataratas;
    bool cuadroAbierto;

    //RayCast Llave
    public GameObject llave;

    //Variables para apertura de Armario
    public Animator aperturaArmarioLlave;

    public GameObject llaveArmario;
    public GameObject llaveArmarioEnMano;
    public GameObject candado;

    bool armarioLlaveAbierto = false;
    bool abriendoArmario = false;
    bool armarioCuadrosAbierto = false;
    float tiempoAnimLlave = 0;

    //GameObjects Puzzle Escudo Nacional 
    public GameObject ovaloEscudoEnMano;
    public GameObject ovaloEscudo;
    public GameObject picaEscudoEnMano;
    public GameObject picaEscudo;
    public GameObject gorroEscudoEnMano;
    public GameObject gorroEscudo;
    public GameObject manosEscudoEnMano;
    public GameObject manosEscudo;
    public GameObject laurelesEscudoEnMano;
    public GameObject laurelesEscudo;
    public GameObject solEscudoEnMano;
    public GameObject solEscudo;

    //Animators mueble dormitorio
    public Animator aperturaPertaIzqMueble;
    public Animator aperturaPertaDerMueble;

    //VARIABLES PARA PUZZLE CUADROS

        //Variables Cambio Colores Botones Cuadros

        int ColorGlaciar = 1;
        int ColorElPalmar = 1;
        int ColorCerro = 1;
        int ColorCarpincho = 1;
        int ColorPinguino = 1;
        int ColorLlama = 1;

            //GameObjects de botones
            public GameObject BotonGlaciar;
            public GameObject BotonElPalmar;
            public GameObject BotonCerro;
            public GameObject BotonCarpincho;
            public GameObject BotonPinguino;
            public GameObject BotonLlama;

            //Booleans para detectar color correcto
            bool colorGlaciarCorrecto = false;
            bool colorPalmarCorrecto = false;
            bool colorCerroCorrecto = false;
            bool colorCarpinchoCorrecto = false;
            bool colorPinguinoCorrecto = false;
            bool colorLlamaCorrecto = false;



    //VARIABLES PARA AGARRAR OBJETOS

    public Transform mano;

    public bool manoOcupada = false;
    public bool agarrasteLlaveCajon = false;
    bool agarrasteLlaveArmario = false;
    bool agarrasteMate = false;
    bool agarrasteOvaloEscudo = false;
    bool agarrastePicaEscudo = false;
    bool agarrasteGorroEscudo = false;
    bool agarrasteManosEscudo = false;
    bool agarrasteLaurelesEscudo = false;
    bool agarrasteSolEscudo = false;

    //bools para detectar piezas colocadas escudo
    bool ovaloEscudoColocado = false;
    bool picaEscudoColocado = false;
    bool gorroEscudoColocado = false;
    bool manosEscudoColocado = false;
    bool laurelesEscudoColocado = false;
    bool solEscudoColocado = false;

    //Sonidos
    public AudioSource agarraObjeto;
    public AudioSource sueltaObjeto;
    public AudioSource _Guitarra;
    public AudioSource armarioAbierto;
    public AudioSource armarioCerrado;
    public AudioSource llaveArmarioUnlock;
    public AudioSource botonCuadros;
    public AudioSource cuadroCataratasAbierto;
    public AudioSource cuadroCataratasCerrado;
    public AudioSource tomarMate;
    public AudioSource manija;
    public AudioSource puertaCerrada;
    public AudioSource botonMesa;
    public AudioSource pistaNotas;
    public AudioSource colocarObjeto;
    public AudioSource musicaAmbiente;


    //Camaras
    public GameObject camara_radio;
    public GameObject camara_panel;
    public GameObject camara_panel2;
    public GameObject camara_pista1;
    public GameObject camara_pista2;
    public GameObject camara_pista3;

    //Player
    public GameObject player;

    //Boleanos
    bool radioActivada = false;
    bool armarioAbiertoBoton = false;

    //TextosCanvas
    public GameObject textoRadio;
    public GameObject textoPuerta;
    public GameObject textoCaja;
    public GameObject textoMate;
    public GameObject textoBoton;
    public GameObject textoCajon;
    public GameObject textoPuerta2;
    public GameObject textoEntrada;
    public GameObject textoSalida;

    public static bool textoActivo = false;

    //Animator
    public Animator armarioCuadros;
    public Animator cofre;

    //GameObjects
    public GameObject cofreActivo;
    public GameObject armarioDormitorioActivo;

    //BoxColliders
    public BoxCollider cerraduraCajon;
    public BoxCollider cajon2;
    public BoxCollider puertaDormitorio;
    public BoxCollider armario;

    //Llave dormitorio
    public GameObject textoAgarrar;
    public GameObject llaveDormitorioEnMano;
    public bool agarrasteLlaveDormitorio = false;
    public Animator aperturaPuertaDormitorio;
    public AudioSource puertaAbriendo;

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        n = inventoryPlayer.GetComponent<InventorySystemAndItemCollector>().n;
        eliminarObjeto = inventoryPlayer.GetComponent<InventorySystemAndItemCollector>().objetosEnInventario[n];
        casillas = inventoryPlayer.GetComponent<InventorySystemAndItemCollector>().canvasCasillas[n];

        //evitar la pausa cuando hay texto en pantalla
        if (textoActivo == true)
        {
            Pausa.noPausa = true;
            Cursor.visible = false;
        }
        else
        {
            Pausa.noPausa = false;
        }

        RaycastObjetosUsables();


        //Usar Llave Cajon
        UsarLlaveCajon();



        //Puzzle Cuadros
        PuzzleCuadros();

    }

 /*   void PuzzleCuadroEscudo()
    {
        if(ovaloEscudoColocado == true && picaEscudoColocado == true && gorroEscudoColocado == true && 
            manosEscudoColocado == true && laurelesEscudoColocado == true && solEscudoColocado == true)
            {
                Debug.Log("PUZZLE ESCUDO RESUELTO");
                armarioDormitorioActivo.SetActive(true);
                aperturaPertaIzqMueble.SetBool("Open", true);
                aperturaPertaDerMueble.SetBool("Open", true);              
            }
    } */

    void UsarLlaveCajon()
    {
        if (agarrasteLlaveCajon == true)
        {
            Debug.Log("Agarraste Llave");
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Soltaste Llave");
                agarrasteLlaveCajon = false;
            }
        }
    }


    void PuzzleCuadros()
    {
        if(colorGlaciarCorrecto == true && colorPalmarCorrecto == true && colorCerroCorrecto == true && colorCarpinchoCorrecto == true
             && colorPinguinoCorrecto == true && colorLlamaCorrecto == true)
        {
            cofre.SetBool("Open", true);
            cofreActivo.SetActive(true);
        }
    }

    void SoltarLlaveDormitorio()
    {
        if (agarrasteLlaveDormitorio == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Soltaste Llave");
                agarrasteLlaveDormitorio = false;
            }
        }      
    }

    void RaycastObjetosUsables()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {


            //Hit LlaveDormitorio
            if (hit.transform.CompareTag("LlaveDormitorio"))
            {
                textoAgarrar.SetActive(true);
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("ESTAS AGARRANDO LLAVE DORMITORIO");
                    hit.transform.SetParent(mano);
                    hit.transform.position = mano.position; //Mano = Spawn
                    //hit.transform.localScale = new Vector3(0.14782f, 0.14782f, 0.14782f);
                    manoOcupada = true;
                    agarrasteLlaveDormitorio = true;
                    agarraObjeto.Play();
                }
            }   

            if (hit.transform.CompareTag("PuertaDormitorio"))
            {
                textoInteractuar.SetActive(true);
                if (agarrasteLlaveDormitorio == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        puertaAbriendo.Play();
                        Destroy(eliminarObjeto);
                        casillas.GetComponent<Casillas>().casillaLlena = false;
                        aperturaPuertaDormitorio.SetBool("Open", true);
                        puertaDormitorio.enabled = false;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    { 
                        if (textoActivo == false)
                        {
                            textoActivo = true;
                            puertaCerrada.Play();
                            textoPuerta2.SetActive(true);
                            StartCoroutine("textoOFF");
                        }
                       
                    }
                     
                }
            }    

            //HIT ARMARIO 2 Abierto
            if (hit.transform.CompareTag("PuertaArmario2") && inventoryPlayer.GetComponent<InventorySystemAndItemCollector>().armarioAbierto == true)
            {
                textoInteractuar.SetActive(true);
                Debug.Log("ESTAS VIENDO EL ARMARIO ABIERTO");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (armario2Abierto == false)
                    {
                        aperturaPertaIzqArmario2.SetBool("Open", true);
                        aperturaPertaDerArmario2.SetBool("Open", true);
                        armario2Abierto = true;
                        armarioAbierto.Play();
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            aperturaPertaIzqArmario2.SetBool("Open", false);
                            aperturaPertaDerArmario2.SetBool("Open", false);
                            armario2Abierto = false;
                            armarioCerrado.Play();
                        }
                    }
                }
            }

            //Hit CuadroCataratas
            if (hit.transform.CompareTag("CuadroCataratas"))
            {
                textoInteractuar.SetActive(true);

                //Abrir Cuadro
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (cuadroAbierto == false)
                    {
                        cuadroCataratasAbierto.Play();
                        aperturaCuadroCataratas.SetBool("Open", true);
                        cuadroAbierto = true;
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            cuadroCataratasCerrado.Play();
                            aperturaCuadroCataratas.SetBool("Open", false);
                            cuadroAbierto = false;
                        }
                    }
                }
            }
            
            //Hit Cajones
            if (hit.transform.CompareTag("Cajones"))
            {
                textoInteractuar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<AperturaCajones>().AbreCierra();
                }
            }

            //Hit Cajon Con Llave
            if (hit.transform.CompareTag("CajonConLlave"))
            {
                textoInteractuar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (agarrasteLlaveCajon == true)
                    {
                        llaveArmarioUnlock.Play();
                        cerraduraCajon.enabled = false;
                        cajon2.enabled = true;
                        //Destroy(llave);
                        Destroy(eliminarObjeto);
                        casillas.GetComponent<Casillas>().casillaLlena = false;
                    }
                    else
                    {
                        if (textoActivo == false)
                        {
                            textoActivo = true;
                            textoCajon.SetActive(true);
                            StartCoroutine("textoOFF");
                            puertaCerrada.Play();
                        }      
                    }
                }     
            }

            //Hit CuadroMapaArg
            if (hit.transform.CompareTag("CuadroMapaArg"))
            {
                textoInteractuar.SetActive(true);

            }


            //Hit CuadroMapaArg
            if (hit.transform.CompareTag("CuadroMapaArg"))
            {
                textoInteractuar.SetActive(true);
            }

            //Hit BotonesCuadros

            //BotonGlaciar
            if (hit.transform.CompareTag("BotonGlaciar"))
            {
                textoInteractuar.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    botonCuadros.Play();
                    ColorGlaciar += 1;
                    if(ColorGlaciar == 4)
                    {
                        ColorGlaciar = 1;
                    }

                    switch (ColorGlaciar)
                    {
                        case 3:
                            BotonGlaciar.GetComponent<Renderer>().material.color = Color.green;
                            BotonGlaciar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            BotonGlaciar.GetComponent<Light>().color = Color.green;
                            colorGlaciarCorrecto = false;
                            break;
                        case 2:
                            BotonGlaciar.GetComponent<Renderer>().material.color = Color.blue;
                            BotonGlaciar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            BotonGlaciar.GetComponent<Light>().color = Color.blue;
                            colorGlaciarCorrecto = true;
                            break;
                        case 1:
                            BotonGlaciar.GetComponent<Renderer>().material.color = Color.red;
                            BotonGlaciar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                            BotonGlaciar.GetComponent<Light>().color = Color.red;
                            colorGlaciarCorrecto = false;
                            break;                           
                    }
                }
            }


            //BotonPalmar
            if (hit.transform.CompareTag("BotonPalmar"))
            {
                textoInteractuar.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    botonCuadros.Play();
                    ColorElPalmar  += 1;
                    if(ColorElPalmar  == 4)
                    {
                        ColorElPalmar  = 1;
                    }

                    switch (ColorElPalmar)
                    {
                        case 3:
                            BotonElPalmar.GetComponent<Renderer>().material.color = Color.red;
                            BotonElPalmar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                            BotonElPalmar.GetComponent<Light>().color = Color.red;
                            colorPalmarCorrecto = false;
                            break;
                        case 2:
                            BotonElPalmar.GetComponent<Renderer>().material.color = Color.green;
                            BotonElPalmar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            BotonElPalmar.GetComponent<Light>().color = Color.green;
                            colorPalmarCorrecto = true;
                            break;
                        case 1:
                            BotonElPalmar.GetComponent<Renderer>().material.color = Color.blue;
                            BotonElPalmar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            BotonElPalmar.GetComponent<Light>().color = Color.blue;
                            colorPalmarCorrecto = false;
                            break;                           
                    }
                }
            }


            //BotonCerro
            if (hit.transform.CompareTag("BotonCerro"))
            {
                textoInteractuar.SetActive(true);

                
                if(Input.GetKeyDown(KeyCode.E))
                {
                    botonCuadros.Play();
                    ColorCerro  += 1;
                    if(ColorCerro  == 4)
                    {
                        ColorCerro  = 1;
                    }

                    switch (ColorCerro)
                    {
                        case 3:
                            BotonCerro.GetComponent<Renderer>().material.color = Color.blue;
                            BotonCerro.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            BotonCerro.GetComponent<Light>().color = Color.blue;
                            colorCerroCorrecto = false;
                            break;
                        case 2:
                            BotonCerro.GetComponent<Renderer>().material.color = Color.red;
                            BotonCerro.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                            BotonCerro.GetComponent<Light>().color = Color.red;
                            colorCerroCorrecto = true;
                            break;
                        case 1:
                            BotonCerro.GetComponent<Renderer>().material.color = Color.green;
                            BotonCerro.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            BotonCerro.GetComponent<Light>().color = Color.green;
                            colorCerroCorrecto = false;
                            break;                           
                    }
                }
            }


            //BotonPinguino
            if (hit.transform.CompareTag("BotonPinguino"))
            {
                textoInteractuar.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    botonCuadros.Play();
                    ColorPinguino  += 1;
                    if(ColorPinguino  == 4)
                    {
                        ColorPinguino  = 1;
                    }

                    switch (ColorPinguino)
                    {
                        case 3:
                            BotonPinguino.GetComponent<Renderer>().material.color = Color.green;
                            BotonPinguino.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            BotonPinguino.GetComponent<Light>().color = Color.green;
                            colorPinguinoCorrecto = false;
                            break;
                        case 2:
                            BotonPinguino.GetComponent<Renderer>().material.color = Color.blue;
                            BotonPinguino.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            BotonPinguino.GetComponent<Light>().color = Color.blue;
                            colorPinguinoCorrecto = true;
                            break;
                        case 1:
                            BotonPinguino.GetComponent<Renderer>().material.color = Color.red;
                            BotonPinguino.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                            BotonPinguino.GetComponent<Light>().color = Color.red;
                            colorPinguinoCorrecto = false;
                            break;                           
                    }
                }
            }

            //BotonCarpincho
            if (hit.transform.CompareTag("BotonCarpincho"))
            {
                textoInteractuar.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    botonCuadros.Play();
                    ColorCarpincho  += 1;
                    if(ColorCarpincho  == 4)
                    {
                        ColorCarpincho  = 1;
                    }

                    switch (ColorCarpincho)
                    {
                        case 3:
                            BotonCarpincho.GetComponent<Renderer>().material.color = Color.red;
                            BotonCarpincho.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                            BotonCarpincho.GetComponent<Light>().color = Color.red;
                            colorCarpinchoCorrecto = false;
                            break;
                        case 2:
                            BotonCarpincho.GetComponent<Renderer>().material.color = Color.green;
                            BotonCarpincho.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            BotonCarpincho.GetComponent<Light>().color = Color.green;
                            colorCarpinchoCorrecto = true;
                            break;
                        case 1:
                            BotonCarpincho.GetComponent<Renderer>().material.color = Color.blue;
                            BotonCarpincho.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            BotonCarpincho.GetComponent<Light>().color = Color.blue;
                            colorCarpinchoCorrecto = false;
                            break;                           
                    }
                }
            }

            //BotonLlama
            if (hit.transform.CompareTag("BotonLlama"))
            {
                textoInteractuar.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    botonCuadros.Play();
                    ColorLlama  += 1;
                    if(ColorLlama  == 4)
                    {
                        ColorLlama  = 1;
                    }

                    switch (ColorLlama)
                    {
                        case 3:
                            BotonLlama.GetComponent<Renderer>().material.color = Color.blue;
                            BotonLlama.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            BotonLlama.GetComponent<Light>().color = Color.blue;
                            colorLlamaCorrecto = false;
                            break;
                        case 2:
                            BotonLlama.GetComponent<Renderer>().material.color = Color.red;
                            BotonLlama.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                            BotonLlama.GetComponent<Light>().color = Color.red;
                            colorLlamaCorrecto = true;
                            break;
                        case 1:
                            BotonLlama.GetComponent<Renderer>().material.color = Color.green;
                            BotonLlama.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            BotonLlama.GetComponent<Light>().color = Color.green;
                            colorLlamaCorrecto = false;
                            break;                           
                    }
                }
            }

            //HitTV
            if (hit.transform.CompareTag("TV"))
            {
                textoInteractuar.SetActive(true);
            }

            //HitGuitarra
            if (hit.transform.CompareTag("Guitarra"))
            {
                textoInteractuar.SetActive(true);
                {

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        musicaAmbiente.Pause();
                        StartCoroutine("PausaMusica");
                        _Guitarra.Play();
                    }

                }
            }

            //hit pilas 

            if(hit.transform.CompareTag("Pilas"))
            {
                textoAgarrar.SetActive(true);

                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Agarraste Pilas");
                    hit.transform.SetParent(mano);
                    hit.transform.position = mano.position; //Mano = Spawn
                    //hit.transform.localScale = new Vector3(0.14782f, 0.14782f, 0.14782f);                    
                    manoOcupada = true;
                    agarraObjeto.Play();
                    pilasEnMano = true;
                }
            }

            //HitRadioConPilas
            if (hit.transform.CompareTag("Radio_Con"))
            {
                textoInteractuar.SetActive(true);

                if(pilasEnMano == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        colocarObjeto.Play();
                        Destroy(eliminarObjeto);
                        casillas.GetComponent<Casillas>().casillaLlena = false;
                        pilasRadio.SetActive(true);
                        radioActivada = true;
                    }
                }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (radioActivada == true)
                        {
                            musicaAmbiente.Pause();
                            player.SetActive(false);
                            camara_radio.SetActive(true);
                            Pausa.noPausa = true;
                        }
                        else
                        {
                            if (textoActivo == false)
                            {
                                textoActivo = true;
                                textoRadio.SetActive(true);
                                StartCoroutine("textoOFF");
                            }   
                        }
                        
                    }

                
            }

            //hit llave
            if (hit.transform.CompareTag("Llave"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit llave armario
            if (hit.transform.CompareTag("LlaveArmario"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit provincias
            if (hit.transform.CompareTag("Provincias"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit ovalo escudo

            if (hit.transform.CompareTag("OvaloEscudoNacional"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit pica 

            if (hit.transform.CompareTag("PicaEscudoNacional"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit gorro frijio

            if (hit.transform.CompareTag("GorroEscudoNacional"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit manos escudo

            if (hit.transform.CompareTag("ManosEscudoNacional"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit laureles escudo

            if (hit.transform.CompareTag("LaurelesEscudoNacional"))
            {
                textoAgarrar.SetActive(true);
            }

            //hit sol escudo

            if (hit.transform.CompareTag("SolEscudoNacional"))
            {
                textoAgarrar.SetActive(true);
            }

            //Hit Cuadro Escudo Nacional
            if (hit.transform.CompareTag("CuadroEscudoNacional"))
            {
                textoInteractuar.SetActive(true);
            }

            //HitPiano
            if (hit.transform.CompareTag("Piano"))
            {
                textoInteractuar.SetActive(true);
            }

            //HitPanel
            if (hit.transform.CompareTag("Panel"))
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        player.SetActive(false);
                        camara_panel.SetActive(true);
                        Pausa.noPausa = true;
                    }
                }
            }

            //HitPanel2
            if (hit.transform.CompareTag("Panel2"))
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        player.SetActive(false);
                        camara_panel2.SetActive(true);
                        Pausa.noPausa = true;
                    }
                }
            }

            //HitManija
            if (hit.transform.CompareTag("Manija"))
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                    {
                        manija.Play();
                        textoCaja.SetActive(true);
                        textoActivo = true;
                        StartCoroutine("textoOFF");
                    }                     
                }
            }

            //HitMate
            if (hit.transform.CompareTag("ObjetoAgarrable"))
            {
                mate = hit.transform.gameObject;

                textoAgarrar.SetActive(true); 

                if(mate.GetComponent<IdentificadorDeObjetos>().mate == true)
                {
                    textoInteractuar.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                    {
                        musicaAmbiente.volume = 0.1f;
                        StartCoroutine("Volumen");
                        tomarMate.Play();
                        textoMate.SetActive(true);
                        textoActivo = true;
                        StartCoroutine("textoOFF");
                    }
                }
                
                
                
            }

            //HitCopa
            if (hit.transform.CompareTag("Copa"))
            {
                textoInteractuar.SetActive(true);
            }

            //HitPuertaEntrada
            if (hit.transform.CompareTag("PuertaEntrada"))
            {
                textoInteractuar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                {
                    textoEntrada.SetActive(true);
                    textoActivo = true;
                    StartCoroutine("textoOFF");
                }
            }

            //HitPuertaSalida
            if (hit.transform.CompareTag("PuertaDeSalida"))
            {
                textoInteractuar.SetActive(true);
             if (mapaProvincias.GetComponent<provinciasMapa>().puertaSalidaAbierta == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SceneManager.LoadScene("GanasteMiArgentina");

                        //MODIFICADO
                        /*provinciasMapa.tdfColocada = false;
                        scColocada = false;
                        chuColocada = false;
                        rnColocada = false;
                        neuColocada = false;
                        lpColocada = false;
                        baColocada = false;
                        menColocada = false;
                        slColocada = false;
                        cordColocada = false;
                        sfColocada = false;
                        erColocada = false;
                        corColocada = false;
                        misColocada = false;
                        sjColocada = false;
                        lrColocada = false;
                        cataColocada = false;
                        tucuColocada = false;
                        sdeColocada = false;
                        chaColocada = false;
                        forColocada = false;
                        salColocada = false;
                        jujColocada = false;*/

                    }
                }

             if (mapaProvincias.GetComponent<provinciasMapa>().puertaSalidaAbierta == false && textoActivo == false)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        puertaCerrada.Play();
                        textoSalida.SetActive(true);
                        textoActivo = true;
                        StartCoroutine("textoOFF");
                    }
                }   
            }

            //HitPuertaSalaMusica
            if (hit.transform.CompareTag("PuertaSala"))
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                    {
                        puertaCerrada.Play();
                        textoPuerta.SetActive(true);
                        textoActivo = true;
                        StartCoroutine("textoOFF");
                    }
                }
            }

            //HitPista1
            if (hit.transform.CompareTag("Pista1"))
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E) && textoActivo == false)
                    {
                        pistaNotas.Play();
                        camara_pista1.SetActive(true);
                        player.SetActive(false);
                        Pausa.noPausa = true;
                    }
                }
            }

            //HitPista2
            if (hit.transform.CompareTag("Pista2") && textoActivo == false)
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pistaNotas.Play();
                        camara_pista2.SetActive(true);
                        player.SetActive(false);
                        Pausa.noPausa = true;
                    }
                }
            }

            //HitPista3
            if (hit.transform.CompareTag("Pista3") && textoActivo == false)
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pistaNotas.Play();
                        camara_pista3.SetActive(true);
                        player.SetActive(false);
                        Pausa.noPausa = true;
                    }
                }
            }

            //HitArmarioCuadros
            if (hit.transform.CompareTag("PuertaArmario1"))
            {
                textoInteractuar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (armarioCuadrosAbierto == false)
                    {
                        armarioCerrado.Play();
                        armarioCuadros.SetBool("Open", false);
                        armarioCuadrosAbierto = true;
                    }
                    else
                    {
                        armarioAbierto.Play();
                        armarioCuadros.SetBool("Open", true);
                        armarioCuadrosAbierto = false;
                    }
                }
            }

            //HitBotonMesa
            if (hit.transform.CompareTag("BotonMesa"))
            {
                textoInteractuar.SetActive(true);
                {
                    if (Input.GetKeyDown(KeyCode.E) && armarioAbiertoBoton == false)
                    {
                        if (Mate.placaMesa == true)
                        {
                            botonMesa.Play();
                            armarioCuadros.SetBool("Open", true);
                            armarioAbiertoBoton = true;
                            armarioAbierto.Play();
                            armario.enabled = true;
                            armarioCuadrosAbierto = false;
                        }
                        else
                        {
                            if (textoActivo == false)
                            {
                                textoActivo = true;
                                botonMesa.Play();
                                textoBoton.SetActive(true);
                                StartCoroutine("textoOFF");
                            }   
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && armarioAbiertoBoton == true)
                    {
                        botonMesa.Play();
                    }
                }
            }
        }
        else
        {
            textoInteractuar.SetActive(false);
            textoAgarrar.SetActive(false);
        }       
    }


    IEnumerator timerAudio()
    {
        yield return new WaitForSeconds(2f);
        llaveArmarioUnlock.Play();
    }

    IEnumerator PausaMusica()
    {
        yield return new WaitForSeconds(2);
        musicaAmbiente.UnPause();
    }

    IEnumerator Volumen()
    {
        yield return new WaitForSeconds(2);
        musicaAmbiente.volume = 0.3f;
    }

    IEnumerator textoOFF()
    {
        yield return new WaitForSeconds(3f);
        textoRadio.SetActive(false);
        textoPuerta.SetActive(false);
        textoCaja.SetActive(false);
        textoMate.SetActive(false);
        textoBoton.SetActive(false);
        textoCajon.SetActive(false);
        textoPuerta2.SetActive(false);
        textoEntrada.SetActive(false);
        textoSalida.SetActive(false);
        textoActivo = false;
    }
}
