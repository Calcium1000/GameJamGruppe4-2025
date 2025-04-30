using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip[] sampleHits;
    System.Random rand = new System.Random(); //RNG used for playing random sound from list

    private static SFXManager instance;

    // Makes the game object persist between scenes
    private void Awake()
    {
        
        if (instance == null) //Makes the class a singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        sampleHits = Resources.LoadAll<AudioClip>("sampleSfx");
    }

    public void PlayRandomSFX()
    {
        SFXSource.PlayOneShot(sampleHits[rand.Next(sampleHits.Length)]);
    }
}