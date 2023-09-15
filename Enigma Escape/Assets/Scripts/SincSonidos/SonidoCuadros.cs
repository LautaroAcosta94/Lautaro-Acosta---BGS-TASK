using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoCuadros : MonoBehaviour
{
    AudioSource aud;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void SonidoCuadroAbriendo(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }

    public void SonidoCuadrCerrando(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }
}
