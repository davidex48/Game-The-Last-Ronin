using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawSound : MonoBehaviour
{
    private Transform player;
    public AudioSource fuenteAudio;
    public AudioClip bladesSound;
    [SerializeField]
    private float delay, current_time, soundDistance;
    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        delay = 2.0f;
        current_time = 0.0f;
        soundDistance = 25.0f;
    }

    /*       current_time -= Time.deltaTime;

             if(current_time <= 0)
           {
               current_time += delay;
               fuenteAudio.clip = bladesSound;
               fuenteAudio.Play();
           }*/
  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        fuenteAudio.clip = bladesSound;
        fuenteAudio.Play();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        fuenteAudio.Stop();
    }*/
    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= soundDistance)
        {
            current_time -= Time.deltaTime;

            if (current_time <= 0)
            {
                current_time += delay;
                fuenteAudio.clip = bladesSound;
                fuenteAudio.Play();
            }
        }

        else if (fuenteAudio.clip == bladesSound) fuenteAudio.Stop();
    }
}
