using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText, livesText;
    [SerializeField] private float outOfScreenDistance;
    private float actualTime;

    [Header("Diificulty")]
    [SerializeField] private float waitTimeBeforeUFODifficulty;
    [SerializeField] private float waitTimeBeforeTankDifficulty;

    private float timeBeforeDifficultyUFOIncrease, timeBeforeDifficultyTankIncrease;

    [Header("UFO")]
    [SerializeField] private GameObject UFO;
    [SerializeField] private float maxUFO, timeBetweenUFO, minTimeBetweenUFO, timeToDecrease;

    [Header("Tank")]
    [SerializeField] private GameObject Tank;
    [SerializeField] private float timeBetweenTank, minTimeBetweenTank, tankTimeToDecrease;

    private GameObject[] allUFO;
    private GameObject newTank;
    private Transform thePlayer;
    private Vector3 posToAppear;
    private float sideOfScreen;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        thePlayer = PlayerMovement.instance.transform;
        timeBeforeDifficultyUFOIncrease = waitTimeBeforeUFODifficulty;

        Invoke("UFOInstantiate", timeBetweenUFO);
        Invoke("TankInstantiate", timeBetweenTank);
    }


    private void Update()
    {
        allUFO = GameObject.FindGameObjectsWithTag("UFO");

        actualTime = Time.time;

        if(actualTime > waitTimeBeforeUFODifficulty && timeBetweenUFO > minTimeBetweenUFO)
        {
            waitTimeBeforeUFODifficulty += timeBeforeDifficultyUFOIncrease;
            timeBetweenUFO -= timeToDecrease;
        }

        if(actualTime > waitTimeBeforeTankDifficulty && timeBetweenTank > minTimeBetweenTank)
        {
            waitTimeBeforeTankDifficulty += timeBeforeDifficultyTankIncrease;
            timeBetweenTank -= tankTimeToDecrease;
        }
    }

    private void ChooseSideOfScreen()
    {
        sideOfScreen = Random.Range(0,2);

        if(sideOfScreen == 0)
        {
            posToAppear = new Vector3(thePlayer.position.x + outOfScreenDistance, 0.9f, 0);
        }
        else
        {
            posToAppear = new Vector3(thePlayer.position.x - outOfScreenDistance, 0.9f, 0);
        }
    }

    //UFO
    private void UFOInstantiate()
    {
        ChooseSideOfScreen();

        if (allUFO.Length < maxUFO)
        {
            Instantiate(UFO, posToAppear, transform.rotation);
        }

        Invoke("UFOInstantiate", timeBetweenUFO);
    }

    private void TankInstantiate()
    {
        ChooseSideOfScreen();

        newTank = Instantiate(Tank, posToAppear, transform.rotation);
        Destroy(newTank, minTimeBetweenTank);

        Invoke("TankInstantiate", timeBetweenTank);
    }

    public void UpdateScoreText(float score)
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void UpdateLivesText(float lives)
    {
        livesText.text = lives.ToString();
    }
}
