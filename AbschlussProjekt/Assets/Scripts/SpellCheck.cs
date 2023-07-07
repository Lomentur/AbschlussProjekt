using UnityEngine;

public class SpellCheck : MonoBehaviour
{
    private string FireTag = "Fire"; //Tag of the first projectile
    private string ElectroTag = "Electro"; //Tag of the second projectile
    private string EnergyTag = "Energy"; //Tag of the third projectile
    private string ArenaTag = "Arena";
    private int status;

    private GameObject collidedSpell; // Store the collided object

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision!");
        // Check if the collided object has a projectile tag
        if (collision.gameObject.CompareTag(FireTag) || collision.gameObject.CompareTag(ElectroTag) || collision.gameObject.CompareTag(EnergyTag))
        {
            Debug.Log("TagFound");
            collidedSpell = collision.gameObject; // Store the collided object

            // Get the tags of the collided projectiles
            string shotProjTag = gameObject.tag;
            string collidedProjTag = collidedSpell.tag;

            // Handle the collision based on the projectile tags
            if (shotProjTag == collidedProjTag) //if same tag
            {
                Debug.Log("SameTag");
                DestroyBothObjects(); //destroy both spells
            }
            else
            {            
                if(shotProjTag == FireTag && collidedProjTag == ElectroTag) //Fire hits Electro
                {
                    Destroy(collidedSpell);//destroy electro
                    Debug.Log(gameObject + " destroyed electro");
                }
                else if(shotProjTag == ElectroTag && collidedProjTag == EnergyTag) //Electro hits Energy
                {
                    Destroy(collidedSpell);//destroy energy
                    Debug.Log(gameObject + " destroyed energy");
                }
                else if(shotProjTag == EnergyTag && collidedProjTag == FireTag) //Energy hits Fire
                {
                    Destroy(collidedSpell); //destroy fire
                    Debug.Log(gameObject + " destroyed fire");
                }
                else
                {
                    Destroy(gameObject);
                    Debug.Log(gameObject + " destroyed self");
                }
            }
        }
        else if (collision.gameObject.CompareTag(ArenaTag))
        {
            Destroy(gameObject);
            Debug.Log(gameObject + " destroyed self");
        }
    }
    
    private void DestroyBothObjects() //method for destroying both spells
    {
        Destroy(gameObject); //destroy shot projectile
        Debug.Log(gameObject + " was destroyed");
        Destroy(collidedSpell); //destroy hit projectile
        Debug.Log(collidedSpell + " was destroyed");
    }
}