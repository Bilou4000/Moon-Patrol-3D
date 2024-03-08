using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Lives")]
    [SerializeField] private float lives;
    private float maxLives;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private float scoreThreshold;
    private float startscoreThreshold;

    [Header("Invicible")]
    [SerializeField] private float timeAfterBingHit;
    [SerializeField] private float timeOfShield;
    private bool isInvincibile;
    private IEnumerator damageCoroutine = null, shieldCoroutine = null;

    private float originalY;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalY = transform.position.y;
        maxLives = lives;
        startscoreThreshold = scoreThreshold;

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

        if (transform.position.y < 0)
        {
            LoseLife(1);

            isInvincibile = false;
            StopCoroutine(shieldCoroutine);
            StopCoroutine(damageCoroutine);
        }

        if (lives < maxLives && score >= scoreThreshold)
        {
            lives += 1;
            scoreThreshold += startscoreThreshold;
            GameManager.instance.UpdateLivesText(lives);
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
            damageCoroutine = Invicible(timeAfterBingHit);
            StartCoroutine(damageCoroutine);
        }

        GameManager.instance.UpdateLivesText(lives);
        transform.position = new Vector3(MapScript.instance.GetOldestFloor(),originalY, transform.position.z);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void SetScore(float newScore)
    {
        score += newScore;
        GameManager.instance.UpdateScoreText(score);
    }

    public void ShieldPickUp()
    {
        StopCoroutine(damageCoroutine);

        shieldCoroutine = Invicible(timeOfShield);
        StartCoroutine(shieldCoroutine);
    }

    private IEnumerator Invicible(float time)
    {
        isInvincibile = true;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        isInvincibile = false;
    }


}
