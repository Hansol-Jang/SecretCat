using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldMove : MonoBehaviour {
    public int world; //몇번째 월드인가
    public int world_start_number; //몇 번째 스테이지가 시작 번호인가
    public int world_stage_number; //스테이지를 몇 개 가지고 있는가
    public Sprite worldTrue; //월드 활성화 스프라이트

    private int star_number = 0; //월드에서 얻은 별 갯수

	// Use this for initialization
	void Start () {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);

        WorldOpen();
        TextUpdate();
    }

    void OnPointerDown(PointerEventData data)
    {
        if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu) //메뉴가 안 켜져있을 때만 눌림
            {
                if (world == 0) //world 0인 경우
                {
                    GameManager.instance.world_level = world;
                    GameManager.instance.sv_start_number = world_start_number;
                    GameManager.instance.sv_stage_number = world_stage_number;
                    LodingSceneManager.LoadScene("stageSelect"); //스테이지 선택 씬으로
                }
                else //world 0 가 아닌 경우
                {
                    if (GameManager.instance.world_clear[world - 1])
                    {
                        GameManager.instance.world_level = world;
                        GameManager.instance.sv_start_number = world_start_number;
                        GameManager.instance.sv_stage_number = world_stage_number;
                        LodingSceneManager.LoadScene("stageSelect"); //스테이지 선택 씬으로
                    }
                    else //에러 박스를 띄워라
                    {
                        GameManager.instance.is_menu = true;
                        GameObject CM = GameObject.Find("CameraMove");
                        CM.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }   
        }
    }

    void WorldOpen() //월드 활성화 스프라이트로 교체하는 함수
    {
        if (world != 0)
        {
            if (GameManager.instance.world_clear[world - 1])
            {
                transform.GetComponent<Image>().sprite = worldTrue;
            }
        }
    }

    void TextUpdate() //별 갯수 텍스트 업데이트 함수
    {
        Text starText = transform.GetChild(2).gameObject.GetComponent<Text>();
        for (int i = 0; i < world_stage_number; i++)
        {
            star_number += GameManager.instance.stage_star[world_start_number - 1 + i];
        }
        starText.text = star_number + " / " + world_stage_number * 3;
    }
}
