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
    [SerializeField] private float waitTimeBeforeDifficulty;
    private float timeBeforeDifficultyIncrease;

    [Header("UFO")]
    [SerializeField] private GameObject UFO;
    [SerializeField] private float maxUFO, timeBetweenUFO, minTimeBetweenUFO, timeToDecrease;

    private GameObject[] allUFO;
    private Transform thePlayer;
    private Vector3 posToAppear;
    private bool canSendUFO;
    private float sideOfScreen;


    private void Awake()
    {
        instance = this;
        canSendUFO = true;
    }

    private void Start()
    {
        thePlayer = PlayerMovement.instance.transform;
        timeBeforeDifficultyIncrease = waitTimeBeforeDifficulty;

        Invoke("UFOInstantiate", timeBetweenUFO);
    }


    private void Update()
    {
        allUFO = GameObject.FindGameObjectsWithTag("UFO");

        actualTime = Time.time;

        if(actualTime > waitTimeBeforeDifficulty && timeBetweenUFO > minTimeBetweenUFO)
        {
            waitTimeBeforeDifficulty += timeBeforeDifficultyIncrease;
            timeBetweenUFO -= timeToDecrease;
        }
    }

    private void ChooseSideOfScreen()
    {
        sideOfScreen = Random.Range(0,2);

        if(sideOfScreen == 0)
        {
            posToAppear = new Vector3(thePlayer.position.x + outOfScreenDistance, 1.5f, 0);
        }
        else
        {
            posToAppear = new Vector3(thePlayer.position.x - outOfScreenDistance, 1.5f, 0);
        }
    }

    //UFO
    private void UFOInstantiate()
    {
        ChooseSideOfScreen();

        if (canSendUFO && allUFO.Length < maxUFO)
        {
            Instantiate(UFO, posToAppear, transform.rotation);
        }

        Invoke("UFOInstantiate", timeBetweenUFO);
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
