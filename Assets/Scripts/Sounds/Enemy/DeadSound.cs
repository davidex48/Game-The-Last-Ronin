using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSound : MonoBehaviour
{
    AudioClip deadSound;
    AudioSource fuenteAudio;

    float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        timeToDestroy = 1.5f;
        fuenteAudio.clip = deadSound;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.fixedDeltaTime;
        if (timeToDestroy <= 0) Destroy(this.gameObject);       // Destroy(gameObject, bulletLife); Asi no ghace falta timer
    }
}
