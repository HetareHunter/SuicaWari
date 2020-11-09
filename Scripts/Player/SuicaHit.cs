using Hand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class SuicaHit : MonoBehaviour
{
    //[SerializeField] AudioClip beatSound;
    AudioSource audioSource;

    ViverationR viveR = new ViverationR();
    ViverationL viveL = new ViverationL();
    [SerializeField] GameObject parentRHand;
    [SerializeField] GameObject parentLHand;
    [SerializeField] float beatViveTime = 0.65f;

    public static bool hitVive = false;
    GameObject gameManager;
    [Header("スイカが消滅するまでのフレーム数")]
    [SerializeField] int untilSuicaKillTime = 1;
    [SerializeField] GameObject measurementPosition;
    public float distanceToSuica;
    [SerializeField] GameObject hitEffectNormal;
    [SerializeField] GameObject hitEffectGood;
    [SerializeField] GameObject hitEffectExcellent;
    [SerializeField] GameObject player;

    public MyGrabbable myGrabbable;

    [SerializeField] AudioClip beatSound1;
    [SerializeField] AudioClip beatSound2;
    [SerializeField] AudioClip beatSound3;
    [SerializeField] AudioClip beatSound4;
    int RandomSound;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myGrabbable = GetComponent<MyGrabbable>();
        viveR = parentRHand.GetComponent<ViverationR>();
        viveL = parentLHand.GetComponent<ViverationL>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController");
        }
        Debug.Log("タグは" + collision.gameObject.tag);

        if (collision.gameObject.tag == "Suica" && PlayerManager.startSwing)
        {
            hitVive = true;
            RecordManager.Instance.destroySuica++;
            JudgeViveHand(); //手の振動

            AudioPlay(RondomSoundPlay());

            MeasurementToSuica(collision);
            Vector3 collisionPoint = collision.gameObject.transform.position;

            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            Observable.TimerFrame(untilSuicaKillTime + 1).Subscribe(_ =>
            {
                gameManager.GetComponent<ScoreCounter>().AddScore();
                HitEffect(collisionPoint);
                Destroy(collision.gameObject);
            }).AddTo(this); //スイカがDestroyするまでの時間。この間に棒がスイカを斬った時どれだけ中心に近いかを計測する
            //collision.gameObject.GetComponent<MeshRenderer>().enabled = false;

            Observable.Timer(TimeSpan.FromSeconds(beatViveTime)).Subscribe(_ =>
            hitVive = false).AddTo(this);
        }
    }

    public void MeasurementToSuica(Collision collision)
    {

        Vector3 hitPos = Vector3.zero;
        foreach (ContactPoint point in collision.contacts)
        {
            hitPos = point.point;
        }
        GameObject hitAfterPosiPoint;
        GameObject hitPosiPoint;
        GameObject deathSuicaPoint;
        hitAfterPosiPoint = Instantiate(measurementPosition, hitPos, Quaternion.identity);
        hitPosiPoint = Instantiate(measurementPosition, hitPos, Quaternion.identity);
        deathSuicaPoint = Instantiate(measurementPosition, collision.transform.GetChild(0).position, Quaternion.identity);

        hitAfterPosiPoint.transform.parent = transform; //Obj_aの方は棒に追従する
        //measureTempObj_b.transform.parent = collision.transform; //Obj_bはスイカと一緒に動き、これを測定座標系の原点とする
        deathSuicaPoint.transform.parent = hitPosiPoint.transform;

        Observable.TimerFrame(untilSuicaKillTime).Subscribe(_ =>  //スイカがdestroyする一瞬前の処理
        {
            hitAfterPosiPoint.transform.parent = hitPosiPoint.transform;

            Vector3 measureVector = (hitPosiPoint.transform.localPosition - hitAfterPosiPoint.transform.localPosition); //Obj_bからObj_aへのベクトルを正規化し、それを変数倍する

            Vector3 measureProject = deathSuicaPoint.transform.localPosition + Vector3.Project(
                deathSuicaPoint.transform.localPosition, measureVector); //b→deathからmeasureVectorに射影する、できた座標が垂線となる点h

            Debug.Log("masureProject:" + measureProject);
            distanceToSuica = Vector3.Distance(deathSuicaPoint.transform.localPosition, measureProject); //この距離がスイカと棒の軌道の最短距離のはず
            float centerToCollisionPoint = Vector3.Distance(deathSuicaPoint.transform.localPosition, hitPosiPoint.transform.position); //スイカの中心と点bの距離
            if (distanceToSuica >= centerToCollisionPoint) //もし端の方を叩くなどして1フレーム後が逆に棒がスイカから遠ざかっていたとき
            {
                distanceToSuica = centerToCollisionPoint;
                Debug.Log("if入りましたー");
            }

            Destroy(hitAfterPosiPoint);
            Destroy(hitPosiPoint);
            Destroy(deathSuicaPoint);
            Debug.Log("distanceToSuica:" + distanceToSuica);
        }).AddTo(this);
    }

    public void HitEffect(Vector3 instancePosi)
    {
        if (RecordManager.Instance.excellentOn)
        {
            GameObject effectObj= Instantiate(hitEffectExcellent, instancePosi, Quaternion.LookRotation(player.transform.position));
            RecordManager.Instance.excellentOn = false;
        }
        else if (RecordManager.Instance.goodOn)
        {
            GameObject effectObj = Instantiate(hitEffectGood, instancePosi, Quaternion.LookRotation(player.transform.position));
            RecordManager.Instance.goodOn = false;
        }
        else if(RecordManager.Instance.normalOn)
        {
            GameObject effectObj = Instantiate(hitEffectNormal, instancePosi, Quaternion.LookRotation(player.transform.position));
            RecordManager.Instance.normalOn = false;
        }
        
    }

    void AudioPlay(AudioClip audio)
    {
        if (!audioSource.enabled)
        {
            audioSource.enabled = true;
        }
        audioSource.PlayOneShot(audio);
    }

    void JudgeViveHand()
    {
        if (myGrabbable.onRightHand)
        {
            StartCoroutine(viveR.TimeVivration(beatViveTime, 1, 1));
        }
        if (myGrabbable.onLeftHand)
        {
            StartCoroutine(viveL.TimeVivration(beatViveTime, 1, 1));
        }
    }

    AudioClip RondomSoundPlay()
    {
        RandomSound = UnityEngine.Random.Range(1, 5);
        if (RandomSound == 1)
        {
            return beatSound1;
        }
        else if (RandomSound == 2)
        {
            return beatSound2;
        }
        else if (RandomSound == 3)
        {
            return beatSound3;
        }
        else
        {
            return beatSound4;
        }
    }
}
