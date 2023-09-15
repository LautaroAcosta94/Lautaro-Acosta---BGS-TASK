using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{

    public float raycastDistance = 8f;
    public Camera fpsCam;
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object implements the IInteractable interface
            IInteractable interactable = hitObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // If the hit object is interactable, call its Interact method
                interactable.Interact();
            }
        }
    }
}
