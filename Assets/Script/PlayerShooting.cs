using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bigBullet, smallBullet, turret;
    [SerializeField] float smoothRotation;
    private Quaternion  yQuater, xQuater;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    
    void Update()
    {
        Vector3 aim = Input.mousePosition;

        //xQuater = new Quaternion((aim.x - smoothRotation) * Time.deltaTime, 0, 0, 1);
        //yQuater = new Quaternion(0, (aim.y + smoothRotation) * Time.deltaTime, 0, 1);
        //turret.transform.rotation = yQuater * xQuater * transform.rotation;
        turret.transform.rotation = Quaternion.Euler(0, 0, aim.x*smoothRotation) * Quaternion.Euler(0, 0, aim.y*smoothRotation);
        
        if(Input.GetKeyUp(KeyCode.Mouse0) && !GameObject.Find("Big Bullet(Clone)"))
        {
            Instantiate(bigBullet, gameObject.transform.position + new Vector3(1,0,0), Quaternion.Euler(0,0,90));
        }
    }
}
