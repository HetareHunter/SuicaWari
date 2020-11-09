using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool startSwing = false;
    private void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Button.One)) //ボタンを押している間探知モード
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Play)
            {
                startSwing = false;
            }
        }
        else
        {
            startSwing = true;
        }
        //Debug.Log("startSwing:"+startSwing);
    }
}
