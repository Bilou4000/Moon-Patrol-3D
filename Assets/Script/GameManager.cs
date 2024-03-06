using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private float smallCraterSize, bigCraterSize;
    private GameObject[] allFloor;
    private GameObject lastFloor;
    private MapState mapState;
    private MapState[] floorDifficulty;
    private float time, nextActionTime, period;

    private void Start()
    {
        period = 10;
    }

    private void Update()
    {
        allFloor = GameObject.FindGameObjectsWithTag("Ground");
        time = Time.time;

        //if(time >= 0 && time < 10)
        //{
        //    mapState = (MapState)Random.Range(0, 3);

        //    floorDifficulty = { };
        //}
        if (time > nextActionTime)
        {
            nextActionTime += period;
            // execute block of code here
        }

        //difficulty easy : if(this difficulty then -->)
        //if not do an edless with increasing difficulty


        if (allFloor.Length < 20)
        {
            mapState = (MapState)Random.Range(0, 3);

            if (mapState == MapState.NoCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x, -1.4f, -0.5f), transform.rotation);
            }
            else if (mapState == MapState.SmallCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x + smallCraterSize, -1.4f, -0.5f), transform.rotation);
            }
            else if (mapState == MapState.BigCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x + bigCraterSize, -1.4f, -0.5f), transform.rotation);
            }
        }
    }

    public enum MapState
    {
        SmallCrater,
        BigCrater,
        NoCrater
    }
}
