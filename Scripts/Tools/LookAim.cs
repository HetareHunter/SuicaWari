using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAim : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform,Vector3.up);
    }
}
