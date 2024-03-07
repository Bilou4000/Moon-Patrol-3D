using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Lives")]
    [SerializeField] private float lives;
    [SerializeField] private float maxLives;


    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private float scoreThreshold;

    [Header("Invicible")]
    [SerializeField] private float timeAfterBingHit;
    [SerializeField] private float timeOfShield;
    private bool isInvincibile;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lives = 4;
        maxLives = 4;
        score = 0;
        scoreThreshold = 1000;

        GameManager.instance.UpdateLivesText(lives);
        GameManager.instance.UpdateScoreText(score);
    }

    private void Update()
    {
        if (lives <= 0)
        {
            //gameOver Screen
            Time.timeScale = 0f;
            //Destroy(gameObject);
        }

        if (lives < maxLives && score >= scoreThreshold)
        {
            lives += 1;
            scoreThreshold += 1000;
        }
    }
    public float GetScore()
    {
        return score;
    }

    public float GetLives()
    {
        return lives;
    }

    public void LoseLife(float damage)
    {
        if(!isInvincibile)
        {
            lives -= damage;
            StartCoroutine(Invicible(timeAfterBingHit));
        }

        GameManager.instance.UpdateLivesText(lives);
    }

    public void SetScore(float newScore)
    {
        score += newScore;
        GameManager.instance.UpdateScoreText(score);
    }

    private IEnumerator Invicible(float time)
    {
        isInvincibile = true;

        yield return new WaitForSeconds(time);

        isInvincibile = false;
    }


}
