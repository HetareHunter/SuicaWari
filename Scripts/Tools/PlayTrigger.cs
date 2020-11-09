using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrigger : MonoBehaviour
{
    MyGrabbable grabbable;
    OVRScreenFade screenFade;
    [SerializeField] GameObject camera;
    public float fadeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<MyGrabbable>();
        screenFade = camera.GetComponent<OVRScreenFade>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(grabbable.onLeftHand&& collider.gameObject.tag == "PlayerCamera")
        {
            screenFade.StartCoroutine(screenFade.FadeIn(0, 1, fadeTime));
            GetComponent<BoxCollider>().enabled = false; //触れなくする
            //transform.position = startPosi;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameManager.Instance.gameState = GameManager.GameState.StartReady;
            Debug.Log("cameraに触れた");
        }
        else if(grabbable.onRightHand && collider.gameObject.tag == "PlayerCamera")
        {
            screenFade.StartCoroutine(screenFade.FadeIn(0, 1, fadeTime));
            GetComponent<BoxCollider>().enabled = false; //触れなくする
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            //transform.position = startPosi;
            GameManager.Instance.gameState = GameManager.GameState.StartReady;
            Debug.Log("cameraに触れた");
        }
    }
}
