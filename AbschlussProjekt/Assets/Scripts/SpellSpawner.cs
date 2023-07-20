/*
* File: SpellSpawner.cs
*Date: 20.07.2023
*Author: L.Ritter
*/
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    //spawn location for spells
    public GameObject SpellSpawnLocation;
    //list of spells that can spawn
    public List<GameObject> objects;

    public void Spawn(string objectName)
    {
        //activate item that corresponds to a specific name
        foreach (var item in objects)
        {
            //item.SetActive(objectName == item.name);
            //Summon item at player camera
            if(objectName == item.name)
            {
                Instantiate(item, position:SpellSpawnLocation.transform.position, rotation:SpellSpawnLocation.transform.rotation);//position:/rotation: to not child object, only use their values
            }
        }
    }
}
