using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectiles : MonoBehaviour
{
    float seconds = 0.5f;
    // Awake is called once the skript is called upon
    // in this case, once a projektile is summoned
    void Awake()
    {
        WaitTime();
        GetComponent<Collider>().enabled = true;
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(seconds);
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward);
    }
}
