/*
* File: SpellSpawner.cs
*Date: 20.07.2023
*Author: L.Ritter
*/
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    //spawn ort der spells
    public GameObject SpellSpawnLocation;
    //liste der spawnbaren spells
    public List<GameObject> objects;
    //methode zum spawnen der spells via UnityEvent
    public void Spawn(string objectName)
    {
        //geh alle objekte in der liste durch
        foreach (var item in objects)
        {
            //wenn übereinstimmung zwischen hinterlegten spells und der liste besteht
            if(objectName == item.name)
            {
                //spawne den spell am angegebenen ort
                Instantiate(item, position:SpellSpawnLocation.transform.position, rotation:SpellSpawnLocation.transform.rotation);
            }
        }
    }
}
