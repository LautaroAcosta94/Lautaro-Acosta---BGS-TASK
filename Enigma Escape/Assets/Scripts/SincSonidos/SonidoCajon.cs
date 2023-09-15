using UnityEngine;

public class SonidoCajon : MonoBehaviour
{
    AudioSource aud;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void SonidoCajonAbriendo(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }

    public void SonidoCajonCerrando(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }
}
