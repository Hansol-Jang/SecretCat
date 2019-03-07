using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageBackGround : MonoBehaviour {

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
            if (!GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                GameObject SM = GameObject.Find("StageManager");
                StageManager SMSC = SM.GetComponent<StageManager>();
                SMSC.NonActiveInfo(); //하이라이트 없애기
            }
        }
    }
}
