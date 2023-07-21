/*
* File: SpellCheck.cs
*Date: 20.07.2023
*Author: L.Ritter
*/
using UnityEngine;

public class SpellCheck : MonoBehaviour
{
    private string FireTag = "Fire"; //Feuer Tag
    private string ElectroTag = "Electro"; //Electro Tag
    private string EnergyTag = "Energy"; //Energie Tag
    private string ArenaTag = "Arena"; //Arena Tag
    //private int status;

    // speicherort für den kollidierten spell
    private GameObject collidedSpell; 
    // wird aufgerufen sobald ein Object den Trigger Collider betreten hat
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision!");
        // überprüfe ob das kollidierte Object einen spell Tag hat
        if (collision.gameObject.CompareTag(FireTag) || collision.gameObject.CompareTag(ElectroTag) || collision.gameObject.CompareTag(EnergyTag))
        {
            Debug.Log("TagFound");
            //speicher das kollidierte objekt
            collidedSpell = collision.gameObject;

            // hohl die tags der beiden objekte ab
            string shotProjTag = gameObject.tag;
            string collidedProjTag = collidedSpell.tag;

            // verfahre mit der kollision, basierent auf den tags

            if (shotProjTag == collidedProjTag) //wenn selber tag
            {
                Debug.Log("SameTag");
                DestroyBothObjects(); //zerstöre beides
            }
            else
            {            
                if(shotProjTag == FireTag && collidedProjTag == ElectroTag) //Feuer trifft Electro
                {
                    Destroy(collidedSpell);//zerstöre electro
                    Debug.Log(gameObject + " destroyed electro");
                }
                else if(shotProjTag == ElectroTag && collidedProjTag == EnergyTag) //Electro trifft Energie
                {
                    Destroy(collidedSpell);//zerstöre energie
                    Debug.Log(gameObject + " destroyed energy");
                }
                else if(shotProjTag == EnergyTag && collidedProjTag == FireTag) //Energie trifft Feuer
                {
                    Destroy(collidedSpell); //zerstöre feuer
                    Debug.Log(gameObject + " destroyed fire");
                }
                else //an sonsten, zerstöre dich selbst
                {
                    Destroy(gameObject);
                    Debug.Log(gameObject + " destroyed self");
                }
            }
        }
        // sicherheits abfrage ob die arena getroffen wurde
        else if (collision.gameObject.CompareTag(ArenaTag))
        {
            Destroy(gameObject);
            Debug.Log(gameObject + " destroyed self");
        }
    }
    
    private void DestroyBothObjects() //methode zum zerstören beider Objekte
    {
        Destroy(gameObject); //zerstöre geschossenen spell
        Debug.Log(gameObject + " was destroyed");
        Destroy(collidedSpell); //zerstöre kollidierten spell
        Debug.Log(collidedSpell + " was destroyed");
    }
}