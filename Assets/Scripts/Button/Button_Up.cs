using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Up : MonoBehaviour
{
    public TouchCameraInGame TCIG;

    public enum function
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NonSwipeArea,
        ReturnAction,
        Bone
    }

    public enum function2
    {
        TCIG_false
    }

    [Header("클릭 시 실행될 함수")]
    public function functionName;
    public function2 function2Name;

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

    void OnPointerDown(PointerEventData data)
    {
        SelectFunction();
    }

    void OnPointerUp(PointerEventData data)
    {
        SelectFunction2();
    }

    void SelectFunction()
    {
        switch (functionName)
        {
            case function.UP:
                UP();
                break;
            case function.DOWN:
                DOWN();
                break;
            case function.LEFT:
                LEFT();
                break;
            case function.RIGHT:
                RIGHT();
                break;
            case function.NonSwipeArea:
                NonSwipeArea();
                break;
            case function.ReturnAction:
                ReturnAction();
                break;
            case function.Bone:
                Bone();
                break;
        }
    }

    void SelectFunction2()
    {
        switch (function2Name)
        {
            case function2.TCIG_false:
                TCIG.nonbutton = false;
                break;
        }
    }

    void UP()
    {
        TCIG.nonbutton = true;
        //if (Input.touchCount == 1) {
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();
        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && (pl_con.pc_floor == BM_SC.is_floor))
        {
            if (!pl_con.boning)
            {
                TCIG.vcam_com.m_XDamping = 0.5f;
                TCIG.vcam_com.m_YDamping = 0.5f;
                TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                pl_con.Move_Up();
            }
            else
            {
                RaycastHit2D hit;
                if (pl_con.hitCollider(0, 1, out hit))
                {
                    TCIG.vcam_com.m_XDamping = 0.5f;
                    TCIG.vcam_com.m_YDamping = 0.5f;
                    TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                    TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                    player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                    pl_con.Bone_Up();
                }
            }
        }
        //}
    }
    void DOWN()
    {
        TCIG.nonbutton = true;
        //if (Input.touchCount == 1) {
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();
        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && (pl_con.pc_floor == BM_SC.is_floor))
        {
            if (!pl_con.boning)
            {
                TCIG.vcam_com.m_XDamping = 0.5f;
                TCIG.vcam_com.m_YDamping = 0.5f;
                TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                pl_con.Move_Down();
            }
            else
            {
                RaycastHit2D hit;
                if (pl_con.hitCollider(0, -1, out hit))
                {
                    TCIG.vcam_com.m_XDamping = 0.5f;
                    TCIG.vcam_com.m_YDamping = 0.5f;
                    TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                    TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                    player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                    pl_con.Bone_Down();
                }
            }
        }
        //}
    }
    void LEFT()
    {
        TCIG.nonbutton = true;
        //if (Input.touchCount == 1) {
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();
        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && (pl_con.pc_floor == BM_SC.is_floor))
        {
            if (!pl_con.boning)
            {
                TCIG.vcam_com.m_XDamping = 0.5f;
                TCIG.vcam_com.m_YDamping = 0.5f;
                TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                pl_con.Move_Left();
            }
            else
            {
                RaycastHit2D hit;
                if (pl_con.hitCollider(-1, 0, out hit))
                {
                    TCIG.vcam_com.m_XDamping = 0.5f;
                    TCIG.vcam_com.m_YDamping = 0.5f;
                    TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                    TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                    player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                    pl_con.Bone_Left();
                }
            }
        }
        //}
    }
    void RIGHT()
    {
        TCIG.nonbutton = true;
        //if (Input.touchCount == 1) {
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();
        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && (pl_con.pc_floor == BM_SC.is_floor))
        {
            if (!pl_con.boning)
            {
                TCIG.vcam_com.m_XDamping = 0.5f;
                TCIG.vcam_com.m_YDamping = 0.5f;
                TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                pl_con.Move_Right();
            }
            else
            {
                RaycastHit2D hit;
                if (pl_con.hitCollider(1, 0, out hit))
                {
                    TCIG.vcam_com.m_XDamping = 0.5f;
                    TCIG.vcam_com.m_YDamping = 0.5f;
                    TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
                    TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
                    player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
                    pl_con.Bone_Right();
                }
            }
        }
        //}
    }

    void NonSwipeArea()
    {
        TCIG.nonbutton = true;
    }

    void ReturnAction()
    {
        TCIG.nonbutton = true;
        //if (Input.touchCount == 1)
        //{
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();

        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boning && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving)
        {
            TCIG.vcam_com.m_XDamping = 0.5f;
            TCIG.vcam_com.m_YDamping = 0.5f;
            TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
            TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
            player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
            pl_con.ReturnAction();
        }
        //}
    }

    void Bone()
    {
        TCIG.nonbutton = true;
        //if (Input.touchCount == 1) {
        GameObject player = GameObject.Find("Player");
        PlayerController pl_con = player.GetComponent<PlayerController>();
        GameObject BM = GameObject.Find("BoardManager");
        BoardManager BM_SC = BM.GetComponent<BoardManager>();
        if (!pl_con.cat_death && !BM_SC.is_clear && !pl_con.boninging && !GameManager.instance.is_menu && !GameManager.instance.quit_menu && !pl_con.moving && (pl_con.pc_floor == BM_SC.is_floor))
        {
            TCIG.vcam_com.m_XDamping = 0.5f;
            TCIG.vcam_com.m_YDamping = 0.5f;
            TCIG.vcam_com.m_DeadZoneWidth = 0.5f;
            TCIG.vcam_com.m_DeadZoneHeight = 0.75f;
            player.transform.GetChild(5).transform.localPosition = new Vector3(0f, 0f, 0f);
            pl_con.Ready_Bone();
        }
        //}
    }
}
