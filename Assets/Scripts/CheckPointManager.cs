using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] Vector3 pos = new Vector3(-1, -1, -1);

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public Vector3 GetPos()
    {
        return pos;
    }

    public void SetPos(Vector3 newPos)
    {
        pos = newPos;
    }
}
