using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChecker : MonoBehaviour
{
    public float speed;
    Vector3 butLatestPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Abs(((transform.position - butLatestPos) / Time.deltaTime).magnitude);
        butLatestPos = transform.position;
    }
}
