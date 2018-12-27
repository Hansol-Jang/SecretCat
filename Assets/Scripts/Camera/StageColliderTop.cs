using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageColliderTop : MonoBehaviour {

    [HideInInspector] public bool top; //가장 위쪽에 카메라가 닿았는가?
    // Use this for initialization

    private void Start()
    {
        top = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) //트리거에 들어갔을 때
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            top = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //트리거에서 나왔을 때
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            top = false;
        }
    }
}
