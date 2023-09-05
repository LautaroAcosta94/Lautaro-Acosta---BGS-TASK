using UnityEngine;

public class AperturaPuertas : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 8f;
    Animator puertaAnim;

    void Update()
    {
        AperturaPuerta();
    }

    public void AperturaPuerta()
    {
        RaycastHit hitPuertas;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitPuertas, range))
        {
            if (hitPuertas.transform.CompareTag("PuertaArmario") || hitPuertas.transform.CompareTag("Cajon"))
            {
                puertaAnim = hitPuertas.transform.GetComponent<Animator>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (puertaAnim.GetBool("Open"))
                    {
                        puertaAnim.SetBool("Open", false);
                    }
                    else
                    {
                        puertaAnim.SetBool("Open", true);
                    }
                }
            }
        }
    }
}