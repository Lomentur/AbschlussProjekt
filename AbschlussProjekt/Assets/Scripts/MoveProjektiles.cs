using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjektiles : MonoBehaviour
{
    //variablen deklaration
    public GameObject projektile;
    public float speed;
    public float x = 0, y = 0, z=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        projektile.transform.position = projektile.transform.position + new Vector3(x,y,z) * speed/100;    
    }
}
