using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;
    Vector3 vOffSet = Vector3.zero;
    void Start()
    {
        vOffSet = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,target.transform.position + vOffSet,
            Time.deltaTime * speed);
    }
}
