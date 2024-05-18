using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontrols : MonoBehaviour
{
    public GameObject inventory;
    public GameObject market;
    public GameObject Message;

    public GameObject TextIntectIndicator;
    public GameObject TextIntectIndicatorTwo;

    bool inventoryOpen = false;
    bool nextToTheMarket = false;
    bool nextToTheMarketClosed = false;
    bool nextToSomeBuilding = false;
    bool MessageOpen = false;

    // Referencias a los componentes AudioSource para abrir y cerrar el inventario o el mercado
    public AudioSource openBag;
    public AudioSource closeBag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nextToSomeBuilding == false)
        {
            if (inventoryOpen == false && Input.GetKeyDown(KeyCode.E))
            {
                inventory.SetActive(true);
                inventoryOpen = true;
                GetComponent<PlayerMovement>().enabled = false; // Desactiva el movimiento del jugador
                openBag.Play(); // Reproduce el sonido de abrir
            }
            else if (inventoryOpen == true && Input.GetKeyDown(KeyCode.E))
            {
                inventory.SetActive(false);
                inventoryOpen = false;
                GetComponent<PlayerMovement>().enabled = true;
                closeBag.Play(); // Reproduce el sonido de cerrar
            }
        }
        else
        {
            MenuAndTextSystem();
        }
    }

    void MenuAndTextSystem()
    {
        if (nextToTheMarket == true)
        {
            if (inventoryOpen == false && Input.GetKeyDown(KeyCode.E))
            {
                market.SetActive(true);
                inventory.SetActive(true);
                inventoryOpen = true;
                GetComponent<PlayerMovement>().enabled = false; // Desactiva el movimiento del jugador
                openBag.Play(); // Reproduce el sonido de abrir
            }
            else if (inventoryOpen == true && Input.GetKeyDown(KeyCode.E))
            {
                market.SetActive(false);
                inventory.SetActive(false);
                inventoryOpen = false;
                GetComponent<PlayerMovement>().enabled = true;
                closeBag.Play(); // Reproduce el sonido de cerrar
            }
        }

        if(nextToTheMarketClosed == true)
        {
            if (MessageOpen == false && Input.GetKeyDown(KeyCode.E))
            {
                Message.SetActive(true);
                MessageOpen = true;
            }
            else if (MessageOpen == true && Input.GetKeyDown(KeyCode.E))
            {
                Message.SetActive(false);
                MessageOpen = false;
            }
        }
    }

    void OpenTextMessage()
    {
        if (inventoryOpen == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Abriste el menu del mercado");
          
        }
        else if (inventoryOpen == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Cerraste el menu del mercado");
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Market"))
        {
            TextIntectIndicator.SetActive(true);
            nextToTheMarket = true;
            nextToSomeBuilding = true;
        }

        if (other.CompareTag("ClosedMarket"))
        {
            TextIntectIndicatorTwo.SetActive(true);
            nextToTheMarketClosed = true;
            nextToSomeBuilding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Market"))
        {
            TextIntectIndicator.SetActive(false);
            nextToTheMarket = false;
            nextToSomeBuilding = false;
        }

        if (other.CompareTag("ClosedMarket"))
        {
            TextIntectIndicatorTwo.SetActive(false);
            nextToTheMarketClosed = false;
            nextToSomeBuilding = false;
        }
    }
}
