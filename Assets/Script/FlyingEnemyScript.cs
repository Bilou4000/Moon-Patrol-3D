using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        //need to move constantly
        //transform.position += -transform.right * speed * Time.deltaTime;
    }
}
