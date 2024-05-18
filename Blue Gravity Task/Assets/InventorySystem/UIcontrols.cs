using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontrols : MonoBehaviour
{
    public GameObject inventory;
    public GameObject market;
    bool inventoryOpen = false;
    public bool nextToTheMarket = false;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nextToTheMarket == false)
        {
            if (inventoryOpen == false && Input.GetKeyDown(KeyCode.E))
            {
                inventory.SetActive(true);
                inventoryOpen = true;
                GetComponent<PlayerMovement>().enabled = false; // Desactiva el movimiento del jugador
            }
            else if (inventoryOpen == true && Input.GetKeyDown(KeyCode.E))
            {
                inventory.SetActive(false);
                inventoryOpen = false;
                GetComponent<PlayerMovement>().enabled = true;
            }
        }
        else
        {
            OpenMarketMenu();
        }
    }

    void OpenMarketMenu()
    {
        if (inventoryOpen == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Abriste el menu del mercado");
            market.SetActive(true);
            inventory.SetActive(true);
            inventoryOpen = true;
            GetComponent<PlayerMovement>().enabled = false; // Desactiva el movimiento del jugador         
        }
        else if (inventoryOpen == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Cerraste el menu del mercado");
            market.SetActive(false);
            inventory.SetActive(false);
            inventoryOpen = false;
            GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Market"))
        {
            nextToTheMarket = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Market"))
        {
            nextToTheMarket = false;
        }
    }
}
