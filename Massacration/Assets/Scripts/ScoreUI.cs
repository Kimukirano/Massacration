using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    public static TMP_Text scoreText;
    public static int ScorePoints = 0;
    // Start is called before the first frame update

    public static void UpdateScore(int points)
    {
        ScorePoints += points;
        scoreText.text = ScorePoints.ToString();
    }
    void Start()
    {
        scoreText = ScoreText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
