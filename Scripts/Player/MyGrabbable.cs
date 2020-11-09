using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabbable : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    public bool onLeftHand = false;
    [SerializeField] GameObject rightHand;
    public bool onRightHand = false;
    [SerializeField] float thresholdGrab = 0.55f;
    [SerializeField] float thresholdRelease = 0.35f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onRightHand)
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) < thresholdRelease)
            {
                transform.parent = null;
                onRightHand = false;
            }
        }
        else if (onLeftHand)
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) < thresholdRelease)
            {
                transform.parent = null;
                onLeftHand = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "RightHandCollider" && OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) >= thresholdGrab)
        {
            transform.parent = rightHand.transform;
            onRightHand = true;
        }
        if (other.name == "LeftHandCollider" && OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) >= thresholdGrab)
        {
            transform.parent = leftHand.transform;
            onLeftHand = true;
        }
    }
}
