using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDisplay : MonoBehaviour {

    public GameObject Collider_Left; //가장 왼쪽
    public GameObject Collider_Right; //가장 오른쪽
    public GameObject Collider_Up; //가장 위쪽
    public GameObject Collider_Down; //가장 아래쪽
    public GameObject arrow_Left; //왼쪽 화살표
    public GameObject arrow_Right; //오른쪽 화살표
    public GameObject arrow_Up; //위쪽 화살표
    public GameObject arrow_Down; //아래쪽 화살표

	// Update is called once per frame
	void Update () {
        if (!Collider_Left.GetComponent<StageColliderLeft>().left) //가장 왼쪽이 아니라면
        {
            arrow_Left.SetActive(true);
        }
        else //가장 왼쪽이라면
        {
            arrow_Left.SetActive(false);
        }
        if (!Collider_Right.GetComponent<StageColliderRight>().right) //가장 오른쪽이 아니라면
        {
            arrow_Right.SetActive(true);
        }
        else //가장 오른쪽이라면
        {
            arrow_Right.SetActive(false);
        }
        if (!Collider_Up.GetComponent<StageColliderTop>().top) //가장 위쪽이 아니라면
        {
            arrow_Up.SetActive(true);
        }
        else //가장 위쪽이라면
        {
            arrow_Up.SetActive(false);
        }
        if (!Collider_Down.GetComponent<StageColliderBottom>().bot) //가장 아래쪽이라면
        {
            arrow_Down.SetActive(true);
        }
        else //가장 아래쪽이 아니라면
        {
            arrow_Down.SetActive(false);
        }
    }
}
