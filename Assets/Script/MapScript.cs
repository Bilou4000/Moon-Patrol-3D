using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] private GameObject floor, player, smallRock, mediumRock, largeRock;
    [SerializeField] private float smallCraterSize, bigCraterSize;
    [SerializeField] private int smallCraterPercent, bigCraterPercent, smallRockChances,mediumRockChances, largeRockChances;
    bool canRock;
    private GameObject[] allFloor, allSmallRocks, allMediumRocks;
    private GameObject lastFloor, oldestFloor;
    private MapState mapState;
    private MapState[] floorDifficulty;
    private float time, nextActionTime, period;


    private void Start()
    {
        period = 10;
        //mapState = (MapState)3;
    }

    private void Update()
    {
        allFloor = GameObject.FindGameObjectsWithTag("Ground");
        allSmallRocks = GameObject.FindGameObjectsWithTag("SmallRock");
        allMediumRocks = GameObject.FindGameObjectsWithTag("MediumRock");
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

        if (player.transform.position.x < allFloor.Last().transform.position.x) 
        {
            NextFloor();
        }       

    }

    private void NextFloor()
    {
        oldestFloor = allFloor.First();
        RandomFloor();
        if (allFloor.Length < 20)
        {


            if (mapState == MapState.NoCrater)
            {
                

                lastFloor = allFloor.Last();
                
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x, -1.4f, -0.5f), transform.rotation);
                if (canRock)
                {
                    Instantiate(RandomRock(), new Vector3(lastFloor.transform.position.x, lastFloor.transform.position.y + 2, lastFloor.transform.position.z), Quaternion.identity);
                    canRock = false;
                }
                canRock = true;
                RandomFloor();
            }
            else if (mapState == MapState.SmallCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x + smallCraterSize, -1.4f, -0.5f), transform.rotation);
                canRock = false;
                RandomFloor();
            }
            else if (mapState == MapState.BigCrater)
            {
                lastFloor = allFloor.Last();
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x + bigCraterSize, -1.4f, -0.5f), transform.rotation);
                canRock = false;
                RandomFloor();
            }
        }
        else if (allFloor.Length >= 20 && Mathf.Abs(oldestFloor.transform.position.x - player.transform.position.x) > 100)
        {
            Destroy(oldestFloor);
            
        }

    }
    private void RandomFloor()
    {
        int rnd = Random.Range(0, 101);
       
        if(rnd < smallCraterPercent)
        {
            mapState = (MapState)0;
            
        }
        else if (smallCraterPercent < rnd && rnd < (smallCraterPercent + bigCraterPercent))
        {
            mapState = (MapState)1;
            
        }
        else if(rnd > (smallCraterPercent + bigCraterPercent))
        {
            mapState = (MapState)2;
            
        }
    }

    private GameObject RandomRock()
    {
        int rnd = Random.Range(0, 101);
        if (rnd < smallRockChances)
        {
            return smallRock;

        }
        else if (smallRockChances < rnd && rnd < (smallRockChances + mediumRockChances))
        {
            return mediumRock;

        }
        else if (rnd > smallRockChances + mediumRockChances && rnd <(smallRockChances +largeRockChances + mediumRockChances))
        {
            return largeRock;
        }
        else if (rnd > (smallRockChances + largeRockChances + mediumRockChances))
        {
            return null;

        }
        return null;
    }

    public enum MapState
    {
        SmallCrater,
        BigCrater,
        NoCrater
    }
}
