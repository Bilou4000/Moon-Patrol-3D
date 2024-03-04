using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockScript : MonoBehaviour
{
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
            if (collision.gameObject.CompareTag("BigBullet"))
            {
                //Instantiate ? or set active true ? 2 medium rocks
                //destroy or set active false gameobject
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
