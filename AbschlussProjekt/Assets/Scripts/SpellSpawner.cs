/*
* File: SpellSpawner.cs
*Date: 24.07.2023
*Author: L.Ritter
*/
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    //spawn ort der spells
    public GameObject SpellSpawnLocation;
    //giebt das GestureRecognizer Object an
    public GameObject GestureRec;
    //liste der spawnbaren spells
    public List<GameObject> spells;
    //methode zum spawnen der spells via UnityEvent
    public void Spawn()
    {
        //hohl dir das result der zeichenerkennung
        string result = GestureRec.GetComponent<GestureRecognizer>().GetResult();
        if (result == "Fire")//dreieck start bottom right to left
        {
            Instantiate(spells[0], position: SpellSpawnLocation.transform.position, rotation: SpellSpawnLocation.transform.rotation);
            Debug.Log("Fire!!!");
        }
        else if (result == "Electro") //cicle start bottom go clockwise
        {
            Instantiate(spells[1], position: SpellSpawnLocation.transform.position, rotation: SpellSpawnLocation.transform.rotation);
            Debug.Log("Electro!!!");
        }
        else if (result == "Energy") //lightning start top, go down left right left
        {
            Instantiate(spells[2], position: SpellSpawnLocation.transform.position, rotation: SpellSpawnLocation.transform.rotation);
            Debug.Log("Energy!!!");
        }
        else
        {Debug.Log("nothing!!!");}
    }
}