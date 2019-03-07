﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitNo : MonoBehaviour
{
    public GameObject cfg;

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
            if (GameManager.instance.quit_menu)
            {
                cfg.SetActive(false);
            GameManager.instance.quit_menu = false;
            }
        }
    }
}
