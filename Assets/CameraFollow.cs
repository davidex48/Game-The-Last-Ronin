using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float cameraSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {

    }
    private void LateUpdate()
    {
        float posX = Mathf.Lerp(transform.position.x, player.transform.position.x
            , Time.deltaTime * cameraSpeed);
        float posY = Mathf.Lerp(transform.position.y, player.transform.position.y
            , Time.deltaTime * cameraSpeed);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {

    }
}