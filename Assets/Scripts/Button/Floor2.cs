using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor2 : MonoBehaviour {

    public BoardManager BM;
    public GameObject fl1;
    public GameObject fl2;
    public GameObject fl3;

    private int floor_dog_list;

    void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
    }

    void OnPointerDown(PointerEventData data)
    {
        if (Input.touchCount == 1)
        {
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        if (!BM.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && !pl_con.cat_death)
        {
            if (BM.is_floor != 2) //보드판이 2층을 보이지 않는다면
            {
                BM.is_floor = 2;
                GameObject board = BM.transform.GetChild(0).gameObject;
                BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
                if (pl_con.pc_floor != BM.is_floor) //플레이어 층 위치랑 보드판 층 위치랑 같지 않다면 플레이어를 없애라
                {
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    player.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    player.transform.GetChild(0).gameObject.SetActive(true);
                    player.GetComponent<BoxCollider2D>().enabled = true;
                }
                switch (btl.floorNumber) //플로어 등장&제거 및 하이라이트
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
            }
        }
        }
    }
}
