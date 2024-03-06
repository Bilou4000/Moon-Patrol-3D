using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    private GameObject[] allFloor;

    private void Update()
    {
        allFloor = GameObject.FindGameObjectsWithTag("Ground");

        //difficulty easy : if(this difficulty then -->)
        //if not do an edless with increasing difficulty
    }
}
