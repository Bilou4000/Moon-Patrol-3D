using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bigBullet, smallBullet, turret;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    
    void Update()
    {
        Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //turret.transform.rotation = Quaternion.Euler(0, 0, aim.y * aim.x) ;
        turret.transform.LookAt(aim);   
        if(Input.GetKeyUp(KeyCode.Mouse0) && !GameObject.Find("Big Bullet(Clone)"))
        {
            Instantiate(bigBullet, gameObject.transform.position + new Vector3(1,0,0), Quaternion.Euler(0,0,90));
        }
    }
}
