using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowmoSound : MonoBehaviour
{
    public GameObject player;
    public AudioClip slowOn, slowOff;
    AudioSource fuenteAudio;
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        flag = true;
        player = GameObject.FindGameObjectWithTag("Player");
        fuenteAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        bool _canSlowTime = player.GetComponent<BetterMovement>().canSlowTime;
        bool _timeSlowed = player.GetComponent<BetterMovement>().timeSlowed;

        if (Input.GetButtonDown("Fire3") && _canSlowTime && !_timeSlowed)
        {
            flag = true;
            fuenteAudio.clip = slowOn;
            fuenteAudio.Play();
        }
        if ((Input.GetButtonDown("Fire3") && _timeSlowed) || !_canSlowTime && flag){
            flag = false;
            fuenteAudio.Stop();
            fuenteAudio.clip = slowOff;
            fuenteAudio.Play();
            
        }
    }
}