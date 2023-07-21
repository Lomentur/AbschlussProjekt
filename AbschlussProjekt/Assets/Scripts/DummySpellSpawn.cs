/*
* File: DummySpellSpawn.cs
*Date: 20.07.2023
*Author: L.Ritter
*/
using System.Collections.Generic;
using UnityEngine;

public class DummySpellSpawn : MonoBehaviour
{
    //spawn ort der spells
    public GameObject SpellSpawnLocation;
    //liste der spawnbaren spells
    public List<GameObject> objects;
    //position des spielers
    public Transform playerPos;
    //voreingestellter RNG wert
    private int RNG = 1;
    //voreingestellter timer wert
    public float timer = 3f;
    
    void Update()   //wird jeden frame aufgerufen
    {
        //wenn zeit höher als 0 ist
        if(timer>0)
       {
        //setze die zeit runter
        timer-=Time.deltaTime;
       }
       //wenn zeit weniger oder gleich 0 ist
       if(timer <= 0)
       {
        //resette den timer
        timer = 3;
        //hohl eine zufalls zahl
        randomizer();
        //switch case mit zufallszahl
        switch (RNG)
        {
            //spawn Spell an spell spawn ort, je nach case
            case 1:
            Instantiate(objects[0],position:SpellSpawnLocation.transform.position, rotation:SpellSpawnLocation.transform.rotation);
            break;
            case 2:
            Instantiate(objects[1],position:SpellSpawnLocation.transform.position, rotation:SpellSpawnLocation.transform.rotation);
            break;
            case 3:
            Instantiate(objects[2],position:SpellSpawnLocation.transform.position, rotation:SpellSpawnLocation.transform.rotation);
            break;
            default: Debug.Log("default");
            break;
        }
       }
    }
    //randomizer methode
    public int randomizer()
    {
        //setze die reichweite von RNG zwischen 1 und 3
        RNG = Random.Range(1,4);
        Debug.Log(RNG);
        //gieb die zufällige zahl zurück
        return RNG;
    }
}
