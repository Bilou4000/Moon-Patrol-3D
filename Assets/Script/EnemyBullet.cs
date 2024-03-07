using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject mine;
    [SerializeField] private float bulletSpeed, positionOfMine;
    private GameObject bulletLocation;
    private PlayerMovement thePlayer;
    private Vector3 pointToGoTo;

    private bool hasTouchedPoint;

    private void Start()
    {
        thePlayer = PlayerMovement.instance;
        bulletLocation = GameObject.Find("BulletTurnPoint");
    }
    void Update()
    {
        if (!hasTouchedPoint)
        {
            transform.LookAt(pointToGoTo);
            pointToGoTo = bulletLocation.transform.position;
        }

        if (Vector3.Distance(transform.position, pointToGoTo) < 0.1f)
        {
            hasTouchedPoint = true;

            transform.rotation = Quaternion.Euler(90,0,0);
            pointToGoTo = thePlayer.transform.position + new Vector3(positionOfMine, -1, 0);
        }

        transform.position = Vector3.MoveTowards(transform.position, pointToGoTo,
            bulletSpeed * thePlayer.GetMoveSpeed() * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.LoseLife(1);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(mine, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
