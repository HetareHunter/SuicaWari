using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISpace;

/// <summary>
/// ゲームのカウントを始めるクラス
/// </summary>
public class Timer : MonoBehaviour
{
    public static float time = 60;
    [SerializeField] float setTime = 90;
    public bool countStart = false;

    /// <summary>
    /// 現在timeSliderの方からインスペクターのsliderの値をいじったときに呼び出されるようになっている
    /// </summary>
    public void SetTimer()
    {
        time = setTime;
    }

    public void CountDown()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

        }
    }

    public void ChangeCountStart()
    {
        countStart = countStart ? false : true;
    }

    private void Update()
    {
        if (countStart)
        {
            CountDown();

            //Debug.Log("time:" + time);

            if (time <= 0)
            {
                ChangeCountStart();
                GameManager.Instance.gameState = GameManager.GameState.PlayEnd;
                time = 0;
            }
        }
    }

}
