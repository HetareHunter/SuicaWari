using Hand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingViveration : MonoBehaviour
{
    float speed;
    SpeedChecker speedChecker;
    float speedPer;
    [SerializeField] float startViveSpeed = 5;
    [SerializeField] float viveMaxSpeed = 30;
    [SerializeField] GameObject viveRightHand;
    [SerializeField] GameObject viveLeftHand;
    [SerializeField] GameObject suicaBut;
    SuicaHit suicaHit;

    // Start is called before the first frame update
    void Start()
    {
        speedChecker = GetComponent<SpeedChecker>();
        suicaHit = suicaBut.gameObject.GetComponent<SuicaHit>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = speedChecker.speed;
        if (!SuicaHit.hitVive && PlayerManager.startSwing)
        {
            if (speed > startViveSpeed)
            {
                speedPer = Mathf.InverseLerp(startViveSpeed, viveMaxSpeed, speed);
                //UniRxやコルーチンで停止処理を書くと振動が途切れ途切れで、変な感じになる
                ViveHand();
            }
            else if (speed <= startViveSpeed)
            {
                StopVive();
            }
        }
    }

    void ViveHand()
    {
        if (suicaHit.myGrabbable.onRightHand)
        {
            OVRInput.SetControllerVibration(speedPer, speedPer, OVRInput.Controller.RTouch);
        }
        else if (suicaHit.myGrabbable.onLeftHand)
        {
            OVRInput.SetControllerVibration(speedPer, speedPer, OVRInput.Controller.LTouch);
        }
    }

    void StopVive()
    {
        if (suicaHit.myGrabbable.onRightHand)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
        else if (suicaHit.myGrabbable.onLeftHand)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
}
