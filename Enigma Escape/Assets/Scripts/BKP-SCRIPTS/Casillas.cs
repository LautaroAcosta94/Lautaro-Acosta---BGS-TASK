using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casillas : MonoBehaviour
{
    //VARIABLE DE ACTIVACION
    public bool casillaLlena = false;

    //VARIABLE DE INVENTARIO
    public GameObject player;
    public GameObject objeto;

    public int n;

    //VARIABLE DE CANVAS
    public GameObject[] canvasObjetos;

    // Update is called once per frame
    void Update()
    {
  
        objeto = player.GetComponent<ToolbeltV2>().objetosEnInventario[n];


        if(casillaLlena == true)
        {
            if(objeto.GetComponent<IdentificadorDeObjetos>().pilas == true)
            {
               canvasObjetos[0].SetActive(true);   
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().buenosAires == true)
            {
                canvasObjetos[1].SetActive(true);              
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().catamarca == true)
            {
                canvasObjetos[2].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().chaco == true)
            {
                canvasObjetos[3].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().chubut == true)
            {
                canvasObjetos[4].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().cordoba == true)
            {
                canvasObjetos[5].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().corrientes == true)
            {
                canvasObjetos[6].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().entreRios == true)
            {
                canvasObjetos[7].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().formosa == true)
            {
                canvasObjetos[8].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().gorroEscudo == true)
            {
                canvasObjetos[9].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().jujuy == true)
            {
                canvasObjetos[10].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().laPampa == true)
            {
                canvasObjetos[11].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().laRioja == true)
            {
                canvasObjetos[12].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().laurelesEscudo == true)
            {
                canvasObjetos[13].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().llaveArmario == true)
            {
                canvasObjetos[14].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().llaveCajon == true)
            {
                canvasObjetos[15].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().llaveDormitorio == true)
            {
                canvasObjetos[16].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().manosEscudo == true)
            {
                canvasObjetos[17].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().mate == true)
            {
                canvasObjetos[18].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().mendoza == true)
            {
                canvasObjetos[19].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().misiones == true)
            {
                canvasObjetos[20].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().neuquen == true)
            {
                canvasObjetos[21].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().ovaloEscudo == true)
            {
                canvasObjetos[22].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().picaEscudo == true)
            {
                canvasObjetos[23].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().rioNegro == true)
            {
                canvasObjetos[24].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().salta == true)
            {
                canvasObjetos[25].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().sanJuan == true)
            {
                canvasObjetos[26].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().sanLuis == true)
            {
                canvasObjetos[27].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().santaCruz == true)
            {
                canvasObjetos[28].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().santaFe == true)
            {
                canvasObjetos[29].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().santiagoDelEstero == true)
            {
                canvasObjetos[30].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().solEscudo == true)
            {
                canvasObjetos[31].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().tierraDelFuego == true)
            {
                canvasObjetos[32].SetActive(true);          
            }

            if(objeto.GetComponent<IdentificadorDeObjetos>().tucuman == true)
            {
                canvasObjetos[33].SetActive(true);          
            }
        }
        else
        {
            canvasObjetos[0].SetActive(false);
            canvasObjetos[1].SetActive(false);
            canvasObjetos[2].SetActive(false);
            canvasObjetos[3].SetActive(false);
            canvasObjetos[4].SetActive(false);
            canvasObjetos[5].SetActive(false);
            canvasObjetos[6].SetActive(false);
            canvasObjetos[7].SetActive(false);
            canvasObjetos[8].SetActive(false);
            canvasObjetos[9].SetActive(false);
            canvasObjetos[10].SetActive(false);
            canvasObjetos[11].SetActive(false);
            canvasObjetos[12].SetActive(false);
            canvasObjetos[13].SetActive(false);
            canvasObjetos[14].SetActive(false);
            canvasObjetos[15].SetActive(false);
            canvasObjetos[16].SetActive(false);
            canvasObjetos[17].SetActive(false);
            canvasObjetos[18].SetActive(false);
            canvasObjetos[19].SetActive(false);
            canvasObjetos[20].SetActive(false);
            canvasObjetos[21].SetActive(false);
            canvasObjetos[22].SetActive(false);
            canvasObjetos[23].SetActive(false);
            canvasObjetos[24].SetActive(false);
            canvasObjetos[25].SetActive(false);
            canvasObjetos[26].SetActive(false);
            canvasObjetos[27].SetActive(false);
            canvasObjetos[28].SetActive(false);
            canvasObjetos[29].SetActive(false);
            canvasObjetos[30].SetActive(false);
            canvasObjetos[31].SetActive(false);
            canvasObjetos[32].SetActive(false);
            canvasObjetos[33].SetActive(false);
        }
    }

}
