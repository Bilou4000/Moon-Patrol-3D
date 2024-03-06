using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    [SerializeField] private float speed, CameraNearPlane;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private Vector3 targetPos;

    void Start()
    { 
        mainCamera = Camera.main;

        CalculateCameraBounds();
        targetPos = GetRandomPosition();
    }

    private void Update()
    {
        CalculateCameraBounds();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if(transform.position == targetPos)
        {
            targetPos = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, transform.position.z);
    }

    private void CalculateCameraBounds()
    {
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, CameraNearPlane)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, CameraNearPlane)).x;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, CameraNearPlane)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, CameraNearPlane)).y;
    }
}
