using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelect : MonoBehaviour
{
    public int stage_level;
    public int stage_num;
    public bool touched = false;
    public Sprite stage_true;

    // Use this for initialization
    void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
        StageOpen();
        DisplayStar();
    }

    void OnPointerDown(PointerEventData data)
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu) {
                if (stage_level != 1)
                {
                    if (GameManager.instance.stage_clear[stage_level - 2])
                    {
                        if (touched)
                        {
                            GameManager.instance.level = stage_level; //스테이지 레벨을 게임 매니저에 보냄
                            LodingSceneManager.LoadScene("ingame"); //인게임 씬을 불러옴
                        }
                        else
                        {
                            TouchOnce();
                        }
                    }
                    else
                    {
                        GameManager.instance.is_menu = true;
                        GameObject CM = GameObject.Find("CameraMove");
                        CM.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (touched)
                    {
                        GameManager.instance.level = stage_level; //스테이지 레벨을 게임 매니저에 보냄
                        LodingSceneManager.LoadScene("ingame"); //인게임 씬을 불러옴
                    }
                    else
                    {
                        TouchOnce();
                    }
                }
            }
        }
    }

    void TouchOnce() //첫 터치
    {
        GameObject SM = GameObject.Find("StageManager");
        StageManager SMSC = SM.GetComponent<StageManager>();
        SMSC.touchStage = stage_num;
        SMSC.ActiveInfo();
    }

    void StageOpen()
    {
        if (stage_level != 1)
        {
            if (GameManager.instance.stage_clear[stage_level - 2])
            {
                transform.GetComponent<Image>().sprite = stage_true;
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    void DisplayStar()
    {
        if (GameManager.instance.stage_star[stage_level - 1] >= 1)
        {
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameManager.instance.stage_star[stage_level - 1] >= 2)
        {
            transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        if (GameManager.instance.stage_star[stage_level - 1] == 3)
        {
            transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.SetActive(true);
        }
    }
}