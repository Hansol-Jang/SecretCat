using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LogData
{
    public int[] log_pl_loc = new int[3]; //0 : X축 좌표 1 : Y축 좌표 2 : 층
    public int[] log_die_dog_loc = new int[2]; //0 : 층, 1 : 자식 인덱스
    public int[] log_bone_dog_loc = new int[2]; //0 : 층, 1 : 자식 인덱스
    public int log_cat_dir; //방향 인덱스
    public int log_mv_num; //이동거리
    public int log_bone_num; //뼈다귀 갯수
    public int log_dog_die_num; //개 죽인 횟수
    public bool log_re_portal; //다시 포탈을 탈 수 있는가?
    public bool log_dog_die; //개가 죽었는가?
    public bool log_dog_bone; //개가 매혹당했는가?
}

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.1f;
    public Vector3 teleport_position;
    public Vector3 sp_teleport_position;
    public LayerMask blockingLayer;
    public LayerMask blockingLayer2;
    public GameObject bone;
    public Sprite bone_sp;
    public List<LogData> log = new List<LogData>();
    [HideInInspector] public int cat_dir = 1; //방향 인덱스
    [HideInInspector] public int dog_die_num = 0; //개 죽인 횟수
    [HideInInspector] public int bone_num; //남은 뼈 갯수
    [HideInInspector] public int pc_floor; //플레이어 층 위치
    [HideInInspector] public bool mv; //움직였는지 여부
    [HideInInspector] public int mv_num = 0; //움직인 횟수
    [HideInInspector] public bool moving = false; //움직이고 있는지 여부
    [HideInInspector] public bool boning = false; // 뼈다귀 던질 준비 하고 있니?
    [HideInInspector] public bool boninging = false; //뼈다귀 던져지고 있니?
    [HideInInspector] public bool cat_death = false; // 게임 오버 여부
    [HideInInspector] public bool re_portal = false; // 포탈 다시 탈 수 있는지 여부
    [HideInInspector] public bool por = false; //포탈을 탔는지 여부
    [HideInInspector] public int[] is_loc = new int[2]; // 고양이 현재 위치
    [HideInInspector] public SpriteRenderer dog_sr; // 죽인 강아지 스프라이트

    private bool mv_up = false; // 위로 가니?
    private bool mv_dw = false; // 아래로 가니?
    private bool mv_le = false; // 왼쪽으로 가니?
    private bool mv_ri = false; // 오른쪽으로 가니?
    private int horizon = 0; // 횡이동
    private int vertical = 0; // 종이동
    private float inverseMoveTime;

    private GameObject MC;// 메인카메라 게임오브젝트
    private GameObject board; // 보드판 게임오브젝트
    private GameObject sp; // Player_Sprite
    private GameObject BM; //보드매니저 게임오브젝트
    private GameObject fl1; //Floor1
    private GameObject fl2; //Floor2
    private GameObject fl3; //Floor3
    private BoardManager BM_SC; //보드매니저 스크립트
    private BoardTileLoc loctile; // 보드타일록 스크립트
    private BoxCollider2D boxCollider;
    private Rigidbody2D rg2D;
    private PlayerSprite ps_sp;
    private Text boneText; //뼈다귀 던지는 횟수
    private Text moveText; //이동한 횟수


    private IEnumerator movement; //강제로 멈추게 하기 위해 만듬

    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rg2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
        MC = GameObject.Find("Main Camera");
        fl1 = MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        fl2 = MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        fl3 = MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        board = GameObject.Find("Board");
        BM = GameObject.Find("BoardManager");
        BM_SC = BM.GetComponent<BoardManager>();
        loctile = board.GetComponent<BoardTileLoc>();
        sp = transform.GetChild(0).gameObject;
        ps_sp = sp.GetComponent<PlayerSprite>();
        boneText = MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        moveText = MC.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        bone_num = loctile.bone_limit;
        pc_floor = loctile.player_loc[2];
        boneText.text = bone_num.ToString();
        moveText.text = mv_num.ToString();
        is_loc = loctile.player_loc; // 시작 위치를 현재 위치에 저장
        AddLogData();
    }

    // Update is called once per frame
    void Update()
    {
        if (por) //포탈 탔을 때, 움직임 함수를 멈추고 텔레포트시킴
        {
            StopCoroutine(movement);
            transform.position = teleport_position;
            sp.transform.localPosition = sp_teleport_position;
            por = false;
            log.RemoveAt(log.Count - 1);
            AddLogData();
        }
        if (GameManager.instance.playerTurn == true && !cat_death && !BM_SC.is_clear) // 플레이어 턴이라면
        {
            if (mv_up == true) {
                mv_up = false;
                horizon = 0;
                vertical = 1;
                RaycastHit2D hit;
                Move(horizon, vertical, out hit, 3); // 위로 가라. 단, blocking layer와 만나지 않는다면.
                if (mv)
                {
                    cat_dir = 3;
                    is_loc[1] += 1; // Y축 위치 +1
                    mv_num += 1;
                    moveText.text = mv_num.ToString();
                    GameManager.instance.playerTurn = false; // 플레이어 턴에서 강아지 턴으로 넘어감
                    GameManager.instance.dogTurn = true;
                    re_portal = false; //이동할 때, re_portal 바뀜
                    AddLogData();
                    ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
                }
                else moving = false;
            }
            else if (mv_dw == true)
            {
                mv_dw = false;
                horizon = 0;
                vertical = -1;
                RaycastHit2D hit;
                Move(horizon, vertical, out hit, 1); // 아래로 가라. 단, blocking layer와 만나지 않는다면.
                if (mv)
                {
                    cat_dir = 1;
                    is_loc[1] -= 1; //Y축 위치 -1
                    mv_num += 1;
                    moveText.text = mv_num.ToString();
                    GameManager.instance.playerTurn = false;
                    GameManager.instance.dogTurn = true;
                    re_portal = false;
                    AddLogData();
                    ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
                }
                else moving = false;
            }
            else if (mv_le == true)
            {
                mv_le = false;
                horizon = -1;
                vertical = 0;
                RaycastHit2D hit;
                Move(horizon, vertical, out hit, 2); // 왼쪽으로 가라. 단, blocking layer와 만나지 않는다면.
                if (mv)
                {
                    cat_dir = 2;
                    is_loc[0] -= 1; //X축 위치 -1
                    mv_num += 1;
                    moveText.text = mv_num.ToString();
                    GameManager.instance.playerTurn = false;
                    GameManager.instance.dogTurn = true;
                    re_portal = false;
                    AddLogData();
                    ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
                }
                else moving = false;
            }
            else if (mv_ri == true)
            {
                mv_ri = false;
                horizon = 1;
                vertical = 0;
                RaycastHit2D hit;
                Move(horizon, vertical, out hit, 4); // 오른쪽으로 가라. 단, blocking layer와 만나지 않는다면.
                if (mv)
                {
                    cat_dir = 4;
                    is_loc[0] += 1; //X축 위치 +1
                    mv_num += 1;
                    moveText.text = mv_num.ToString();
                    GameManager.instance.playerTurn = false;
                    GameManager.instance.dogTurn = true;
                    re_portal = false;
                    AddLogData();
                    ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
                }
                else moving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackRange") //공격 범위랑 닿았을 때
        {
            cat_death = true;
            GameManager.instance.is_menu = true;
            dog_sr = collision.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

            //강아지가 공격하는 스프라이트로 바뀌는 코드
            //dog_sr.sprite = collision.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<DogSprite>().sp_atk;
            GameManager.instance.GameOver(); //게임오버 함수를 불러와라
        }
        if (collision.gameObject.tag == "ClearTile" && !cat_death) //클리어 타일에 닿았을 때
        {
            StartCoroutine("ClearTime");
        }
    }

    protected IEnumerator ClearTime()
    {
        yield return new WaitForSeconds(0.1f);
        if (!cat_death)
        {
            BM_SC.is_clear = true;
            GameManager.instance.is_menu = true;
            GameManager.instance.StageClear();
        }
    }

    public void Die() //죽을 때 스프라이트 바뀌는 함수
    {
            ps_sp.cat_anim.SetBool("Die", true);
    }

    public void ClearSP() //클리어했을 때 스프라이트 바뀌는 함수
    {
            ps_sp.cat_anim.SetBool("Clear", true);
    }

    public void Move_Up() { //위로 움직이는 함수
        mv_up = true;
        moving = true;
    }

    public void Move_Down() { //아래로 움직이는 함수
        mv_dw = true;
        moving = true;
    }

    public void Move_Left() //왼쪽으로 움직이는 함수
    {
        mv_le = true;
        moving = true;
    }

    public void Move_Right() //오른쪽으로 움직이는 함수
    {
        mv_ri = true;
        moving = true;
    }

    void RangeFalse() //뼈 범위 없애는 함수
    {
        boning = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        ps_sp.cat_anim.SetBool("Bone", false);
    }

    public void Ready_Bone() //뼈다귀 액션 버튼 눌렀을 때 나타나는 함수
    {
        if (!boning && bone_num != 0)
        {
            boning = true;
            GameObject bone_instance = Instantiate(bone) as GameObject;//뼈다귀 생성
            bone_instance.GetComponent<SpriteRenderer>().sprite = null;
            bone_instance.name = "Bone";
            ps_sp.cat_anim.SetBool("Bone", true);
            bone_instance.transform.position = transform.position + new Vector3(0f,0f,0.15f);
            RaycastHit2D hit;
            if (hitCollider(-1,0,out hit))
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }
            if (hitCollider(1, 0, out hit))
            {
                transform.GetChild(4).gameObject.SetActive(true);
            }
            if (hitCollider(0, -1, out hit))
            {
                transform.GetChild(2).gameObject.SetActive(true);
            }
            if (hitCollider(0, 1, out hit))
            {
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else if (boning)
        {
            RangeFalse();
            Destroy(GameObject.Find("Bone"));
        }
    }

    public void Bone_Up() //뼈다귀 위로 던지기
    {
        cat_dir = 3;
        boninging = true;
        ps_sp.cat_anim.SetInteger("Boning", 3);
        GameObject bone_in = GameObject.Find("Bone");
        bone_in.GetComponent<SpriteRenderer>().sprite = bone_sp;
        bone_num -= 1;
        boneText.text = bone_num.ToString();
        AddLogData();
        ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
        StartCoroutine(SmoothBone(0, 1, 0f, 1f, 0f, 0f, 0.05f, 0.05f, 0.0030f, 0.0030f, bone_in.transform.position + new Vector3(0f, 1f, 0f)));
    }

    public void Bone_Down() //뼈다귀 아래로 던지기
    {
        cat_dir = 1;
        boninging = true;
        ps_sp.cat_anim.SetInteger("Boning", 1);
        GameObject bone_in = GameObject.Find("Bone");
        bone_in.GetComponent<SpriteRenderer>().sprite = bone_sp;
        bone_num -= 1;
        boneText.text = bone_num.ToString();
        AddLogData();
        ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
        StartCoroutine(SmoothBone(0, -1, 0f, -1f, 0f, 0f, 0.05f, 0.05f, 0.0030f, 0.0030f, bone_in.transform.position + new Vector3(0f, -1f, 0f)));
    }

    public void Bone_Left() //뼈다귀 왼쪽으로 던지기
    {
        cat_dir = 2;
        boninging = true;
        ps_sp.cat_anim.SetInteger("Boning", 2);
        GameObject bone_in = GameObject.Find("Bone");
        bone_in.GetComponent<SpriteRenderer>().sprite = bone_sp;
        bone_num -= 1;
        boneText.text = bone_num.ToString();
        AddLogData();
        ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
        StartCoroutine(SmoothBone(-1, 0, -1f, 1f, 0f, 0.075f, 0f, 0f, 0f, 0f, bone_in.transform.position + new Vector3(-1f, 0f, 0f)));
    }

    public void Bone_Right() //뼈다귀 오른쪽으로 던지기
    {
        cat_dir = 4;
        boninging = true;
        ps_sp.cat_anim.SetInteger("Boning", 4);
        GameObject bone_in = GameObject.Find("Bone");
        bone_in.GetComponent<SpriteRenderer>().sprite = bone_sp;
        bone_num -= 1;
        boneText.text = bone_num.ToString();
        AddLogData();
        ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir);
        StartCoroutine(SmoothBone(1, 0, 1f, 1f, 0f, 0.075f, 0f, 0f, 0f, 0f, bone_in.transform.position + new Vector3(1f,0f,0f)));
    }

    public void Scoring() //스코어링
    {
        int temp_score = 100 -(2*mv_num)+(6*(loctile.dog_num.Length - dog_die_num))+(20*bone_num);
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "스테이지\n" + GameManager.instance.world_level + " - " + (GameManager.instance.level - GameManager.instance.sv_start_number + 1);
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = (loctile.dog_num.Length - dog_die_num) + " x 6 = " + (6 * (loctile.dog_num.Length - dog_die_num));
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = bone_num + " x 20 = " + (20 * bone_num);
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = mv_num + " x 2 = " + (2 * mv_num);
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(9).gameObject.GetComponent<Text>().text = "" + temp_score;
        if (temp_score >= GameManager.instance.stage_score[GameManager.instance.level - 1])
        {
            GameManager.instance.stage_score[GameManager.instance.level - 1] = temp_score;
        }
        int temp_star;
        if (temp_score >= loctile.three_star_point)
        {
            temp_star = 3;
        }
        else if ((temp_score < loctile.three_star_point) && (temp_score >= loctile.two_star_point))
        {
            temp_star = 2;
        }
        else temp_star = 1;
        if (temp_star >= GameManager.instance.stage_star[GameManager.instance.level - 1])
        {
            GameManager.instance.stage_star[GameManager.instance.level - 1] = temp_star;
        }
        switch (temp_star)
        {
            case 1:
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 3:
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(3).gameObject.SetActive(true);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject.SetActive(false);
                MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(5).gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void AddLogData() //로그 데이터 만들기
    {
        LogData log_data = new LogData(); //로그데이터 작성
        log_data.log_pl_loc[0] = is_loc[0];
        log_data.log_pl_loc[1] = is_loc[1];
        log_data.log_cat_dir = cat_dir;
        log_data.log_pl_loc[2] = pc_floor; //위치 저장
        log_data.log_mv_num = mv_num; //이동 거리 저장
        log_data.log_bone_num = bone_num; //남은 뼈다귀 저장
        log_data.log_dog_die_num = dog_die_num; //죽인 강아지 숫자 저장
        log_data.log_re_portal = re_portal; //다시 포탈탈 수 있는지 여부 저장
        log_data.log_dog_die = false; //강아지가 죽었나?
        log_data.log_dog_bone = false; //강아지가 매혹당했나?
        AddLogDataToList(log_data); //로그데이터에 저장
    }

    private void AddLogDataToList(LogData logdata) //리스트에 저장
    {
        log.Add(logdata);
    }

    public void ReturnAction() //직전 행동으로 돌아가는 기능
    {
        if (log.Count > 1)
        {
            //직전 행동의 변수 불러옴
            is_loc[0] = log[log.Count - 2].log_pl_loc[0];
            is_loc[1] = log[log.Count - 2].log_pl_loc[1];
            cat_dir = log[log.Count - 2].log_cat_dir;
            pc_floor = log[log.Count - 2].log_pl_loc[2];
            BM_SC.is_floor = log[log.Count - 2].log_pl_loc[2];
            mv_num = log[log.Count - 2].log_mv_num;
            bone_num = log[log.Count - 2].log_bone_num;
            dog_die_num = log[log.Count - 2].log_dog_die_num;
            re_portal = log[log.Count - 2].log_re_portal;

            //직전 행동으로 모든 걸 바꿈
            transform.position = new Vector3(-3.5f + is_loc[0], -4.5f + is_loc[1], 0f); //고양이 위치
            transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.3f, is_loc[1] - 0.5f); //고양이 원근감
            if (log[log.Count - 1].log_dog_die) //죽은 개 살리기
            {
                board.transform.GetChild(log[log.Count - 1].log_die_dog_loc[0] - 1).GetChild(1).GetChild(log[log.Count - 1].log_die_dog_loc[1]).gameObject.SetActive(true);
            }
            if(log[log.Count - 1].log_dog_bone) //매혹당한 개 풀기
            {
                board.transform.GetChild(log[log.Count - 1].log_bone_dog_loc[0] - 1).GetChild(1).GetChild(log[log.Count - 1].log_bone_dog_loc[1]).gameObject.GetComponent<Dog>().boned = false;
                board.transform.GetChild(log[log.Count - 1].log_bone_dog_loc[0] - 1).GetChild(1).GetChild(log[log.Count - 1].log_bone_dog_loc[1]).GetChild(1).gameObject.tag = "AttackRange";
                board.transform.GetChild(log[log.Count - 1].log_bone_dog_loc[0] - 1).GetChild(1).GetChild(log[log.Count - 1].log_bone_dog_loc[1]).gameObject.GetComponent<Dog>().dsp_anim.SetBool("anim_bone", false);
                board.transform.GetChild(log[log.Count - 1].log_bone_dog_loc[0] - 1).GetChild(1).GetChild(log[log.Count - 1].log_bone_dog_loc[1]).gameObject.GetComponent<Dog>().dsp.transform.localPosition = board.transform.GetChild(log[log.Count - 1].log_bone_dog_loc[0] - 1).GetChild(1).GetChild(log[log.Count - 1].log_bone_dog_loc[1]).gameObject.GetComponent<Dog>().dsp_ve3;
            }
            sp.SetActive(true); //고양이 스프라이트 활성화
            ps_sp.cat_anim.SetInteger("Idle_Dir", cat_dir); //고양이 방향 설정

            //층 보이기
                switch (pc_floor)
                {
                    case 1:
                        switch (loctile.floorNumber)
                        {
                            case 1:
                                board.transform.GetChild(0).gameObject.SetActive(true);
                                break;
                            case 2:
                                board.transform.GetChild(0).gameObject.SetActive(true);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                break;
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(true);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                board.transform.GetChild(2).gameObject.SetActive(false);
                                break;
                            default:
                                break;
                        }

                        int floor1_dog_list = board.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                        for (int i = 0; i < floor1_dog_list; i++)
                        {
                            board.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                        }
                        fl1.SetActive(true);
                        fl2.SetActive(false);
                        fl3.SetActive(false);
                        break;
                    case 2:
                        switch (loctile.floorNumber)
                        {
                            case 2:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(true);
                                break;
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(true);
                                board.transform.GetChild(2).gameObject.SetActive(false);
                                break;
                            default:
                                break;
                        }

                        int floor2_dog_list = board.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                        for (int i = 0; i < floor2_dog_list; i++)
                        {
                            board.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                        }
                        fl1.SetActive(false);
                        fl2.SetActive(true);
                        fl3.SetActive(false);
                        break;
                    case 3:
                        switch (loctile.floorNumber)
                        {
                            case 3:
                                board.transform.GetChild(0).gameObject.SetActive(false);
                                board.transform.GetChild(1).gameObject.SetActive(false);
                                board.transform.GetChild(2).gameObject.SetActive(true);
                                break;
                            default:
                                break;
                        }
                        int floor3_dog_list = board.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.childCount;
                        for (int i = 0; i < floor3_dog_list; i++)
                        {
                            board.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Dog>().Boned();
                        }
                        fl1.SetActive(false);
                        fl2.SetActive(false);
                        fl3.SetActive(true);
                        break;
                    default:
                        break;
                }
            moveText.text = mv_num.ToString(); //이동 거리 텍스트 바꿈
            boneText.text = bone_num.ToString(); //남은 뼈다귀 텍스트 바꿈

            //리스트에서 마지막 요소들 삭제
            log.RemoveAt(log.Count - 1);
        }
    }

    public bool hitCollider(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        if (hit.transform == null)
        {
            return true;
        }
        else return false;
    }

    protected void Move(int xDir, int yDir, out RaycastHit2D hit, int anim_dir) //움직임 함수
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        if (hit.transform == null) //blockingLayer와 만났니?
        {
            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer2);
            boxCollider.enabled = true;
            if (hit.transform == null)
            {
                ps_sp.cat_anim.SetInteger("Move", anim_dir);
                
            }
            else
            {
                ps_sp.cat_anim.SetInteger("Attack", anim_dir);
            }

            movement = SmoothMovement(end, yDir);
            StartCoroutine(movement);

            mv = true;
            return;
        }

        else
        {
            mv = false;
            return;
        }
    }

    protected IEnumerator SmoothMovement(Vector3 end,int yDir)
    {
        if (yDir == -1)
        {
            sp.transform.localPosition += new Vector3(0f, 0f, -1f);
        }
        boxCollider.enabled = false; //이동 시작할 때 박스컬라이더 없앰 --> 다 이동해야 상호작용하기 위해서
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rg2D.position, end, inverseMoveTime * Time.deltaTime/2.5f);
            rg2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        boxCollider.enabled = true; //이동 끝날 때 박스컬라이더 다시 생김
        if (yDir == 1)
        {
            sp.transform.localPosition += new Vector3(0f, 0f, 1f);
        }
        moving = false;
    }

    protected IEnumerator SmoothBone(int x, int y, float horizontalMove, float verticalMove, float x_accel, float y_accel, float x_scale, float y_scale, float x_scale_accel, float y_scale_accel, Vector3 end)
    {
        GameObject bone_in = GameObject.Find("Bone");
        float sqrRemainingDistance = (bone_in.transform.position - end).sqrMagnitude;
        float x_speed = 1.5f;
        float y_speed = 1.5f;
        if (y != 0)
        {
            while (sqrRemainingDistance > 0.1f)
            {
                bone_in.transform.localScale += new Vector3(x_scale, y_scale, 0f);
                bone_in.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalMove * x_speed, verticalMove * y_speed, 0f);
                sqrRemainingDistance = (bone_in.transform.position - end).sqrMagnitude;
                x_speed -= x_accel;
                y_speed -= y_accel;
                x_scale -= x_scale_accel;
                y_scale -= y_scale_accel;
                yield return null;
            }
        }
        else
        {
            while (sqrRemainingDistance > 0.3f)
            {
                bone_in.transform.localScale += new Vector3(x_scale, y_scale, 0f);
                bone_in.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalMove * x_speed, verticalMove * y_speed, 0f);
                sqrRemainingDistance = (bone_in.transform.position - end).sqrMagnitude;
                x_speed -= x_accel;
                y_speed -= y_accel;
                x_scale -= x_scale_accel;
                y_scale -= y_scale_accel;
                yield return null;
            }
        }
        
        Destroy(bone_in);
        for (int i = 0; i < GameManager.instance.dogs.Count; i++)
        {
            if (GameManager.instance.dogs[i].is_loc_dog[0] == is_loc[0] + x && GameManager.instance.dogs[i].is_loc_dog[1] == is_loc[1] + y && GameManager.instance.dogs[i].is_loc_dog[2] == pc_floor)
            {
                log[log.Count - 1].log_dog_bone = true;
                log[log.Count - 1].log_bone_dog_loc[0] = GameManager.instance.dogs[i].is_loc_dog[2];
                log[log.Count - 1].log_bone_dog_loc[1] = GameManager.instance.dogs[i].transform.GetSiblingIndex();
                GameManager.instance.dogs[i].boned = true;
                GameManager.instance.dogs[i].gameObject.transform.GetChild(1).gameObject.tag = "BonedRange";
                GameManager.instance.dogs[i].Boned();
                break;
            }
        }
        RangeFalse();
        boninging = false;
    }
}
