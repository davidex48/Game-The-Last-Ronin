using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public Animator animator;
    public int index;
    public string levelName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
 
            //Cargar level amb build index
            SceneManager.LoadScene(index);
            //Cargar level pel nom
            SceneManager.LoadScene(levelName);
            //animator.SetTrigger("FadeOut");

            //Restart level
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }
    }
    public void OnFadeComplete()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}