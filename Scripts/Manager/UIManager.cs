using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace UISpace
{

    [System.Serializable]
    public class UIInspector
    {
        [Header("レーザーの長さ")]
        public Slider rayLength;
        public Text rayText;
        [Header("周波数")]
        public Slider viveFrequency;
        public Text frequencyText;
        [Header("振幅")]
        public Slider viveAmplitude;
        public Text amplitudeText;
        [Header("振動時間(一回目振動してからの時間)")]
        public Slider viveTime;
        public Text timeText;
        [Header("StartSwing変数のオンオフ")]
        public bool startSwing = PlayerManager.startSwing;
        public Text startSwingText;
        [Header("振り上げた時の判定の高さ")]
        public Slider startSwingHeight;
        public Text startSwingHeightText;
        [Header("スイカを叩いた時の判定の高さ")]
        public Slider SwingDownHeight;
        public Text SwingDownHeightText;
        [Header("スイカを叩いた回数")]
        public int swingHitCount = ScoreCounter.score;
        public Text swingHitCountText;
        [Header("叩ける時間")]
        public Text HitTimeCountText;
        [Header("叩ける時間のタイマー")]
        public Slider timeSlider;
        public Text timeCountText;
    }
    public class UIManager : MonoBehaviour
    {
        public UIInspector config;

        void Update()
        {
            config.rayText.text = config.rayLength.value.ToString() + "m(メートル)";
            config.frequencyText.text = config.viveFrequency.value.ToString();
            config.amplitudeText.text = config.viveAmplitude.value.ToString();
            config.timeText.text = config.viveTime.value.ToString() + "秒";
            config.startSwing = PlayerManager.startSwing;
            config.startSwingText.text = "StartSwing:" + config.startSwing;
            config.startSwingHeightText.text = config.startSwingHeight.value.ToString() + "m(メートル)";
            config.SwingDownHeightText.text = config.SwingDownHeight.value.ToString() + "m(メートル)";
            config.swingHitCount = ScoreCounter.score;
            config.swingHitCountText.text = config.swingHitCount.ToString() + " 点";
            config.HitTimeCountText.text = "残り " + Timer.time.ToString("F2") + " 秒";
            config.timeCountText.text = "カウント：" + config.timeSlider.value.ToString("F2");
        }
    }
}