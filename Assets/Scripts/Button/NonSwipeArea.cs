using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonSwipeArea : MonoBehaviour {

    public TouchCameraInGame TCIG;

    // Use this for initialization
    void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerUp.eventID = EventTriggerType.PointerUp;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        entry_PointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
        eventTrigger.triggers.Add(entry_PointerUp);
    }

    void OnPointerUp(PointerEventData data)
    {
        TCIG.nonbutton = false;
    }

    void OnPointerDown(PointerEventData data)
    {
        TCIG.nonbutton = true;
    }
}
