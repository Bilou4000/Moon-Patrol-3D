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
        if (collision.gameObject.CompareTag("MediumRock"))
        {
            PlayerManager.instance.SetScore(100);
        }

        if (collision.gameObject.CompareTag("LargeRock"))
        {
            PlayerManager.instance.SetScore(200);
        }

        if (collision.gameObject.CompareTag("Tank"))
        {
            PlayerManager.instance.SetScore(200);
        }

        if (collision.gameObject.CompareTag("UFO"))
        {
            PlayerManager.instance.SetScore(100);
        }

        Destroy(gameObject);
    }
}
