using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DummyAdd : MonoBehaviour
{
    public GameObject bone;

    // Start is called before the first frame update
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
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,10);
        Instantiate(bone,mousePosition,Quaternion.identity);
    }
}
