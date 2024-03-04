using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    private TypeOfRocks Rocks;
    private GameObject[] childsOfLargeRock;

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
            foreach (Transform child in transform)
            { child.gameObject.SetActive(false); }
            Rocks = TypeOfRocks.largeRock;
            //**************************************A CHANGER***********************************
            //childsOfLargeRock = GetComponentsInChildren<GameObject>();

            //foreach (GameObject mediumRocks in childsOfLargeRock)
            //{
            //    mediumRocks.SetActive(false);
            //}
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
                //**************************************A CHANGER***********************************
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                gameObject.SetActive(false);
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
