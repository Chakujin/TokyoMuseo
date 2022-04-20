using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasos : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip sonidoPasos;
    
    void PlaySteps()
    {
        audioSource.PlayOneShot(sonidoPasos);
    }
}
