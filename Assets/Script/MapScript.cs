using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] private GameObject floor, player;

    [Header("Difficulty Modifier")]
    [SerializeField] int craterDifficulty;
    [SerializeField] int rockDifficulty;

    [Header("Rock")]
    [SerializeField] private GameObject smallRock;
    [SerializeField] private GameObject mediumRock, largeRock, noRock;
    [SerializeField] private int smallRockChances, mediumRockChances, largeRockChances;
    float spawnHeight;

    [Header("Crater")]
    [SerializeField] private float smallCraterSize;
    [SerializeField] private float bigCraterSize;
    [SerializeField] private int smallCraterPercent, bigCraterPercent;

    bool canRock, increaseDifficulty;
    private GameObject[] allFloor;
    private GameObject lastFloor, oldestFloor;
    private MapState mapState;
    private MapState[] floorDifficulty;
    private float time, nextActionTime, period;

    public static MapScript instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        period = 10;
        //mapState = (MapState)3;
    }

    private void Update()
    {
        allFloor = GameObject.FindGameObjectsWithTag("Ground");
        time = Time.time;

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
        if (allFloor.Length < 40)
        {


            if (mapState == MapState.NoCrater)
            {
                

                lastFloor = allFloor.Last();
                
                MeshRenderer renderer = lastFloor.GetComponent<MeshRenderer>();
                Vector3 lastFloorSize = renderer.bounds.size;

                Instantiate(floor, new Vector3(lastFloor.transform.position.x + lastFloorSize.x, -1.4f, -0.5f), transform.rotation);
                if (canRock)
                {
                    Instantiate(RandomRock(), new Vector3(lastFloor.transform.position.x, lastFloor.transform.position.y + spawnHeight, lastFloor.transform.position.z), transform.rotation);
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
        else if (allFloor.Length >= 40 && Mathf.Abs(oldestFloor.transform.position.x - player.transform.position.x) > 100)
        {
            Destroy(oldestFloor);
            increaseDifficulty = !increaseDifficulty;
            if (increaseDifficulty)
            {
                smallCraterPercent += craterDifficulty;
                bigCraterPercent += craterDifficulty;
                smallRockChances += rockDifficulty;
                mediumRockChances += rockDifficulty;
                largeRockChances += rockDifficulty;
            }
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
            spawnHeight = 2f;
            return smallRock;
            
        }
        else if (smallRockChances < rnd && rnd < (smallRockChances + mediumRockChances))
        {
            spawnHeight = 2.5f;
            return mediumRock;
            

        }
        else if (rnd > smallRockChances + mediumRockChances && rnd <(smallRockChances +largeRockChances + mediumRockChances))
        {
            spawnHeight = 3f;
            return largeRock;
        }
        else if (rnd > (smallRockChances + largeRockChances + mediumRockChances))
        {
            return noRock;

        }
        return noRock;
    }

    public float GetOldestFloor()
    {
        return allFloor[4].transform.position.x;
    }
    public enum MapState
    {
        SmallCrater,
        BigCrater,
        NoCrater
    }
}
