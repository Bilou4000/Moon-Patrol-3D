using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        transform.LookAt(new Vector3(PlayerMovement.instance.gameObject.transform.position.x, transform.position.y, transform.position.z));
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Call GameManager life player 
        }

        if (collision.gameObject.CompareTag("BigBullet"))
        {
            Destroy(gameObject);
        }
    }
}
