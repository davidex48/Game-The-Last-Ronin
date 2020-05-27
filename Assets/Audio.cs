using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{

    public AudioClip Dog;
    public float Volume;
    AudioSource audio;
    public bool alreadyPlayed = false;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            audio.PlayOneShot(Dog, Volume);
            alreadyPlayed = true;
        }
    }
}