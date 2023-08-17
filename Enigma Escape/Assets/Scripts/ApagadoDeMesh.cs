using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagadoDeMesh : MonoBehaviour
{
    [Header("Configuracion")]

    public GameObject meshObserv;

    void Update()
    {
       if(meshObserv.GetComponent<MeshRenderer>().enabled == false)
       {
            GetComponent<MeshRenderer>().enabled = false;
       }
       else
       {
            GetComponent<MeshRenderer>().enabled = true;
       } 
    }
}
