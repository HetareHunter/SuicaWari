using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int score = 0;
    public int basePoint = 50;
    public int maxSpeedPoint = 25;
    [SerializeField] float maxCheckSpeed = 0.01f;
    float butSpeed;
    int speedPoint;
    int centerPoint;
    float suicaDistance;
    [SerializeField] int maxCenterPoint = 25;
    [SerializeField] float minCenterDistance = 0.2f;
    [SerializeField] GameObject SuicaButTop;
    [SerializeField] GameObject SuicaBut;
    [SerializeField] GameObject suica;

    public void AddScore()
    {
        int addPoint = basePoint;
        SetSpeedScore();
        SetCenterScore();
        addPoint += speedPoint + centerPoint;
        RecordManager.Instance.Judgement(addPoint);
        score += addPoint;
        Debug.Log("score:" + score);

    }

    public void SetSpeedScore()
    {
        if (SuicaButTop == null)
        {
            SuicaButTop=GameObject.FindGameObjectWithTag("SuicaButTop");
        }

        butSpeed = SuicaButTop.GetComponent<SpeedChecker>().speed;
        float speedPointPer = Mathf.InverseLerp(0, maxCheckSpeed, butSpeed);
        Debug.Log("butSpeed:" + butSpeed);
        Debug.Log("SpeedPointPer:" + speedPointPer);
        speedPoint = (int)Mathf.Round(maxSpeedPoint * speedPointPer); //端数は五捨五入
        Debug.Log("speedPoint:" + speedPoint);
    }

    public void SetCenterScore()
    {
        if (SuicaBut == null)
        {
            SuicaBut = GameObject.FindGameObjectWithTag("SuicaBut");
        }

        suicaDistance = SuicaBut.GetComponent<SuicaHit>().distanceToSuica;
        float centerPointPer = 1 - Mathf.InverseLerp(minCenterDistance, suica.transform.lossyScale.x / 2, suicaDistance);
        Debug.Log("centerPointPer:" + centerPointPer);
        centerPoint = (int)Mathf.Round(maxCenterPoint * centerPointPer); //端数は五捨五入
        Debug.Log("centerPoint:" + centerPoint);
    }

}
