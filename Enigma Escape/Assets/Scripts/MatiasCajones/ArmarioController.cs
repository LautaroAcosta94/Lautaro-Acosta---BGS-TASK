using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmarioController : MonoBehaviour
 {
    public UnityEvent onAbierto;
    public UnityEvent onCerrado;

    private Animator animator;
    public bool isOpen = false; //check de puerta

    private void Start()
    {
        animator = GetComponentInParent<Animator>(); //se accede a la bisagra en este caso que quedó como padre, si n se cambia por donde esté
                                                     //fue a modo de prueba
    }

    public void AbrirPuerta()
    {
        isOpen = true;
        animator.SetBool("Open", isOpen);
        onAbierto.Invoke();
    }

    public void CerrarPuerta()
    {
        isOpen = false;
        animator.SetBool("Open", isOpen);
        onCerrado.Invoke();
    }
}