using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Screen")]
    [SerializeField] private float outOfScreenDistance;
    [SerializeField] private TextMeshProUGUI scoreText, livesText;
    private float actualTime;

    [Header("Difficulty")]
    [SerializeField] private float waitTimeBeforeUFODifficulty;
    [SerializeField] private float waitTimeBeforeTankDifficulty, waitTimeBeforeShieldDifficulty, timeToDecrease;

    private float timeBeforeDifficultyUFOIncrease, timeBeforeDifficultyTankIncrease, timeBeforeDifficultyShieldIncrease;

    [Header("UFO")]
    [SerializeField] private GameObject UFO;
    [SerializeField] private GameObject UFORightArrow, UFOLeftArrow;
    [SerializeField] private float maxUFO, timeBetweenUFO, minTimeBetweenUFO;
    private GameObject[] allUFO;

    [Header("Tank")]
    [SerializeField] private GameObject Tank;
    [SerializeField] private GameObject tankRightArrow, tankLeftArrow;
    [SerializeField] private float timeBetweenTank, minTimeBetweenTank;
    private GameObject newTank;

    [Header("Shield")]
    [SerializeField] private GameObject Shield;
    [SerializeField] private float timeBetweenShield, minTimeBetweenShield;
    private GameObject[] allShield;
    private GameObject newShield;

    [Header("GameOver")]
    [SerializeField] GameObject gameOver;
    [SerializeField]
    private TextMeshProUGUI gOscoreText;
    float gOScore;

    private Transform thePlayer;
    private Vector3 posToAppear;
    private float sideOfScreen;
    private bool spawningFromRight;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        thePlayer = PlayerMovement.instance.transform;

        gameOver.SetActive(false);

        timeBeforeDifficultyUFOIncrease = waitTimeBeforeUFODifficulty;
        timeBeforeDifficultyTankIncrease = waitTimeBeforeTankDifficulty;
        timeBeforeDifficultyShieldIncrease = waitTimeBeforeShieldDifficulty;

        Invoke("UFOInstantiate", timeBetweenUFO);
        Invoke("TankInstantiate", timeBetweenTank);
        Invoke("ShieldInstantiate", timeBetweenShield);

        Time.timeScale = 1f;
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
            posToAppear = new Vector3(thePlayer.position.x + outOfScreenDistance * 2, 0.9f, 0);
            spawningFromRight = true;
        }
        else
        {
            posToAppear = new Vector3(thePlayer.position.x - outOfScreenDistance, 0.9f, 0);
            spawningFromRight = false;
        }
    }

    //UFO
    private void UFOInstantiate()
    {
        ChooseSideOfScreen();

        if (allUFO.Length < maxUFO)
        {
            if (spawningFromRight)
            {
                UFORightArrow.SetActive(true);
            }
            else if (!spawningFromRight)
            {
                UFOLeftArrow.SetActive(true);
            }

            Instantiate(UFO, posToAppear, transform.rotation);

            StartCoroutine(HideObjectOnScreen(UFORightArrow));
            StartCoroutine(HideObjectOnScreen(UFOLeftArrow));
        }

        Invoke("UFOInstantiate", timeBetweenUFO);
    }

    private void TankInstantiate()
    {
        ChooseSideOfScreen();

        if (spawningFromRight)
        {
            tankRightArrow.SetActive(true);
        }
        else if (!spawningFromRight)
        {
            tankLeftArrow.SetActive(true);
        }

        newTank = Instantiate(Tank, posToAppear, transform.rotation);

        StartCoroutine(HideObjectOnScreen(tankRightArrow));
        StartCoroutine(HideObjectOnScreen(tankLeftArrow));

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

    private IEnumerator HideObjectOnScreen(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    public void UpdateScoreText(float score)
    {
        gOScore = score;
        scoreText.text = "Score : " + score.ToString();
    }

    public void UpdateLivesText(float lives)
    {
        livesText.text = lives.ToString();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gOscoreText.text = "Score : " + gOScore.ToString();
        Invoke("StopGame", 3.15f);
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
