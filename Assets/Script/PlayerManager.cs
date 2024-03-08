using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [Header("Player")]
    [SerializeField] GameObject buggy;
    [SerializeField] GameObject wheel1, wheel2, wheel3, wheel4, explosionDeath;

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
    private IEnumerator damageCoroutine = null;

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
        }

        if (lives < maxLives && score >= scoreThreshold)
        {
            lives += 1;
            scoreThreshold += startscoreThreshold;
            GameManager.instance.UpdateLivesText(lives);
        }

        if(isInvincibile)
        {
            GetComponent<Rigidbody>().excludeLayers = 6;
        }
        else
        {
            GetComponent<Rigidbody>().includeLayers = 6;
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
        StartCoroutine(Death());

    }

    public void SetScore(float newScore)
    {
        score += newScore;
        GameManager.instance.UpdateScoreText(score);
    }

    public void ShieldPickUp()
    {
        StopCoroutine(damageCoroutine);
        StartCoroutine(Invicible(timeOfShield));
    }

    IEnumerator Death()
    {
        Instantiate(explosionDeath, transform.position, transform.rotation);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;
        buggy.SetActive(false);
        wheel1.SetActive(false);
        wheel2.SetActive(false);
        wheel3.SetActive(false);
        wheel4.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(GameObject.Find("Bomb_Explosion(Clone)"));
        transform.position = new Vector3(MapScript.instance.GetOldestFloor(), originalY, transform.position.z);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerShooting>().enabled=true;
        buggy.SetActive(true);
        wheel1.SetActive(true);
        wheel2.SetActive(true);
        wheel3.SetActive(true);
        wheel4.SetActive(true);
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
