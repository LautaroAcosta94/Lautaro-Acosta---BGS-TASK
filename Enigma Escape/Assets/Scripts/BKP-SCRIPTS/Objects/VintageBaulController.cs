using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VintageBaulController : MonoBehaviour
{
    public Animator baul;
    public GameObject sonidoBaulAbierto;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Open()
    {
        baul.SetBool("Open", true);
        sonidoBaulAbierto.SetActive(true);
    }
}
