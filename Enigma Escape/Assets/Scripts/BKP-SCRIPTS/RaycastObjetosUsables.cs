using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RaycastObjetosUsables : MonoBehaviour
{
    //VARIABLES PARA RAYCAST

        public float range = 20f;
        public Camera fpsCam;
        public GameObject textoInteractuar;
        public GameObject textoTomar;

    //VARIABLES PARA OBTENER DATOS Y VARIABLES DEL SCRIPT TOOLBELT V2

        int n;
        public GameObject inventoryPlayer;
        public GameObject eliminarObjeto;
        public GameObject casillas;
    //VARIABLES PARA LA TELE

        public GameObject TV_Encendida;
        public GameObject TV_Apagada;
        public bool encender = false;
        public AudioSource TV_ON;
        public AudioSource TV_OFF;
        public AudioSource musicaAmbiente;

    //VARIABLES PARA CUADRO CATARATAS

        public Animator CuadroCataratas;
        public bool cuadroAbierto = false;
        public AudioSource CuadroAbriendo;
        public AudioSource CuadroCerrando;

    //VARIABLES PARA ACTIVACION DEL SISTEMA DE LA RADIO

        public GameObject camaraRadio;
        public bool pilasEnMano = false;
        public GameObject pilasEnRadio;
        public GameObject cameraPlayer;

    //VARIABLES PARA ACTIVACION DE PANEL PUERTA MUSICA

        public GameObject canvasPanelPuerta;
        bool panelCajaFuerteAbierto;

    //VARIABLES PARA ACTIVACION DE CAJA FUERTE

        public GameObject canvasCajaFuerte;
        bool panelPuertaAbierto;

    //VARIABLES PARA PUZZLE CUADROS

        //Variables Botones

        public float tiempoApagado = 0.9f; // Timer de apagado de boton

        public AudioSource sonidoBotonCuadros;
        public Animator botonGlaciar;
        public Animator botonElPalmar;
        public Animator botonCerro;
        public Animator botonCarpincho;
        public Animator botonPinguino;
        public Animator botonLlama;

        //Apertura del BAUL

        public Animator baul;
        public GameObject sonidoBaulAbierto;

        //TEXTOS

        public GameObject textoRadio;
        public GameObject textoPuertaTres;
        public GameObject textoPuertaEntrada;
        public GameObject textoPuertaDormitorio;
        public GameObject textoPuertaSalida;
        public GameObject textoCajon;
        public GameObject textoPuertaArmario;

        public AudioSource audioDuda;
        public AudioSource audioPuertaCerrada;
        public AudioSource audioPistas;

        float timerTextoRadio = 0;
        float timerTextoPuertaTres = 0;
        float timerTextoPuertaEntrada = 0;
        float timerTextoPuertaDormitorio = 0;
        float timerTextoCajon = 0;
        float timerTextoPuertaArmario = 0;
        float timerTextoPuertaSalida = 0;

        //PISTAS - 3 EN TOTAL

        public GameObject zoomPistaUno;
        public GameObject zoomPistaDos;
        public GameObject zoomPistaTres;

        public AudioSource pistaPapel;

        //PUERTA SALIDA
        public GameObject mapaProvincias;

        //Variables Cambio Colores Botones Cuadros

        int ColorGlaciar = 1;
        int ColorElPalmar = 1;
        int ColorCerro = 1;
        int ColorCarpincho = 1;
        int ColorPinguino = 1;
        int ColorLlama = 1;

            //GameObjects de botones
            public GameObject LuzBotonGlaciarUno;
            public GameObject LuzBotonGlaciarDos;

            public GameObject LuzBotonElPalmarUno;
            public GameObject LuzBotonElPalmarDos;

            public GameObject LuzBotonCerroUno;
            public GameObject LuzBotonCerroDos;

            public GameObject LuzBotonCarpinchoUno;
            public GameObject LuzBotonCarpinchoDos;

            public GameObject LuzBotonPinguinoUno;
            public GameObject LuzBotonPinguinoDos;

            public GameObject LuzBotonLlamaUno;
            public GameObject LuzBotonLlamaDos;

            //Booleans para detectar color correcto
            bool colorGlaciarCorrecto = false;
            bool colorPalmarCorrecto = false;
            bool colorCerroCorrecto = false;
            bool colorCarpinchoCorrecto = false;
            bool colorPinguinoCorrecto = false;
            bool colorLlamaCorrecto = false;
    

    void Update()
    {
        n = inventoryPlayer.GetComponent<ToolbeltV2>().n;
        eliminarObjeto = inventoryPlayer.GetComponent<ToolbeltV2>().objetosEnInventario[n];
        casillas = inventoryPlayer.GetComponent<ToolbeltV2>().canvasCasillas[n];

        ObjetosUsables();
        PuzzleCuadros();
        ApagadoDeTextos();
    }

    //METODO QUE CONTENDRA LAS ACCIONES DE TODOS LOS OBJETOS INTERACTORES DEL MAPA. EJEMPLO: TELE - BOTONES - PIANO - PANELES DE CODIGO - PUERTAS

    void ObjetosUsables()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if(hit.transform.CompareTag("ObjetoAgarrable"))
            {
                textoTomar.SetActive(true);
            }
            else
            {
                textoTomar.SetActive(false);
            }

            //TELEVISION
            if (hit.transform.CompareTag("Tele"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (!encender)
                    {
                        musicaAmbiente.volume = 0.1f;
                        TV_ON.Play();                                  
                        TV_Encendida.SetActive(true);
                        TV_Apagada.SetActive(false);
                        encender = true;
                        //Tele.GetComponent<VideoPlayer>().enabled = true;
                    }
                    else
                    {
                        musicaAmbiente.volume = 0.3f;
                        TV_OFF.Play();      
                        TV_Apagada.SetActive(true);              
                        TV_Encendida.SetActive(false);
                        encender = false;
                        //Tele.GetComponent<VideoPlayer>().enabled = false;
                    }
                }
            }

            //CUADRO CATARATAS
            if (hit.transform.CompareTag("CuadroCataratas"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(!cuadroAbierto)
                    {
                        CuadroCataratas.SetBool("Open", true);
                        CuadroAbriendo.Play();
                        cuadroAbierto = true;
                    }
                    else
                    {
                        CuadroCataratas.SetBool("Open", false);
                        CuadroCerrando.Play();
                        cuadroAbierto = false;
                    }
                }
            }

            //ACTIVACION DE ZOOM DE PISTAS

                //PISTA NUMERO UNO

                if(hit.transform.CompareTag("Pista1"))
                {
                    textoInteractuar.SetActive(true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        //pistaPapel.Play();
                        zoomPistaUno.SetActive(true);
                        inventoryPlayer.SetActive(false);
                    }
                }

                if(hit.transform.CompareTag("Pista2"))
                {
                    textoInteractuar.SetActive(true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        //pistaPapel.Play();
                        zoomPistaDos.SetActive(true);
                        inventoryPlayer.SetActive(false);
                    }
                }
                
                if(hit.transform.CompareTag("Pista3"))
                {
                    textoInteractuar.SetActive(true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        //pistaPapel.Play();
                        zoomPistaTres.SetActive(true);
                        inventoryPlayer.SetActive(false);
                    }
                }


            //CAJON ARMARIO CON LLAVE CERRADO

            if(hit.transform.CompareTag("ArmarioDosCajonConLlave"))
            {
                textoInteractuar.SetActive(true);
                if(inventoryPlayer.GetComponent<ToolbeltV2>().llaveCajonColocada == false)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        textoCajon.SetActive(true);
                        audioDuda.Play();
                    }
                }
            }

            //PUERTA ARMARIO CON LLAVE CERRADO

            if(hit.transform.CompareTag("PuertaSupMedio"))
            {
                textoInteractuar.SetActive(true);
                if(inventoryPlayer.GetComponent<ToolbeltV2>().llavePuertaColocada == false)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        textoPuertaArmario.SetActive(true);
                        audioDuda.Play();
                    }
                }
            }

            //PUERTA DE ENTRADA CERRADA

            if(hit.transform.CompareTag("PuertaSala"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    textoPuertaEntrada.SetActive(true);
                    audioPuertaCerrada.Play();
                }
            }

            //PUERTA DE DORMITORIO CERRADA

            if(hit.transform.CompareTag("PuertaDormitorio"))
            {
                textoInteractuar.SetActive(true);
                if(inventoryPlayer.GetComponent<ToolbeltV2>().llaveColocada == false)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        textoPuertaDormitorio.SetActive(true);
                        audioPuertaCerrada.Play();                       
                    }
                }
            }

            //PUERTA SALIDA CERRADA

            if(hit.transform.CompareTag("PuertaSalidaCerrada"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    textoPuertaSalida.SetActive(true);
                    audioPuertaCerrada.Play();
                }
            }

            //PUERTA SALIDA ABIERTA
            if(hit.transform.CompareTag("Portal"))
            {
                textoInteractuar.SetActive(true);
                if(mapaProvincias.GetComponent<provinciasMapa>().puertaSalidaAbierta == true)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        SceneManager.LoadScene("GanasteMiArgentina");
                    }
                }
                
            }

            //PUERTA SALA DE MUSICA

            if (hit.transform.CompareTag("PanelPuertaSalaMusica"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(!panelPuertaAbierto)
                    {
                        canvasPanelPuerta.SetActive(true);
                        cameraPlayer.SetActive(false);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        panelPuertaAbierto = true;
                    }
                }
            }

            if(hit.transform.CompareTag("PuertaSalaMusica"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(!panelPuertaAbierto)
                    {
                        textoPuertaTres.SetActive(true);
                        audioPuertaCerrada.Play();
                    }
                }
            }  

            //CAJA FUERTE 

            if (hit.transform.CompareTag("CajaFuerte"))
            {
                textoInteractuar.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(!panelCajaFuerteAbierto)
                    {
                        canvasCajaFuerte.SetActive(true);
                        cameraPlayer.SetActive(false);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        panelCajaFuerteAbierto = true;
                    }
                }
            }

            //COLOCACION DE PILAS EN RADIO Y ACTIVACION DE CAMARA Y FUNCION DE LA RADIO
                if(hit.transform.CompareTag("Radio"))
                {
                    textoInteractuar.SetActive(true);
                    if(pilasEnMano)//ANALIZA SI LAS PILAS SE ENCUENTRAN ACTUALMENTE EN LA MANO DEL JUGADOR
                    {
                        if(!pilasEnRadio.activeSelf)//SI LAS PILAS AUN NO FUERON COLOCADAS EN LA RADIO PERMITE ACCEDER AL BLOQUE PARA QUE AL TENERLAS EN LA MANO Y TOCAR LA TECLA "E" SE COLOQUEN Y SE ACTIVE LA RADIO.
                        {
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                pilasEnRadio.SetActive(true);//SE ENCIENDE LA VISUALIZACION DE LAS PILAS COLOCADAS EN LA RADIO
                            }
                        }
                    }
                    if(pilasEnRadio.activeSelf)//SI SE DETECTAN QUE LAS PILAS YA ESTAN COLOCADAS YA SE PUEDE ACCEDER A MANEJAR LAS EMISORAS DE LA RADIO
                    {
                        if(!camaraRadio.activeSelf)
                        {
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                camaraRadio.SetActive(true); //ACTIVA LA CAMARA QUE MIRA A LA RADIO Y POR CONSECUENTE LAS FUNCIONES DE SELECCION DE EMISORA
                            }
                        }
                    }
                    else
                    {
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            textoRadio.SetActive(true);
                            audioDuda.Play();
                        }
                    }
                }

                if(camaraRadio.activeSelf)
                {
                    camaraRadio.SetActive(true);
                    inventoryPlayer.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

            //PUZZLE CUADROS ANIMALES Y PROVINCIAS (ESTA PORCION DE CODIGO CONTIENE EL FUNCIONAMIENTO DEL PUZZLE DE LOS CUADROS - DESDE ACA SE CAMBIAN LOS COLORES DE LOS CUADROS AL TOCAR EL BOTON DE CADA CUADRO)

                //BotonGlaciar
                if (hit.transform.CompareTag("BotonGlaciar"))
                {
                    //textoInteractuar.SetActive(true);


                        if(Input.GetKeyDown(KeyCode.E))
                        {   
                            StartCoroutine(EncenderYApagarAnimacionBotonGlaciar());
                            sonidoBotonCuadros.Play();
                            ColorGlaciar += 1;

                            if(ColorGlaciar == 6)
                            {
                                ColorGlaciar = 1;
                            }

                            switch (ColorGlaciar)
                            {
                                case 5:
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.color = Color.yellow;
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.color = Color.yellow;
                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                    colorGlaciarCorrecto = false;
                                    break;
                                case 4:
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.color =  Color.magenta;
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.color =  Color.magenta;
                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                    colorGlaciarCorrecto = false;
                                    break;
                                case 3:
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.color = Color.green;
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.color = Color.green;
                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                    colorGlaciarCorrecto = false;
                                    break;
                                case 2:
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.color = Color.blue;
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.color = Color.blue;
                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                    colorGlaciarCorrecto = true;
                                    break;
                                case 1:
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.color = Color.red;
                                    LuzBotonGlaciarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.color = Color.red;
                                    LuzBotonGlaciarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                    colorGlaciarCorrecto = false;
                                    break;                           
                            }
                        }
                    
                }


                //BotonPalmar
                if (hit.transform.CompareTag("BotonPalmar"))
                {
                    //textoInteractuar.SetActive(true);


                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EncenderYApagarAnimacionBotonPalmar());
                        sonidoBotonCuadros.Play();
                        ColorElPalmar  += 1;
                        if(ColorElPalmar  == 6)
                        {
                            ColorElPalmar  = 1;
                        }

                        switch (ColorElPalmar)
                        {
                            case 5:
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                colorPalmarCorrecto = false;
                                break;
                            case 4:
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                colorPalmarCorrecto = false;
                                break;
                            case 3:
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                colorPalmarCorrecto = true;
                                break;
                            case 2:
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                colorPalmarCorrecto = false;
                                break;
                            case 1:
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonElPalmarUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonElPalmarDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                colorPalmarCorrecto = false;
                                break;                         
                        }
                    }
                }


                //BotonCerro
                if (hit.transform.CompareTag("BotonCerro"))
                {
                    //textoInteractuar.SetActive(true);
                    
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EncenderYApagarAnimacionBotonCerro());
                        sonidoBotonCuadros.Play();
                        ColorCerro  += 1;
                        if(ColorCerro  == 6)
                        {
                            ColorCerro  = 1;
                        }

                        switch (ColorCerro)
                        {
                            case 5:
                                LuzBotonCerroUno.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonCerroUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                LuzBotonCerroDos.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonCerroDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                colorCerroCorrecto = false;
                                break;
                            case 4:
                                LuzBotonCerroUno.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonCerroUno.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                LuzBotonCerroDos.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonCerroDos.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                colorCerroCorrecto = false;
                                break;
                            case 3:
                                LuzBotonCerroUno.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonCerroUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                LuzBotonCerroDos.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonCerroDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                colorCerroCorrecto = false;
                                break;
                            case 2:
                                LuzBotonCerroUno.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonCerroUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                LuzBotonCerroDos.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonCerroDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                colorCerroCorrecto = false;
                                break;
                            case 1:
                                LuzBotonCerroUno.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonCerroUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                LuzBotonCerroDos.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonCerroDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                colorCerroCorrecto = true;
                                break;                        
                        }
                    }
                }


                //BotonPinguino
                if (hit.transform.CompareTag("BotonPinguino"))
                {
                    //textoInteractuar.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EncenderYApagarAnimacionBotonPinguino());
                        sonidoBotonCuadros.Play();
                        ColorPinguino  += 1;
                        if(ColorPinguino  == 6)
                        {
                            ColorPinguino  = 1;
                        }

                        switch (ColorPinguino)
                        {
                            case 5:
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                colorPinguinoCorrecto = false;
                                break;
                            case 4:
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                colorPinguinoCorrecto = false;
                                break;
                            case 3:
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                colorPinguinoCorrecto = false;
                                break;
                            case 2:
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                colorPinguinoCorrecto = true;
                                break;
                            case 1:
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonPinguinoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonPinguinoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                colorPinguinoCorrecto = false;
                                break;                         
                        }
                    }
                }

                //BotonCarpincho
                if (hit.transform.CompareTag("BotonCarpincho"))
                {
                    //textoInteractuar.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EncenderYApagarAnimacionBotonCarpincho());
                        sonidoBotonCuadros.Play();
                        ColorCarpincho  += 1;
                        if(ColorCarpincho  == 6)
                        {
                            ColorCarpincho  = 1;
                        }

                        switch (ColorCarpincho)
                        {
                            case 5:
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                colorCarpinchoCorrecto = false;
                                break;
                            case 4:
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                colorCarpinchoCorrecto = false;
                                break;
                            case 3:
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                colorCarpinchoCorrecto = true;
                                break;
                            case 2:
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                colorCarpinchoCorrecto = false;
                                break;
                            case 1:
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonCarpinchoUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonCarpinchoDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                colorCarpinchoCorrecto = false;
                                break;                         
                        }
                    }
                }

                //BotonLlama
                if (hit.transform.CompareTag("BotonLlama"))
                {
                    //textoInteractuar.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EncenderYApagarAnimacionBotonLlama());
                        sonidoBotonCuadros.Play();
                        ColorLlama  += 1;
                        if(ColorLlama  == 6)
                        {
                            ColorLlama  = 1;
                        }

                        switch (ColorLlama)
                        {
                            case 5:
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                LuzBotonLlamaDos.GetComponent<Renderer>().material.color = Color.yellow;
                                LuzBotonLlamaDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);

                                colorLlamaCorrecto = false;
                                break;
                            case 4:
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                LuzBotonLlamaDos.GetComponent<Renderer>().material.color =  Color.magenta;
                                LuzBotonLlamaDos.GetComponent<Renderer>().material.SetColor("_EmissionColor",  Color.magenta);

                                colorLlamaCorrecto = false;
                                break;
                            case 3:
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                LuzBotonLlamaDos.GetComponent<Renderer>().material.color = Color.green;
                                LuzBotonLlamaDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                                colorLlamaCorrecto = false;
                                break;
                            case 2:
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                LuzBotonLlamaDos.GetComponent<Renderer>().material.color = Color.blue;
                                LuzBotonLlamaDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);

                                colorLlamaCorrecto = false;
                                break;
                            case 1:
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonLlamaUno.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                LuzBotonLlamaDos.GetComponent<Renderer>().material.color = Color.red;
                                LuzBotonLlamaDos.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                                colorLlamaCorrecto = true;
                                break;                          
                        }
                    }
                }

        }
        else
        {
            textoInteractuar.SetActive(false);
        }
    }

    //APAGADO DE TEXTOS

    void ApagadoDeTextos()
    {
        if(textoRadio.activeSelf)
        {
            timerTextoRadio += Time.deltaTime;

            if(timerTextoRadio >= 2)
            {
                textoRadio.SetActive(false);
                timerTextoRadio = 0;
            }
        }

        if(textoPuertaTres.activeSelf)
        {
            timerTextoPuertaTres += Time.deltaTime;

            if(timerTextoPuertaTres >= 2)
            {
                textoPuertaTres.SetActive(false);
                timerTextoPuertaTres = 0;
            }
        }

        
        if(textoPuertaEntrada.activeSelf)
        {
            timerTextoPuertaEntrada += Time.deltaTime;

            if(timerTextoPuertaEntrada >= 2)
            {
                textoPuertaEntrada.SetActive(false);
                timerTextoPuertaEntrada = 0;
            }
        }

        if(textoPuertaDormitorio.activeSelf)
        {
            timerTextoPuertaDormitorio += Time.deltaTime;

            if(timerTextoPuertaDormitorio >= 2)
            {
                textoPuertaDormitorio.SetActive(false);
                timerTextoPuertaDormitorio = 0; 
            }
        }

        if(textoCajon.activeSelf)
        {
            timerTextoCajon += Time.deltaTime;

            if(timerTextoCajon >= 2)
            {
                textoCajon.SetActive(false);
                timerTextoCajon = 0; 
            }
        }

        if(textoPuertaArmario.activeSelf)
        {
            timerTextoPuertaArmario += Time.deltaTime;

            if(timerTextoPuertaArmario >= 2)
            {
                textoPuertaArmario.SetActive(false);
                timerTextoPuertaArmario = 0; 
            }
        }

        if(textoPuertaSalida.activeSelf)
        {
            timerTextoPuertaSalida += Time.deltaTime;

            if(timerTextoPuertaSalida >= 2)
            {
                textoPuertaSalida.SetActive(false);
                timerTextoPuertaSalida = 0; 
            }
        }
        
        

    }

    //FUNCIONES DE ALGUNOS PUZZLES

    void PuzzleCuadros()
    {
        if(colorGlaciarCorrecto == true && colorPalmarCorrecto == true && colorCerroCorrecto == true && colorCarpinchoCorrecto == true
             && colorPinguinoCorrecto == true && colorLlamaCorrecto == true)
        {
            baul.SetBool("Open", true);
            sonidoBaulAbierto.SetActive(true);
        }
    }  

    //Partes de codigos para cerrar Ventanas de canvas

    //RADIO

    public void cerrarCamRadio()
    {
        inventoryPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camaraRadio.SetActive(false);
    }

    public void cerrarCamPanelPuerta()
    {
        canvasPanelPuerta.SetActive(false);
        cameraPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panelPuertaAbierto = false;
    }

    public void cerrarCamPanelCF()
    {
        canvasCajaFuerte.SetActive(false);
        cameraPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panelCajaFuerteAbierto = false;
    }

    public void cerrarPistaUno()
    {
        zoomPistaUno.SetActive(false);
        inventoryPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void cerrarPistaDos()
    {
        zoomPistaDos.SetActive(false);
        inventoryPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void cerrarPistaTres()
    {
        zoomPistaTres.SetActive(false);
        inventoryPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    
    //COURRUTINES ENCENDIDO Y APAGADO DE ANIMACIONES DE LOS BOTONES - ESTO SIRVE PARA ENCENDER Y APAGAR AUTOMATICAMENTE LAS ANIMACIONES DE LOS BOTONES LUEGO DE UNOS SEGUNDOS DE SER EJECUTADAS

        private IEnumerator EncenderYApagarAnimacionBotonGlaciar()
        {
                Debug.Log("Enciende Courrutine");
                botonGlaciar.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
                while (botonGlaciar.GetBool("Press") == true)
                {
                    Debug.Log("Ingresa al while");
                    yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
                    botonGlaciar.SetBool("Press", false); // LUEGO LA DESACTIVA
                }
                yield break; //FINALIZA LA COURRUTINE
        }

        private IEnumerator EncenderYApagarAnimacionBotonPalmar()
        {
                Debug.Log("Enciende Courrutine");
                botonElPalmar.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
                while (botonElPalmar.GetBool("Press") == true)
                {
                    Debug.Log("Ingresa al while");
                    yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
                    botonElPalmar.SetBool("Press", false); // LUEGO LA DESACTIVA
                }
                yield break; //FINALIZA LA COURRUTINE
        }

        private IEnumerator EncenderYApagarAnimacionBotonCerro()
        {
                Debug.Log("Enciende Courrutine");
                botonCerro.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
                while (botonCerro.GetBool("Press") == true)
                {
                    Debug.Log("Ingresa al while");
                    yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
                    botonCerro.SetBool("Press", false); // LUEGO LA DESACTIVA
                }
                yield break; //FINALIZA LA COURRUTINE
        }

        private IEnumerator EncenderYApagarAnimacionBotonCarpincho()
        {
                Debug.Log("Enciende Courrutine");
                botonCarpincho.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
                while (botonCarpincho.GetBool("Press") == true)
                {
                    Debug.Log("Ingresa al while");
                    yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
                    botonCarpincho.SetBool("Press", false); // LUEGO LA DESACTIVA
                }
                yield break; //FINALIZA LA COURRUTINE
        }

        private IEnumerator EncenderYApagarAnimacionBotonPinguino()
        {
                Debug.Log("Enciende Courrutine");
                botonPinguino.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
                while (botonPinguino.GetBool("Press") == true)
                {
                    Debug.Log("Ingresa al while");
                    yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
                    botonPinguino.SetBool("Press", false); // LUEGO LA DESACTIVA
                }
                yield break; //FINALIZA LA COURRUTINE
        }

        private IEnumerator EncenderYApagarAnimacionBotonLlama()
        {
                Debug.Log("Enciende Courrutine");
                botonLlama.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
                while (botonLlama.GetBool("Press") == true)
                {
                    Debug.Log("Ingresa al while");
                    yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
                    botonLlama.SetBool("Press", false); // LUEGO LA DESACTIVA
                }
                yield break; //FINALIZA LA COURRUTINE
        }
//CIERRE DE COURUTINES


}
