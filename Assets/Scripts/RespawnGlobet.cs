using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGlobet : MonoBehaviour
{
    AudioSource fuenteAudio;
    public AudioClip lightGlobet, GlobetBurning;
    [SerializeField]
    private bool onFire, combustion;
    [SerializeField]
    private float delay, current_time, soundDistance;
    private Transform player;
    void Start()
    {
        soundDistance = 30.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        delay = 1.0f;           //ajustar para que suenen seguidos
        onFire = combustion = false;
        fuenteAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !onFire)
        {
            Debug.Log("Audio Enter!!!!");
            //Set animator ON!!!!!!!!!!!!!!!
            GetComponent<Animator>().SetBool("Flame", true);

            //     if (!onFire)
            //   {
            //delay += Time.deltaTime;
            //float current_time = delay - Time.deltaTime;
            onFire = true;
                fuenteAudio.clip = lightGlobet;
                fuenteAudio.Play();

           // }
        }
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       if (onFire && fuenteAudio.clip == lightGlobet)
        {      
            //current_time = delay - Time.deltaTime;
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                fuenteAudio.Stop();//    if (fuenteAudio.clip == lightGlobet) fuenteAudio.Stop();
                Debug.Log("Audio Stop Enter!!!!!!!!!");
                combustion = true;
            }

            /*fuenteAudio.clip = GlobetBurning;
            fuenteAudio.Play();*/
        }

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (combustion && distToPlayer < soundDistance)
        {
            if (current_time <= 0)
            {
                fuenteAudio.clip = GlobetBurning;
                fuenteAudio.Play();
                current_time += 10;
            }
            else current_time -= Time.deltaTime;
        }
        else if (distToPlayer > soundDistance && fuenteAudio.clip == GlobetBurning)
        {
            current_time = 0;
            fuenteAudio.Stop();
        }
    }
}