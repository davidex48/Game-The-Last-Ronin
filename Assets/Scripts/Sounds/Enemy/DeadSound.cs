using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSound : MonoBehaviour
{
    public AudioClip deadSound;
    AudioSource fuenteAudio;

    float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        timeToDestroy = 8.5f;
        fuenteAudio.clip = deadSound;
    }

    // Update is called once per frame
    void Update()
    {
        /*fuenteAudio.clip = deadSound;
        fuenteAudio.Play();*/
        timeToDestroy -= Time.fixedDeltaTime;
        if (timeToDestroy <= 0) Destroy(this.gameObject);       // Destroy(gameObject, bulletLife); Asi no hace falta timer
    }
}
