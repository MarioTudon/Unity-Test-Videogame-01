using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Player player;
    private TMPro.TMP_Text scoreText;
    [HideInInspector] public float score;

    void Start()
    {
        scoreText = GetComponent<TMPro.TMP_Text>();
        scoreText.text = "00000";
    }

    // Update is called once per frame
    void Update()
    {
        if (player.youLose || !player.runing) return;

        score += Time.deltaTime * 4f;
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }
}
