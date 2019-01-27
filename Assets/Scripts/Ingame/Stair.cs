using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour {

    public int[] is_loc_stair = new int[5]; //0 : 계단 인덱스, 1 : 계단 상하 2 : 계단의 X축 위치, 3 : 계단의 Y축 위치, 4 : 계단의 층 위치;

    private GameObject board; // 보드판 게임오브젝트
    private BoardTileLoc loctile; // 보드타일록 스크립트

    private int floor_dog_list;

    // Use this for initialization
    protected void Start()
    {
        board = GameObject.Find("Board");
        loctile = board.GetComponent<BoardTileLoc>();
        GameManager.instance.AddStairToList(this); //리스트에 추가하라
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject pl = GameObject.Find("Player");
        PlayerController pl_con = pl.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();
        GameObject fl1 = GameObject.Find("Main Camera").gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        GameObject fl2 = GameObject.Find("Main Camera").gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        GameObject fl3 = GameObject.Find("Main Camera").gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        if (collision.gameObject.tag == "Player")
        { //플레이어가 계단에 들어오면
            if (pl_con.re_portal == false)
            {
                pl_con.StairSound();
                int temp = is_loc_stair[1];
                switch (pl_con.pc_floor + is_loc_stair[1])
                {
                    case 1:
                        switch (loctile.floorNumber)
                        {
                            case 1:
                                board.transform.GetChild(0).gameObject.SetActive(true);
                                break;
                            case 2:
                                board.transform.GetChild(0).gameObject.SetActive(true);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                break;
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(true);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                board.transform.GetChild(2).gameObject.SetActive(false);
                                break;
                            default:
                                break;
                        }
                        floor_dog_list = board.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                        for (int i = 0; i < floor_dog_list; i++)
                        {
                            board.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                        }
                        fl1.SetActive(true);
                        fl2.SetActive(false);
                        fl3.SetActive(false);
                        break;
                    case 2:
                        switch (loctile.floorNumber)
                        {
                            case 2:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(true);
                                break;
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(true);
                                board.transform.GetChild(2).gameObject.SetActive(false);
                                break;
                            default:
                                break;
                        }
                        floor_dog_list = board.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                        for (int i = 0; i < floor_dog_list; i++)
                        {
                            board.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                        }
                        fl1.SetActive(false);
                        fl2.SetActive(true);
                        fl3.SetActive(false);
                        break;
                    case 3:
                        switch (loctile.floorNumber)
                        {
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                board.transform.GetChild(2).gameObject.SetActive(true);
                                break;
                            default:
                                break;
                        }
                        floor_dog_list = board.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                        for (int i = 0; i < floor_dog_list; i++)
                        {
                            board.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                        }
                        fl1.SetActive(false);
                        fl2.SetActive(false);
                        fl3.SetActive(true);
                        break;
                    default:
                        break;
                }
                pl_con.teleport_position = pl.transform.position;
                pl_con.sp_teleport_position = pl.transform.GetChild(0).transform.localPosition;
                pl_con.re_portal = true;
                pl_con.por = true;
                pl_con.pc_floor += temp;
                BM_SC.is_floor += temp;
            }
        }
    }
}
