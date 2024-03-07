using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bigBullet, smallBullet, turret;
    [SerializeField] private float touretMaxRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    
    void Update()
    {

        Vector3 tempMousePosition = Input.mousePosition;
        tempMousePosition.z = -Camera.main.transform.position.z; 

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(tempMousePosition);
        mousePos.z = 0;

        if (mousePos.y > touretMaxRotation)
        {
            turret.transform.LookAt(mousePos, Vector3.up);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !GameObject.Find("Big Bullet(Clone)"))
        {
            Instantiate(bigBullet, gameObject.transform.position + new Vector3(1,0,0), Quaternion.Euler(0,0,90));
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Instantiate(smallBullet,turret.transform.position + turret.transform.forward * 0.8f, turret.transform.rotation);
        }
    }
}
