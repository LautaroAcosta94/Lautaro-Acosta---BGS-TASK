using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarPartida : MonoBehaviour
{
    public Animator crossfade;
    public GameObject cargando;

    public void Iniciar()
    {
        Time.timeScale = 1.0f;
        StartCoroutine("Transicion"); 
    }

    public void Salir()
    {
        Application.Quit();
    }

    IEnumerator Transicion()
    {
        crossfade.SetBool("Open", true);
        cargando.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Mi Argentina v2");
    }
}
