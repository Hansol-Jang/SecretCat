using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnAction : MonoBehaviour
{
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
        //if (Input.touchCount == 1)
        //{
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();

        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boning && !pl_con.boninging && !GameManager.instance.is_menu && !pl_con.moving)
            {
            pl_con.ReturnAction();
            }
        //}
    }
}
