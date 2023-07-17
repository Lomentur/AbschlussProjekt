using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    //list of spells to spawn
    public GameObject SpellSpawnLocation;
    public List<GameObject> objects;

    public void Spawn(string objectName)
    {
        //activate item that corresponds to a specific name
        foreach (var item in objects)
        {
            //item.SetActive(objectName == item.name);
            //Summon item at player camera
            Instantiate(item, position:SpellSpawnLocation.transform.position, rotation:SpellSpawnLocation.transform.rotation);//position:/rotation: to not child object, only use their values
        }
    }
}
