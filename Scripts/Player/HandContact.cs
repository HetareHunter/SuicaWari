using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// このスクリプトはCustomHandRightかCustomHandLeftに付ける事を前提としてる
/// </summary>
public class HandContact : MonoBehaviour
{
    public bool contactHand = false;
    public bool swingContactHand = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("触れた！");
        if (other.CompareTag ("TopHandPosi"))
        {
            if (!contactHand)
            {
                contactHand = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("離れた！");
        if (other.CompareTag("TopHandPosi"))
        {
            if (contactHand)
            {
                contactHand = false;
            }
        }
    }
}