using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SangurasuController : MonoBehaviour
{

    Vector3 startPosi = new Vector3(0,0,0);

    [SerializeField] GameObject sangurasu;
    // Start is called before the first frame update
    void Start()
    {
        startPosi = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.StartReady)
        {
            MeshHide();
        }
        else if (GameManager.Instance.gameState == GameManager.GameState.PlayEnd)
        {
            MeshPop();
            ResetPosi();
        }
    }

    void ResetPosi()
    {
        gameObject.transform.position = startPosi;
    }

    void MeshHide()
    {
        sangurasu.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void MeshPop()
    {
        sangurasu.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
