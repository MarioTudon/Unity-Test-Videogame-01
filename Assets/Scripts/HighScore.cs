using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    [SerializeField] private Score score;
    private TMPro.TMP_Text highScoreText;

    void Start()
    {
        highScoreText = GetComponent<TMPro.TMP_Text>();
        highScoreText.text = "HI " + Mathf.RoundToInt(PlayerPrefs.GetFloat("High Score", score.score)).ToString("D5");
    }

    public void UpdateHighScore()
    {
        if(score.score >= PlayerPrefs.GetFloat("High Score", score.score))
        {
            PlayerPrefs.SetFloat("High Score", score.score);
        }
    }
}
