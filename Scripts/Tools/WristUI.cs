using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristUI : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = Timer.time.ToString("F2");

        scoreText.text = "スコア:" + ScoreCounter.score.ToString();
    }
}
