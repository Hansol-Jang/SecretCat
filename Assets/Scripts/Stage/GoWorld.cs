﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoWorld : MonoBehaviour {

    // Use this for initialization
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
            if (!GameManager.instance.is_menu) { //메뉴가 켜지지 않았다면
                LodingSceneManager.LoadScene("worldSelect");
            }
        }
    }
}
