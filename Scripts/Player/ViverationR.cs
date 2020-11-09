using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViverationR : MonoBehaviour
{

    public IEnumerator TimeVivration(float time, float frequency, float amplitude)
    {
        //握られているコントローラーを検出
        var activeController = OVRInput.Controller.RTouch;
        //振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, activeController);
        Debug.Log("右手振動してるぞーー");
        //振動を止めるまで待機
        yield return new WaitForSeconds(time);
        //振動を止める
        OVRInput.SetControllerVibration(0, 0, activeController);
        yield break;
    }

    public IEnumerator ContinueVivration(float frequency, float amplitude)
    {
        //握られているコントローラーを検出
        var activeController = OVRInput.Controller.RTouch;
        //振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, activeController);
        Debug.Log("右手振動してるぞーー");
        //振動を止めるまで待機
        yield return new WaitForSeconds(50000); //コルーチンを一度呼び出し、ここで指定した秒数を過ぎると、以降変な振動になるっぽい？未解明
        //振動を止める
        OVRInput.SetControllerVibration(0, 0, activeController);
        yield break;
    }
}
