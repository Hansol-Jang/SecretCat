using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageColliderBottom : MonoBehaviour {

    [HideInInspector] public bool bot; //가장 아래쪽에 카메라가 닿았는가?
    // Use this for initialization

    private void Start()
    {
        bot = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) //트리거에 들어갔을 때
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            bot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //트리거에서 나왔을 때
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            bot = false;
        }
    }
}
