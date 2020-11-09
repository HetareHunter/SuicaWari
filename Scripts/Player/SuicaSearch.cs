using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hand
{
    public class SuicaSearch : MonoBehaviour
    {
        [SerializeField] float maxDistance = 10f; //距離を割る分母
        ViverationR viveR = new ViverationR();
        ViverationL viveL = new ViverationL();
        [SerializeField] GameObject butTop; //距離に応じた振動を測定するため
        [SerializeField] GameObject viveRightHand;
        [SerializeField] GameObject viveLeftHand;
        //[SerializeField] AnimationCurve viveDisDenominatorCurve;
        [SerializeField] float maxViveIntervalTime = 2.0f;
        [SerializeField] float vivePower = 0.5f;
        [SerializeField] float vivePowerPer = 0.7f;
        //[SerializeField] float viveTime = 0.2f;
        float viveIntervalTime;
        float distancePer;
        public static bool searchVive = false;
        float viveCycle = 0;

        SuicaHit suicaHit;
        [SerializeField] GameObject suicaBut;

        Coroutine searchCoroutine;

        private void Start()
        {
            viveR = viveRightHand.GetComponent<ViverationR>();
            viveL = viveLeftHand.GetComponent<ViverationL>();
            suicaHit = suicaBut.gameObject.GetComponent<SuicaHit>();
        }

        private void Update()
        {
            viveCycle += viveIntervalTime * Time.deltaTime;
        }

        private void OnTriggerStay(Collider collider)
        {
            var objNameTag = collider.gameObject.tag;
            if (objNameTag == "Suica" && !PlayerManager.startSwing)
            {
                searchVive = true;
                float dis = Vector3.Distance(butTop.gameObject.transform.position, collider.gameObject.transform.position);
                distancePer = Mathf.InverseLerp(0, maxDistance, dis);
                SetIntervalTime();
                SetVivePower();
                //StartCoroutine(viveR.TimeVivration(vivePower, vivePower, viveIntervalTime));
                StartViveHand();

                //Observable.TimerFrame((int)viveIntervalTime).Subscribe(_=>
                //)
                //OVRInput.SetControllerVibration(viveDisDenominatorCurve.Evaluate(distancePer) //UniRxやコルーチンで停止処理を書くと振動が途切れ途切れで、変な感じになる
                //    , viveDisDenominatorCurve.Evaluate(distancePer), OVRInput.Controller.RTouch);

                //Observable.Timer(TimeSpan.FromMilliseconds(viveTime)).Subscribe(_ =>
                //StartCoroutine(viveR.TimeVivration(0, 0, 0))).AddTo(this);
                //if (OVRInput.GetUp(OVRInput.Button.One))
                //{
                //    StartCoroutine(viveR.TimeVivration(0, 0, 0));
                //    Debug.Log("else if!");
                //}
            }

            if (OVRInput.GetUp(OVRInput.Button.One))//上のif文の振動のさせ方だと2秒間振動し続けてしまうので、探知モードをやめるなら振動を停止する処理を書かなければならない
            {
                //StartCoroutine(viveR.TimeVivration(0, 0, 0));
                if (searchCoroutine != null)
                {
                    StopCoroutine(searchCoroutine);
                }
                
                searchVive = false;
            }
        }
        private void OnTriggerExit(Collider collider) //上のgetupのif文と同じ理由で、スイカから探知コライダーがズレたら振動をやめる
        {
            var objNameTag = collider.gameObject.tag;
            if (objNameTag == "Suica" && !PlayerManager.startSwing)
            {
                StopCoroutine(searchCoroutine);
                Debug.Log("OnTriggerExit!");
            }
        }

        void SetIntervalTime()
        {
            viveIntervalTime = maxViveIntervalTime * (1 - distancePer);
        }

        void StartViveHand()
        {
            if (suicaHit.myGrabbable.onRightHand)
            {
                searchCoroutine = StartCoroutine(viveR.ContinueVivration(vivePower, vivePower));
            }
            if (suicaHit.myGrabbable.onLeftHand)
            {
                searchCoroutine = StartCoroutine(viveL.ContinueVivration(vivePower, vivePower));
            }
        }

        void SetVivePower()
        {
            vivePower = Mathf.Round(Mathf.Abs(Mathf.Sin(viveCycle))) * vivePowerPer;
        }
    }
}