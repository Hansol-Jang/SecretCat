using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {

    public Sprite main0; //아래
    public Sprite main1; //왼쪽
    public Sprite main2; //위
    public Sprite main3; //오른쪽
    public Sprite main4; //죽음
    public Sprite main5; //클리어
    public Sprite main6; //뼈0
    public Sprite main7; //뼈1
    public Sprite main8; //뼈2
    public Sprite main9; //뼈3

    public Animator cat_anim;

    protected void Start()
    {
        cat_anim = transform.GetComponent<Animator>();
    }
}
