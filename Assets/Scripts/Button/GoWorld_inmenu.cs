using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//인게임에서 월드맵으로 가기
public class GoWorld_inmenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
            if (GameManager.instance.is_menu)
            {
                GameManager.instance.is_menu = false;
                LodingSceneManager.LoadScene("worldSelect");
            }
        }
    }
}
