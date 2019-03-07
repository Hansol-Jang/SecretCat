using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//튜토리얼 진행하는 버튼
public class Tutorial_Button : MonoBehaviour {

    public Tutorial_Process TP;

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
            if (GameManager.instance.is_menu && !GameManager.instance.quit_menu)
            {
                TP.tutorial_process += 1;
                TP.GoProcess();
            }
        }
    }
}
