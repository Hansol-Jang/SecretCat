using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingArrow : MonoBehaviour {

    SpriteRenderer SP;
    Color cl_a = new Color(255f,255f,255f,0f);
    Color cl_b = new Color(255f, 255f, 255f, 1f);
    float speed = 2.0f;

    // Use this for initialization
    void Start () {
        SP = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        SP.color = Color.Lerp(cl_a, cl_b, Mathf.PingPong(Time.time * speed, 1.5f)); //알파를 바꿔서 깜빡깜빡거리게
    }
}
