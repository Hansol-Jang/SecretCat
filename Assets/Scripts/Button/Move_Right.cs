using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move_Right : MonoBehaviour {

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
        if (Input.touchCount == 1) {
            GameObject player = GameObject.Find("Player");
            PlayerController pl_con = player.GetComponent<PlayerController>();
            GameObject BM = GameObject.Find("BoardManager");
            BoardManager BM_SC = BM.GetComponent<BoardManager>();
            if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !pl_con.moving && (pl_con.pc_floor == BM_SC.is_floor))
            {
                if (!pl_con.boning)
                {
                    pl_con.Move_Right();
                }
                else
                {
                    RaycastHit2D hit;
                    if (pl_con.hitCollider(1, 0, out hit))
                    {
                        pl_con.Bone_Right();
                    }
                }
            }
        }
    }
}
