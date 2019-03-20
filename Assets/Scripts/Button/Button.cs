using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public enum function
    {
        Back_inmenu,
        Error,
        GoWorld_inmenu,
        Hide,
        Floor1,
        Floor2,
        Floor3,
        NextStage,
        No,
        QuitNo,
        QuitYes,
        Restart_inmenu,
        See,
        Yes,
        StageBackGround,
        GoWorld,
        Restart,
        GoStageSelect
    }

    [Header("클릭 시 실행될 함수")]
    public function functionName;

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
        SelectFunction();
    }

    void SelectFunction()
    {
        switch (functionName)
        {
            case function.Back_inmenu:
                Back_inmenu();
                break;
            case function.Error:
                Error();
                break;
            case function.GoWorld_inmenu:
                GoWorld_inmenu();
                break;
            case function.Hide:
                Hide(transform.GetComponent<HideButton>().MC);
                break;
            case function.Floor1:
                Floor(1, transform.GetComponent<Floor>().BM, transform.GetComponent<Floor>().fl1, transform.GetComponent<Floor>().fl2, transform.GetComponent<Floor>().fl3);
                break;
            case function.Floor2:
                Floor(2, transform.GetComponent<Floor>().BM, transform.GetComponent<Floor>().fl1, transform.GetComponent<Floor>().fl2, transform.GetComponent<Floor>().fl3);
                break;
            case function.Floor3:
                Floor(3, transform.GetComponent<Floor>().BM, transform.GetComponent<Floor>().fl1, transform.GetComponent<Floor>().fl2, transform.GetComponent<Floor>().fl3);
                break;
            case function.NextStage:
                NextStage();
                break;
            case function.No:
                NoButton(transform.GetComponent<NoButton>().cfg);
                break;
            case function.QuitNo:
                QuitNo(transform.GetComponent<NoButton>().cfg);
                break;
            case function.QuitYes:
                QuitYes();
                break;
            case function.Restart_inmenu:
                Restart_inmenu();
                break;
            case function.See:
                See(transform.GetComponent<SeeButton>().MC);
                break;
            case function.Yes:
                Yes();
                break;
            case function.StageBackGround:
                StageBackGround();
                break;
            case function.GoWorld:
                GoWorld();
                break;
            case function.Restart:
                Restart();
                break;
            case function.GoStageSelect:
                GoStageSelect(transform.GetComponent<NoButton>().cfg);
                break;
        }
    }

    void Restart()
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                LodingSceneManager.LoadScene("ingame");
            }
        }
    }

    void GoStageSelect(GameObject cfg)
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameManager.instance.is_menu = true;
                cfg.SetActive(true);
            }
        }
    }

    void StageBackGround()
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameObject SM = GameObject.Find("StageManager");
                StageManager SMSC = SM.GetComponent<StageManager>();
                SMSC.NonActiveInfo(); //하이라이트 없애기
            }
        }
    }

    void GoWorld()
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            { //메뉴가 켜지지 않았다면
                LodingSceneManager.LoadScene("worldSelect");
            }
        }
    }

    void Back_inmenu()
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameManager.instance.is_menu = false;
                LodingSceneManager.LoadScene("stageSelect");
            }
        }
    }

    void Error()
    {
        if (Input.touchCount == 1 && GameManager.instance.is_menu && !GameManager.instance.quit_menu) //터치했고 메뉴가 켜져있다면
        {
            GameManager.instance.is_menu = false;
            transform.gameObject.SetActive(false); //사라짐
        }
    }

    void GoWorld_inmenu()
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameManager.instance.is_menu = false;
                LodingSceneManager.LoadScene("worldSelect");
            }
        }
    }

    void Hide(GameObject MC)
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.SetActive(true);
                GameManager.instance.is_menu = false;
            }
        }
    }

    void Floor(int fl_num, BoardManager BM, GameObject fl1, GameObject fl2, GameObject fl3)
    {
        int floor_dog_list;

        if (Input.touchCount == 1)
        {
            GameObject player = GameObject.Find("Player");
            PlayerController pl_con = player.GetComponent<PlayerController>();
            if (!BM.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && !pl_con.cat_death)
            {
                if (BM.is_floor != fl_num) //보드판이 해당 층을 보이지 않는다면
                {
                    BM.is_floor = fl_num;
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
                    if (fl_num == 1)
                    {
                        switch (btl.floorNumber) //플로어 등장&제거 및 하이라이트
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
                    }
                    else if (fl_num == 2)
                    {
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
                    }
                    else if (fl_num == 3)
                    {
                        switch (btl.floorNumber) //플로어 등장&제거 및 하이라이트
                        {
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                board.transform.GetChild(2).gameObject.SetActive(true);
                                break;
                            default:
                                break;
                        }
                    }
                    
                    floor_dog_list = board.transform.GetChild(fl_num-1).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                    for (int i = 0; i < floor_dog_list; i++)
                    {
                        board.transform.GetChild(fl_num-1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                    }

                    if (fl_num == 1)
                    {
                        fl1.SetActive(true);
                        fl2.SetActive(false);
                        fl3.SetActive(false);
                    }
                    else if (fl_num == 2)
                    {
                        fl1.SetActive(false);
                        fl2.SetActive(true);
                        fl3.SetActive(false);
                    }
                    else if (fl_num == 3)
                    {
                        fl1.SetActive(false);
                        fl2.SetActive(false);
                        fl3.SetActive(true);
                    }
                }
            }
        }
    }

    void NextStage()
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameManager.instance.is_menu = false;
                GameManager.instance.level += 1;
                LodingSceneManager.LoadScene("ingame");
            }
        }
    }

    void NoButton(GameObject cfg)
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                cfg.SetActive(false);
                GameManager.instance.is_menu = false;
            }
        }
    }
    
    void QuitNo(GameObject cfg)
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.quit_menu)
            {
                cfg.SetActive(false);
                GameManager.instance.quit_menu = false;
            }
        }
    }

    void QuitYes()
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.quit_menu)
            {
                Application.Quit();
            }
        }
    }

    void Restart_inmenu()
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameManager.instance.is_menu = false;
                LodingSceneManager.LoadScene("ingame");
            }
        }
    }

    void See(GameObject MC)
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                GameManager.instance.is_menu = true;
            }
        }
    }

    void Yes()
    {
        if (Input.touchCount == 1)
        {
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameManager.instance.is_menu = false;
                LodingSceneManager.LoadScene("stageSelect"); //스테이지 선택 씬으로
            }
        }
    }
}