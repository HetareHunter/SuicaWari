using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] GameObject spawner;
    SuicaSpawn1 suicaSpawn1 = new SuicaSpawn1();

    [SerializeField]Timer timer;
    [SerializeField] List<GameObject> dontDestroy;

    [SerializeField] PlayTrigger playTrigger = new PlayTrigger();
    [SerializeField] GameObject playTriggerObj;
    [SerializeField] GameObject camera;
    OVRScreenFade screenFade;
    [SerializeField]GameObject suicaBut;

    private void Start()
    {
        screenFade = camera.GetComponent<OVRScreenFade>();
    }
    public enum GameState
    {
        Title,
        StartReady,
        Play,
        PlayEnd,
        Result
    }
    public GameState gameState;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        foreach (var item in dontDestroy)
        {
            DontDestroyOnLoad(item);
        }
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Title:
                break;

            case GameState.StartReady:
                suicaSpawn1 = spawner.GetComponent<SuicaSpawn1>();
                suicaSpawn1.StartSpawn(); //スポナー起動

                Observable.Timer(TimeSpan.FromSeconds(playTrigger.fadeTime)).Subscribe(_ =>
                { screenFade.StartCoroutine(screenFade.FadeIn(1, 0, playTrigger.fadeTime));
                    timer.ChangeCountStart();
                }).AddTo(this);  //タイマー起動
                RecordManager.Instance.ResetRecord();
                ScoreCounter.score = 0;
                destroy1.destroyCount = 0;
                GetComponent<Timer>().SetTimer();

                GetComponent<SkyboxFade>().FadeDown();

                gameState = GameState.Play;
                break;

            case GameState.Play:
                
                suicaBut.GetComponent<SuicaHit>().enabled = true;
                break;
            case GameState.PlayEnd:
                suicaSpawn1.StopSpawn();
                RecordManager.Instance.JudgeRank();
                playTriggerObj.GetComponent<BoxCollider>().enabled = true;
                playTriggerObj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                suicaBut.GetComponent<SuicaHit>().enabled = false;
                

                GameObject[] enemySuica = GameObject.FindGameObjectsWithTag("Suica");
                foreach (var suica in enemySuica)
                {
                    Destroy(suica);
                }

                GetComponent<SkyboxFade>().FadeUp();
                gameState = GameState.Result;
                break;

            case GameState.Result:
                
                
                break;

            default:
                break;
        }
    }

    public void GameStart()
    {
        if (gameState == GameState.PlayEnd)
        {
            gameState = GameState.StartReady;
        }
    }
}
