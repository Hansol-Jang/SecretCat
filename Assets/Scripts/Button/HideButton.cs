using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//게임 오버 되었을 때, UI 숨기기
public class HideButton : MonoBehaviour {

    public GameObject MC;

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
            if (GameManager.instance.is_menu)
            {
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.SetActive(true);
                GameManager.instance.is_menu = false;
            }
        }
    }
}
