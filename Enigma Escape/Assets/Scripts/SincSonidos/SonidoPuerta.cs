using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPuerta : MonoBehaviour
{
    AudioSource aud;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void SonidoPuertaAbriendo(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }

    public void SonidPuertaCerrando(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }
}
