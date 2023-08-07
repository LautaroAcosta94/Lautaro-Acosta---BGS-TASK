using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    //VARIABLES QUE CONTROLAN LA ANIMACION DE LA AGUJA
    public Animator agujaRadio;

    public int n; //VARIABLE QUE IRA CAMBIANDO SU VALOR Y DETERMINARA LA EMISORA

    bool sistemaActivo = false;

    //VARIABLES PARA EL RAYCAST
    public Camera cam;
    public LayerMask mask;

    void Update()
    {
        raycastMouse();
        
    }

    void raycastMouse()
    {      
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if(Input.GetMouseButton(0))
        {
            agujaRadio.SetInteger("cambioDeEmisora", n);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                //LA IZQUIERDA CAMBIA A LA EMISORA ANTERIOR
                if(sistemaActivo == true)
                {
                    if(hit.transform.CompareTag("PerillaIzq"))
                    {
                        if(n >= 1)
                        {
                            n--;
                        }             
                    }
                }
                
                //LA DERECHA CAMBIA A LA EMISORA SIGUIENTE
                if(hit.transform.CompareTag("PerillaDer"))
                {
                    if(sistemaActivo == false)
                    {
                        sistemaActivo = true;
                    }
                    if(sistemaActivo = true)
                    {
                        if(n <= 8)
                        {
                            n++;
                        }    
                    }
                    
                }
            }
        }
    }


}
