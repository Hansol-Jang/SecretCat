using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class startTouch : MonoBehaviour {

    private Touch tempTouchs;
    private bool touchOn;

    // Use this for initialization
    void Start () {

        touchOn = false;
        //touchOn = true;
		
	}

    // Update is called once per frame
    void Update() {
        if (Time.time > 1) {
            if (!GameManager.instance.quit_menu)
            {
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        tempTouchs = Input.GetTouch(i);
                        if (tempTouchs.phase == TouchPhase.Began)
                        {
                            touchOn = true;
                            break;
                        }
                    }
                }
                if (touchOn == true)
                { //터치하면 시작 화면에서 월드 씬으로 넘어감
                    touchOn = false;
                    LodingSceneManager.LoadScene("worldSelect");
                }
            }
        }
    }
}
