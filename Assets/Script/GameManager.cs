using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    private void Awake()
    {
        instance = this;
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
