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
    [SerializeField] private float waitTimeBeforeTankDifficulty, waitTimeBeforeShieldDifficulty, timeToDecrease;

    private float timeBeforeDifficultyUFOIncrease, timeBeforeDifficultyTankIncrease, timeBeforeDifficultyShieldIncrease;

    [Header("UFO")]
    [SerializeField] private GameObject UFO;
    [SerializeField] private float maxUFO, timeBetweenUFO, minTimeBetweenUFO;
    private GameObject[] allUFO;

    [Header("Tank")]
    [SerializeField] private GameObject Tank;
    [SerializeField] private float timeBetweenTank, minTimeBetweenTank;
    private GameObject newTank;

    [Header("Shield")]
    [SerializeField] private GameObject Shield;
    [SerializeField] private float timeBetweenShield, minTimeBetweenShield;
    private GameObject[] allShield;
    private GameObject newShield;

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
        timeBeforeDifficultyTankIncrease = waitTimeBeforeTankDifficulty;
        timeBeforeDifficultyShieldIncrease = waitTimeBeforeShieldDifficulty;

        Invoke("UFOInstantiate", timeBetweenUFO);
        Invoke("TankInstantiate", timeBetweenTank);
        Invoke("ShieldInstantiate", timeBetweenShield);
    }


    private void Update()
    {
        allUFO = GameObject.FindGameObjectsWithTag("UFO");
        allShield = GameObject.FindGameObjectsWithTag("Shield");

        actualTime = Time.time;

        if(actualTime > waitTimeBeforeUFODifficulty && timeBetweenUFO > minTimeBetweenUFO)
        {
            waitTimeBeforeUFODifficulty += timeBeforeDifficultyUFOIncrease;
            timeBetweenUFO -= timeToDecrease;
        }

        if(actualTime > waitTimeBeforeTankDifficulty && timeBetweenTank > minTimeBetweenTank)
        {
            waitTimeBeforeTankDifficulty += timeBeforeDifficultyTankIncrease;
            timeBetweenTank -= timeToDecrease;
        }

        if (actualTime > waitTimeBeforeShieldDifficulty && timeBetweenShield > minTimeBetweenShield)
        {
            waitTimeBeforeShieldDifficulty += timeBeforeDifficultyShieldIncrease;
            timeBetweenShield -= timeToDecrease;
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

    private void ShieldInstantiate()
    {
        posToAppear = new Vector3(thePlayer.position.x + outOfScreenDistance, 0.9f, 0);

        if(allShield.Length <= 0)
        {
            newShield = Instantiate(Shield, posToAppear, transform.rotation);
        }

        if(newShield != null)
        {
            Destroy(newShield, minTimeBetweenTank);
        }

        Invoke("ShieldInstantiate", timeBetweenShield);
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
