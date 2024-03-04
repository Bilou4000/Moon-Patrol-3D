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
    }

    
    void Update()
    {
        if(gameObject.CompareTag("BigBullet"))
        {
            transform.position += -(transform.up * bigBulletSpeed * Time.deltaTime);
        }
       
    }

    void Blast()
    {
        Destroy(gameObject);
    }
}
