using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXVolume : MonoBehaviour
{
    [Range(0, 100)]
    public float volume = 100;

    public void Start()
    {
        if (GetComponent<Properties>() != null && GetComponent<Properties>().objectstate == Properties.objectState.state3)
            GetComponent<AudioSource>().volume = volume/100;
    }
}