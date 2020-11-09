using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjStalker : MonoBehaviour
{
    [SerializeField] GameObject chased;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = chased.transform.position;
    }
}
