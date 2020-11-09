using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicaGenerater : MonoBehaviour
{
    public GameObject suicaInstance ;
    public bool suicaExist = false;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(suicaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.gameStart&&!suicaExist)
        //{
        //    Instantiate(suicaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //    suicaExist = true;
        //}
    }

    //public void GenerateSuica()
    //{

    //    Instantiate(suicaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    //}
}
