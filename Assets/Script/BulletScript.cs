using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed, bulletDestructionTime;
    void Start()
    {
        Invoke("Blast", bulletDestructionTime);
    }

    
    void Update()
    {
        if(gameObject.CompareTag("BigBullet"))
        {
            transform.position += -(transform.up * bulletSpeed * PlayerMovement.instance.GetMoveSpeed() * Time.deltaTime);
        }

        if (gameObject.CompareTag("SmallBullet"))
        {
            transform.position += transform.forward * bulletSpeed * PlayerMovement.instance.GetMoveSpeed() * Time.deltaTime;
        }

    }

    void Blast()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
