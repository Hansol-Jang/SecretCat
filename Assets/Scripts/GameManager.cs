using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool is_menu = false; //메뉴 열렸니?
    public int world_level = 0; //월드 넘버
    public int level = 0; //레벨 넘버
    public int sv_start_number; //월드 시작 스테이지 넘버를 저장한다
    public int sv_stage_number; //월드의 스테이지 수를 저장한다
    public float levelStartDelay = 2f;

    public List<Dog> dogs; //Dog의 리스트
    public List<Portal> portals; //Portal의 리스트
    public List<Stair> stairs; //Stair의 리스트
    public static GameManager instance = null;

    [HideInInspector] public int[] stage_score = new int[15]; //최대 점수 --> 스테이지 늘어날 때마다 늘려줘야함
    [HideInInspector] public int[] stage_star = new int[15]; //스테이지별 별 --> 스테이지 늘어날 때마다 늘려줘야함
    [HideInInspector] public bool[] world_clear = new bool[3]; //월드 클리어 모아둔 배열 --> 월드 늘어날 때마다 늘려줘야함
    [HideInInspector] public bool[] stage_clear = new bool[15]; //스테이지 클리어 모아둔 배열 --> 스테이지 늘어날 때마다 늘려줘야함
    [HideInInspector] public bool playerTurn = true; //플레이어 턴?
    [HideInInspector] public bool dogTurn; //강아지 턴?
    
    private bool doingSetup; //세팅중...

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //파괴되지않아!
        dogs = new List<Dog>();
        portals = new List<Portal>();
        stairs = new List<Stair>();
        for (int i = 0; i < world_clear.Length; i++)
        {
            world_clear[i] = true;
        }
        for (int i = 0; i < stage_clear.Length; i++)
        {
            stage_clear[i] = true;
            stage_score[i] = -9999;
            stage_star[i] = 0;
            //이미 깬 구간은 저장된 변수를 불러와서 true로 바꿔주는 작업 필요
        }
    }

    public void InitGame() //게임 들어가기 전 세팅
    {
        doingSetup = true;
        Invoke("Loading", levelStartDelay);

        dogs.Clear();
        portals.Clear();
        stairs.Clear();
    }

    private void Loading() //로딩중...
    {
        doingSetup = false;
    }

    public void GameOver() //게임오버 함수
    {
        GameObject ply = GameObject.Find("Player");
        PlayerController pc = ply.GetComponent<PlayerController>();
        GameObject MC = GameObject.Find("Main Camera");
        pc.Die();
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true); //게임오버 UI 보이기
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>().sprite = pc.dog_sr.sprite; //강아지 스프라이트 가져오기
    }

    public void StageClear() //스테이지 클리어 함수
    {
        GameObject MC = GameObject.Find("Main Camera");
        stage_clear[level - 1] = true; //스테이지 클리어 저장
        GameObject ply = GameObject.Find("Player");
        PlayerController pc = ply.GetComponent<PlayerController>();
        pc.ClearSP();
        pc.Scoring();
        MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //클리어 UI 보이기
        if (level == 5 || level == 10 || level == 15) // 각 월드 마지막 스테이지일 시 --> 월드에 스테이지 늘어날 때마다 늘려줘야 함
        {
            world_clear[world_level] = true;
            MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(5).gameObject.SetActive(false); //다음 미션 false
            MC.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(6).gameObject.SetActive(true); //월드맵 true
        }
    }

    // Update is called once per frame
    void Update() //턴 바꾸는 작업
    {
        if (playerTurn || doingSetup)
        {
            return;
        }
        if (dogTurn)
        {
            AttackDogs();
            return;
        }
    }

    public void AddDogToList(Dog script) //리스트에 추가
    {
        dogs.Add(script);
    }

    public void AddPortalToList(Portal script) //리스트에 추가
    {
        portals.Add(script);
    }

    public void AddStairToList(Stair script) //리스트에 추가
    {
        stairs.Add(script);
    }

    void AttackDogs() //강아지가 공격하는 함수
    {
        dogTurn = false;
        playerTurn = true;
    }
}
