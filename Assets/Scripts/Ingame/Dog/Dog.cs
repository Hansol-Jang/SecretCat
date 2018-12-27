using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public float atkTime = 0f;
    [HideInInspector] public bool boned = false; //뼈다귀에 매혹되었는지?
    [HideInInspector] public int[] is_loc_dog;// 강아지 현재 위치

    private GameObject board; // 보드판 게임오브젝트
    private BoardTileLoc loctile; // 보드타일록 스크립트
    private GameObject ply; // 플레이어 게임오브젝트
    private PlayerController pc; // 플레이어컨트롤러 스크립트
    private GameObject dsp; // Dog_Sprite
    private Animator dsp_anim; //Dog_Sprite의 Animator

    // Use this for initialization
    protected void Start () {
        is_loc_dog = new int[3];
        board = GameObject.Find("Board");
        loctile = board.GetComponent<BoardTileLoc>();
        ply = GameObject.Find("Player");
        pc = ply.GetComponent<PlayerController>();
        dsp = transform.GetChild(0).gameObject;
        dsp_anim = dsp.GetComponent<Animator>();
        GameManager.instance.AddDogToList(this); //리스트에 추가하라
        is_loc_dog[0] = loctile.dog_num[GameManager.instance.dogs.Count - 1].dog_loc[2]; //시작 위치를 현재 위치에 저장
        is_loc_dog[1] = loctile.dog_num[GameManager.instance.dogs.Count - 1].dog_loc[3];
        is_loc_dog[2] = loctile.dog_num[GameManager.instance.dogs.Count - 1].dog_loc[4];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pc.dog_die_num += 1;
            gameObject.SetActive(false); //플레이어와 부딪히면 사라짐.
        }
    }

    public void Boned() //매혹당한 스프라이트로 바꾸기
    {
        if (boned)
        {
            dsp_anim.SetBool("anim_bone", true);
        }
    }
}
