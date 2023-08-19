using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajones : MonoBehaviour
{
    public float interaccionDistancia = 2.0f;
    private ArmarioController armarioController;

    private void Start()
    {
        armarioController = FindObjectOfType<ArmarioController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DetectarInteractuables();
        }
    }

    private void DetectarInteractuables() //el nombre del método es lo mismo, esto es a modo de test, hay que pasarlo luego a tu codigo
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interaccionDistancia))
        {
            ArmarioController armario = hit.collider.GetComponentInParent<ArmarioController>();
            if (armario != null && armario == armarioController)
            {
                if (armario.isOpen)
                {
                    armario.CerrarPuerta();
                }
                else
                {
                    armario.AbrirPuerta();
                }
            }
        }
    }
}