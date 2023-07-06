using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public string FireTag; //Tag of the first projectile
    public string ElectroTag; //Tag of the second projectile
    public string EnergyTag; //Tag of the third projectile
    private int status;

    private GameObject collidedSpell; // Store the collided object

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a projectile tag
        if (collision.gameObject.CompareTag(FireTag) || collision.gameObject.CompareTag(ElectroTag) || collision.gameObject.CompareTag(ElectroTag))
        {
            collidedSpell = collision.gameObject; // Store the collided object

            // Get the tags of the collided projectiles
            string shotProj = gameObject.tag;
            string collidedProj = collidedSpell.tag;

            // Handle the collision based on the projectile tags
            if (shotProj == collidedProj) //if same tag
            {
                DestroyBothObjects(); //destroy both spells
            }
            switch (status)
            {
                case 0 when shotProj == FireTag && collidedProj == ElectroTag: //fire hits electro
                DestroyObject(collidedSpell);//destroy electro
                break;

                case 1 when shotProj == ElectroTag && collidedProj == FireTag: //electro hits fire
                DestroyObject(gameObject);//destroy electro
                break;

                case 2 when shotProj == FireTag && collidedProj == EnergyTag: //Fire hits Energy
                DestroyObject(gameObject);//destroy fire
                break;

                case 3 when shotProj == EnergyTag && collidedProj == FireTag: //energy hits fire
                DestroyObject(collidedSpell); //destroy fire
                break;

                case 4 when shotProj == EnergyTag && collidedProj == ElectroTag: //energy hits electro
                DestroyObject(gameObject);//destroy energy
                break;
                
                case 5 when shotProj == ElectroTag && collidedProj == EnergyTag: //electro hits energy
                DestroyObject(collidedSpell);//destroy energy
                break;
                
                default:
                DestroyBothObjects(); //destroy both spells
                break;
            }
        }
    }

    private void DestroyBothObjects() //method for destroying both spells
    {
        DestroyObject(gameObject); //destroy shot projectile
        DestroyObject(collidedSpell); //destroy hit projectile
    }

    private void DestroyObject(GameObject obj) //method for destroying target projectile
    {
        Destroy(obj); //destroy projectile
    }
}