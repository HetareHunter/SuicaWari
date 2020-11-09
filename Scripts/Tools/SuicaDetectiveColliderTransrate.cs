using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicaDetectiveColliderTransrate : MonoBehaviour
{
    [SerializeField] GameObject SuicaButTip;
    [SerializeField] float collidersHigher = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = butRayPointer.hitPoint;
        Vector3 pos = transform.position;
        pos.x = SuicaButTip.transform.position.x;
        pos.y = collidersHigher;
        pos.z = SuicaButTip.transform.position.z;
        transform.position = pos;

        Vector3 direction = SuicaButTip.transform.forward;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        //transform.rotation=Quaternion.Euler(worldAngle.eulerAngles);
    }
}
