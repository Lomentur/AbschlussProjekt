/*
* File: DummySpellSpawn.cs
*Date: 20.07.2023
*Author: L.Ritter
*/
using System.Collections.Generic;
using UnityEngine;

public class DummySpellSpawn : MonoBehaviour
{
    //spawn location for spells
    public GameObject SpellSpawnLocation;
    //list of spells that can spawn
    public List<GameObject> objects;
    public Transform playerPos;
    //default value for RNG
    private int RNG = 1;
    //default value for timer
    public float timer = 3f;
    
    void Update()
    {
        //if timer is higher than 0
        if(timer>0)
       {
        //decrease time
        timer-=Time.deltaTime;
       }
       //if timer is smaller equal to 0
       if(timer <= 0)
       {
        //reset timer
        timer = 3;
        //call randomizer method
        randomizer();
        //switch case with RNG Number
        switch (RNG)
        {
            //spawn Spell depending on case
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
    //randomizer method
    public int randomizer()
    {
        //set RNG to a random number between 1 and 3
        RNG = Random.Range(1,4);
        Debug.Log(RNG);
        //return RNG with random number
        return RNG;
    }
}
