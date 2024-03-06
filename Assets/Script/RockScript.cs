using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] private GameObject mediumRock;
    private TypeOfRocks Rocks;

    private void Start()
    {
        if (gameObject.CompareTag("SmallRock"))
        {
            Rocks = TypeOfRocks.smallRock;
        }
        if (gameObject.CompareTag("MediumRock"))
        {
            Rocks = TypeOfRocks.mediumRock;
        }
        if (gameObject.CompareTag("LargeRock"))
        {
            
            Rocks = TypeOfRocks.largeRock;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Rocks == TypeOfRocks.smallRock)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Call GameManagerLife
                //can't be destroyed
            }

        }
        if (Rocks == TypeOfRocks.mediumRock)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Call GameManagerLife
            }
            if (collision.gameObject.CompareTag("BigBullet"))
            {
                Destroy(gameObject);
            }
        }
        if (Rocks == TypeOfRocks.largeRock)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Call GameManagerLife
            }
            if (collision.gameObject.CompareTag("BigBullet"))
            {
                Instantiate(mediumRock, new Vector3(transform.position.x + 1.5f,transform.position.y - 1.5f,transform.position.z),Quaternion.identity );
                Instantiate(mediumRock, new Vector3(transform.position.x - 0.8f, transform.position.y - 1.5f, transform.position.z), Quaternion.identity);
                Destroy(gameObject) ;
            }
        }
    }
}


public enum TypeOfRocks
{
    smallRock,
    mediumRock,
    largeRock
}
