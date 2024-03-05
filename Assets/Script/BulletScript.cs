using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bigBulletSpeed, smallBulletSpeed, bigBulletDestructionTime;
    void Start()
    {
        if(gameObject.CompareTag("BigBullet"))
        {
            Invoke("Blast", bigBulletDestructionTime);
        }

        if (gameObject.CompareTag("SmallBullet"))
        {
            Invoke("Blast", bigBulletDestructionTime);
        }
    }

    
    void Update()
    {
        if(gameObject.CompareTag("BigBullet"))
        {
            transform.position += -(transform.up * bigBulletSpeed * Time.deltaTime);
        }

        if (gameObject.CompareTag("SmallBullet"))
        {
            transform.position += transform.up * smallBulletSpeed * Time.deltaTime;
        }

    }

    void Blast()
    {
        Destroy(gameObject);
    }
}
