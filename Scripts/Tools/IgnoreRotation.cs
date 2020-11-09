using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreRotation : MonoBehaviour
{
    Vector3 def;
    Vector3 defParent;

    void Awake()
    {
        def = transform.localRotation.eulerAngles;
    }

    private void Start()
    {
        defParent = GameObject.Find("RightHandAnchor").transform.localPosition;
    }

    void Update()
    {
        //defParent = transform.parent.transform.parent.transform.parent.transform.localRotation.eulerAngles;

        ////修正箇所
        //transform.localRotation = Quaternion.Euler(def - defParent);

        ////ログ用
        //Vector3 result = transform.localRotation.eulerAngles;
        //Debug.Log("def=" + def + "     _parent=" + defParent + "     result=" + result);
        //Debug.Log("transform.parent.transform.parentのオブジェクト名：" + transform.parent.transform.parent.transform.parent.name);
    }

}
