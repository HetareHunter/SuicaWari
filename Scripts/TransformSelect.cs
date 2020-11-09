using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformSelect : MonoBehaviour
{
    // 位置を制御するオブジェクト（Transform）を格納する変数
    //public List<Transform> obj = new List<Transform>();
    // X軸座標を制御する Slider オブジェクトを格納する変数
    //public Slider sliderPosX;
    // Y軸座標を制御する Slider オブジェクトを格納する変数
    //public List<Slider> sliderPosY = new List<Slider>();
    // Z軸座標を制御する Slider オブジェクトを格納する変数
    //public Slider sliderPosZ;

    public List<ObjPosYValue> objPosYValues = new List<ObjPosYValue>();

    // X軸座標を制御する処理
    //public void ValuePosX()
    //{
    //    if (sliderPosX != null)
    //    {
    //        // オブジェクトの座標を変数 objPos に格納
    //        Vector3 objPos = obj.localPosition;
    //        // スライダーの値を変数 objPos に格納
    //        objPos.x = sliderPosX.value;
    //        // 変数 objPos の値をオブジェクトの座標に格納
    //        obj.localPosition = objPos;
    //    }
    //}

    // Y軸座標を制御する処理
    public void ValuePosY()
    {
        for (int i = 0; i < objPosYValues.Count; i++)
        {
            Debug.Log(objPosYValues[i].sliderPosY.value);
            if (objPosYValues != null)
            {
                // オブジェクトの座標を変数 objPos に格納
                Vector3 objPos = objPosYValues[i].obj.localPosition;
                // スライダーの値を変数 objPos に格納
                objPos.y = objPosYValues[i].sliderPosY.value;
                // 変数 objPos の値をオブジェクトの座標に格納
                objPosYValues[i].obj.localPosition = objPos;
            }
        }


    }

    // Z軸座標を制御する処理
    //public void ValuePosZ()
    //{
    //    if (sliderPosZ != null)
    //    {
    //        // オブジェクトの座標を変数 objPos に格納
    //        Vector3 objPos = obj.localPosition;
    //        // スライダーの値を変数 objPos に格納
    //        objPos.z = sliderPosZ.value;
    //        // 変数 objPos の値をオブジェクトの座標に格納
    //        obj.localPosition = objPos;
    //    }
    //}

    // リセット処理(座標値)
    //public void ValueResetPos()
    //{
    //    // スライダーの値に 0 を格納
    //    sliderPosX.value = 0f;
    //    sliderPosY.value = 0f;
    //    sliderPosZ.value = 0f;
    //}
    //private void Update()
    //{
    //    //ValuePosY();
    //}

}

[System.Serializable]
public class ObjPosYValue
{
    public Transform obj;
    public Slider sliderPosY;
}