using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private float smallCraterSize, bigCraterSize;
    private new List <GameObject> allFloor;
    private GameObject lastFloor;
    private MapState mapState;
    private MapState[] floorDifficulty;
    private float time, nextActionTime, period;


    private void Start()
    {
        period = 10;
        GameObject firstFloor = GameObject.Find("Floor2");
        allFloor.Add(firstFloor);
        //mapState = (MapState)3;
    }

    private void Update()
    {
        
        time = Time.time;

        Debug.Log(mapState.ToString());
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

        NextFloor();

    }

    private void NextFloor()
    {
        RandomFloor();
        if (allFloor.Count < 20)
        {


            if (mapState == MapState.NoCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                allFloor.Add(Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x, -1.4f, -0.5f), transform.rotation));
                mapState = (MapState)RandomFloor();
            }
            else if (mapState == MapState.SmallCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                allFloor.Add(Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x + smallCraterSize, -1.4f, -0.5f), transform.rotation));
                mapState = (MapState)RandomFloor();
            }
            else if (mapState == MapState.BigCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                allFloor.Add(Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x + bigCraterSize, -1.4f, -0.5f), transform.rotation));
                mapState = (MapState)RandomFloor();
            }
        }
        
    }
    private int RandomFloor()
    {

        return Random.Range(0, 3);
    }
    public enum MapState
    {
        SmallCrater,
        BigCrater,
        NoCrater
    }
}
