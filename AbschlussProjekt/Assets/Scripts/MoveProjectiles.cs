/*
* File: MoveProjectiles.cs
*Date: 20.07.2023
*Author: L.Ritter
*/
using System.Collections;
using UnityEngine;

public class MoveProjectiles : MonoBehaviour
{
    //deklaration
    float seconds = 0.5f;
    // Awake is called once the skript is called upon
    // in this case, once a projektile is summoned
    void Awake() //Awake wird aufgerufen sobald das skript aufgerufen wird
    {
        //warte
        WaitTime(); 
        // stell den collider an
        GetComponent<Collider>().enabled = true;
    }
    //Warte Methode
    IEnumerator WaitTime()
    {
        //warte die angegebene zeit
        yield return new WaitForSeconds(seconds);
    }
    void Update() //wird jeden frame aufgerufen
    {
        //bewege den spell forwärts
        GetComponent<Rigidbody>().AddForce(transform.forward);
    }
}
