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
        if(transform.position.x < PlayerMovement.instance.transform.position.x)
        {
            transform.position += transform.forward * speed * 1.5f * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.LoseLife(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("BigBullet"))
        {
            Destroy(gameObject);
        }
    }
}
