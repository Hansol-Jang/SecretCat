using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnAction : MonoBehaviour
{
    public TouchCameraInGame TCIG;

    // Start is called before the first frame update
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
        //if (Input.touchCount == 1)
        //{
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();

        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boning && !pl_con.boninging && !GameManager.instance.is_menu && !pl_con.moving)
            {
            TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
            TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
            player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
            pl_con.ReturnAction();
            }
        //}
    }
}
