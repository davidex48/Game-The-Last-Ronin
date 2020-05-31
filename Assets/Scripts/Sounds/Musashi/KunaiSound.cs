using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiSound : MonoBehaviour
{
    public GameObject player;
    public AudioClip throwKunai;
    AudioSource fuenteAudio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fuenteAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float stamineCost = player.GetComponent<Bullet>().stamineShotCost;
        float currentStamine = player.GetComponent<BetterMovement>().stamine;

        if (Input.GetButtonDown("Fire1") && currentStamine >= stamineCost)
        { //&& Bullet.canShoot) {
            fuenteAudio.clip = throwKunai;
            fuenteAudio.Play();
        }
    }
}
