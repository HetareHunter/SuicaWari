using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : SingletonMonoBehaviour<RecordManager>
{
    public int destroySuica = 0;
    public int excellentCount = 0;
    public int goodCount = 0;
    public int normalCount = 0;

    [SerializeField] int excellentBoundary = 90;
    [SerializeField] int goodBoundary = 70;

    [SerializeField] float rankSPer = 0.9f;
    [SerializeField] float rankAPer = 0.6f;
    [SerializeField] float rankBPer = 0.3f;

    public bool excellentOn = false;
    public bool goodOn = false;
    public bool normalOn = false;

    public string rank = "";

    /// <summary>
    /// スイカを叩いた時の当たった時の良し悪しの判定
    /// </summary>
    /// <param name="addScore"></param>
    public void Judgement(int addScore)
    {
        if (addScore >= excellentBoundary)
        {
            excellentCount++;
            excellentOn = true;
            Debug.Log("excellentCount:"+excellentCount);
            return;
        }
        if (addScore >= goodBoundary)
        {
            goodCount++;
            goodOn = true;
            Debug.Log("goodCount:" + goodCount);
            return;
        }
        normalCount++;
        normalOn = true;
        Debug.Log("normalCount:" + normalCount);
    }

    /// <summary>
    /// ゲーム終了時にランクを決定する
    /// </summary>
    public void JudgeRank()
    {
        int suicaNum = SuicaSpawn1.suicaSpawnNum; //生成されたスイカの数
        int score = ScoreCounter.score; //現在の得点
        int TheoryPoint = suicaNum * 100; //理論上最高点
        float scorePerTheory = Mathf.InverseLerp(0, TheoryPoint, score);

        if (scorePerTheory >= rankSPer)
        {
            rank = "S";
        }
        else if(scorePerTheory < rankSPer && scorePerTheory >= rankAPer)
        {
            rank = "A";
        }
        else if (scorePerTheory < rankAPer && scorePerTheory >= rankBPer)
        {
            rank = "B";
        }
        else
        {
            rank = "C";
        }
    }

    public void ResetRecord()
    {
        destroySuica = 0;
        excellentCount = 0;
        goodCount = 0;
        normalCount = 0;
    }
}
