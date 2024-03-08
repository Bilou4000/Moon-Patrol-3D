using System.Collections;
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
    [SerializeField] private GameObject shield;
    [SerializeField] private float timeAfterBingHit, timeOfShield;
    private bool isInvincibile, hasShield;
    private IEnumerator damageCoroutine = null;

    GameObject[] allUFO, allMines;
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
        allMines = GameObject.FindGameObjectsWithTag("Mine");
        allUFO = GameObject.FindGameObjectsWithTag("UFO");
        if (lives <= 0)
        {
            GameManager.instance.GameOver();
        }

        if (transform.position.y < -3)
        {
            lives -= 1;
            GameManager.instance.UpdateLivesText(lives);
            StartCoroutine(onImpact());
            transform.position = new Vector3(MapScript.instance.GetOldestFloor(), originalY, transform.position.z);


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
            StartCoroutine(onImpact());
            StartCoroutine(damageCoroutine);
        }

        for (int i = 0; i < allUFO.Length; i++)
        {
            Destroy(allUFO[i]);
        }
        for (int i = 0; i < allMines.Length; i++)
        {
            Destroy(allMines[i]);
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
        if(damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }

        shield.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Invicible(timeOfShield));
        StartCoroutine(CreateShield());
    }

    IEnumerator Death()
    {
        if(!GameObject.Find("Bomb_Explosion(Clone)"))
        {
            Instantiate(explosionDeath, transform.position, transform.rotation);
        }
        
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
        GetComponent<PlayerShooting>().enabled = true;

        buggy.SetActive(true);
        wheel1.SetActive(true);
        wheel2.SetActive(true);
        wheel3.SetActive(true);
        wheel4.SetActive(true);
    }
    private IEnumerator Invicible(float time)
    {
        isInvincibile = true;
        Physics.IgnoreLayerCollision(4, 6, true);
        Physics.IgnoreLayerCollision(4, 7, true);

        yield return new WaitForSeconds(time);

        if (hasShield)
        {
            shield.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(1);
            shield.GetComponent<ParticleSystem>().Stop();
            hasShield = false;
        }

        isInvincibile = false;
        Physics.IgnoreLayerCollision(4, 6, false);
        Physics.IgnoreLayerCollision(4, 7, false);
    }

    private IEnumerator CreateShield()
    {
        hasShield = true;
        yield return new WaitForSeconds(3.1f);
        shield.GetComponent<ParticleSystem>().Pause();
    }

    private IEnumerator onImpact()
    {
        buggy.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        buggy.GetComponent<MeshRenderer>().material.color = Color.white;
    }


}
