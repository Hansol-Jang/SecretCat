using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [HideInInspector] public int[] is_loc_portal;

    private GameObject board; // 보드판 게임오브젝트
    private BoardTileLoc loctile; // 보드타일록 스크립트

    // Use this for initialization
    protected void Start () {
        board = GameObject.Find("Board");
        loctile = board.GetComponent<BoardTileLoc>();
        is_loc_portal = new int[4]; // 0 : 포탈 인덱스, 1 : X축 위치, 2 : Y축 위치, 3 : 층 위치
        GameManager.instance.AddPortalToList(this); //리스트에 추가하라
        is_loc_portal[0] = loctile.portal_num[GameManager.instance.portals.Count - 1].portal_loc[0]; //인덱스 저장
        is_loc_portal[1] = loctile.portal_num[GameManager.instance.portals.Count - 1].portal_loc[1]; //X축 위치
        is_loc_portal[2] = loctile.portal_num[GameManager.instance.portals.Count - 1].portal_loc[2]; //Y축 위치
        is_loc_portal[3] = loctile.portal_num[GameManager.instance.portals.Count - 1].portal_loc[3]; //층 위치
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject pl = GameObject.Find("Player");
        PlayerController pl_con = pl.GetComponent<PlayerController>();
        int[] temp_loc = new int[2];
        if (collision.gameObject.tag == "Player") { //플레이어가 포탈에 들어오면
            if (pl_con.re_portal == false) {
                for (int i = 0; i < GameManager.instance.portals.Count; i++) {
                    if (GameManager.instance.portals[i].is_loc_portal[0] == -is_loc_portal[0]) {
                        temp_loc[0] = GameManager.instance.portals[i].is_loc_portal[1];
                        temp_loc[1] = GameManager.instance.portals[i].is_loc_portal[2];
                        pl_con.teleport_position = new Vector3(-3.5f + temp_loc[0], -4.5f + temp_loc[1], 0f);
                        break;
                    }
                }
                pl_con.re_portal = true;
                pl_con.por = true;
                pl_con.is_loc[0] = temp_loc[0];
                pl_con.is_loc[1] = temp_loc[1];
            }
        }
    }
}
