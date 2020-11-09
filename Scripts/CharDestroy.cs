using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDestroy : MonoBehaviour
{
    [SerializeField] float deathTime = 1.5f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, deathTime);
    }
}
