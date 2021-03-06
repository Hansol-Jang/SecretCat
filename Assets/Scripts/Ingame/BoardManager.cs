﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoardManager : MonoBehaviour {

    //프리팹 모음 장소(플레이어, 적, 타일 등)
    public GameObject cat;
    public GameObject[] dog = new GameObject[24];
    public GameObject[] portal = new GameObject[15];
    public GameObject cleartile;
    public GameObject stair1;
    public GameObject stair2;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;
    public TouchCameraInGame TCIG;
    public GameObject cam;
    public CinemachineVirtualCamera vcam;

    public int is_floor; //현재 플로어
    public bool is_clear = false; //그 스테이지를 클리어했니?

    private GameObject[] floor = new GameObject[3]; //플로어 게임오브젝트를 저장할 배열
    private GameObject floor_CL;

    // Use this for initialization
    void Start () {
        GameManager.instance.InitGame(); // 게임 들어가기 전 세팅 과정
        SetScene(GameManager.instance.level); //보드를 만드는 과정
    }

    void BoardSetup(int level) { //레벨에 맞는 보드를 가져온다.
        GameObject prefab = Resources.Load("Prefabs/Board/Board"+level) as GameObject;
        GameObject BM = GameObject.Find("BoardManager");
        GameObject board = Instantiate(prefab, BM.transform) as GameObject;
        board.name = "Board";
        BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
        btl.Setting();
        for (int i = 0; i < btl.floorNumber; i++) //층을 배열에 넣음
        {
            floor[i] = board.transform.GetChild(i).gameObject;
        }
    }

    void UnitSetup() { //유닛을 배치한다.
        GameObject board = GameObject.Find("Board");
        BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
        GameObject player_instance = Instantiate(cat) as GameObject; //플레이어 생성
        TCIG.pl_cam = player_instance.transform.GetChild(5).gameObject;
        player_instance.name = "Player";
        player_instance.transform.position += new Vector3(btl.player_loc[0], btl.player_loc[1], 0f); //시작 위치로 플레이어를 옮긴다.
        player_instance.transform.GetChild(0).transform.localPosition += new Vector3(0f, 0f, btl.player_loc[1]-0.5f);
        vcam.Follow = player_instance.transform.GetChild(5).transform;
        if (btl.floorNumber >= 3) //3층 이상 있는 경우
        {
            GameObject dogboard3 = floor[2].transform.GetChild(1).gameObject; //floor3를 불러와

            for (int i = 0; i < btl.dog_num.GetLength(0); i++)
            { //dog_num의 크기만큼 개 유닛을 만든다.
                if (btl.dog_num[i,4] == 3)
                {
                    GameObject instance = Instantiate(dog[btl.dog_num[i,0]*4 + btl.dog_num[i,1]], dogboard3.transform) as GameObject;
                    instance.transform.position += new Vector3(btl.dog_num[i,2], btl.dog_num[i,3], btl.dog_num[i,3]);
                }
            }
        }
        if (btl.floorNumber >= 2) //2층 이상 있는 경우
        {
            GameObject dogboard2 = floor[1].transform.GetChild(1).gameObject; //floor2를 불러와
            for (int i = 0; i < btl.dog_num.GetLength(0); i++)
            { //dog_num의 크기만큼 개 유닛을 만든다.
                if (btl.dog_num[i, 4] == 2)
                {
                    GameObject instance = Instantiate(dog[btl.dog_num[i, 0] * 4 + btl.dog_num[i, 1]], dogboard2.transform) as GameObject;
                    instance.transform.position += new Vector3(btl.dog_num[i, 2], btl.dog_num[i, 3], btl.dog_num[i, 3]);
                }
            }
        }
        if (btl.floorNumber >= 1) //1층 이상 있는 경우
        {
            GameObject dogboard1 = floor[0].transform.GetChild(1).gameObject; //floor1를 불러와
            for (int i = 0; i < btl.dog_num.GetLength(0); i++)
            { //dog_num의 크기만큼 개 유닛을 만든다.
                if (btl.dog_num[i, 4] == 1)
                {
                    GameObject instance = Instantiate(dog[btl.dog_num[i, 0] * 4 + btl.dog_num[i, 1]], dogboard1.transform) as GameObject;
                    instance.transform.position += new Vector3(btl.dog_num[i, 2], btl.dog_num[i, 3], btl.dog_num[i, 3]);
                }
            }
        }
    }

    void PortalSetup() //포탈을 배치한다.
    {
        GameObject board = GameObject.Find("Board");
        BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
        if (btl.floorNumber >= 3) //3층 이상 있는 경우
        {
            GameObject portalboard3 = floor[2].transform.GetChild(2).gameObject; //floor3를 불러와
            for (int i = 0; i < btl.portal_num.GetLength(0); i++)
            { //portal_num의 크기만큼 포탈을 만든다.
                if (btl.portal_num[i,3] == 3)
                {
                    int j = Mathf.Abs(btl.portal_num[i,0]);
                    GameObject instance = Instantiate(portal[j - 1], portalboard3.transform) as GameObject;
                    instance.transform.position += new Vector3(btl.portal_num[i,1], btl.portal_num[i,2], 990f);
                }
            }
        }
        if (btl.floorNumber >= 2) //2층 이상 있는 경우
        {
            GameObject portalboard2 = floor[1].transform.GetChild(2).gameObject; //floor2를 불러와
            for (int i = 0; i < btl.portal_num.GetLength(0); i++)
            { //portal_num의 크기만큼 포탈을 만든다.
                if (btl.portal_num[i, 3] == 2)
                {
                    int j = Mathf.Abs(btl.portal_num[i, 0]);
                    GameObject instance = Instantiate(portal[j - 1], portalboard2.transform) as GameObject;
                    instance.transform.position += new Vector3(btl.portal_num[i, 1], btl.portal_num[i, 2], 990f);
                }
            }
        }
        if (btl.floorNumber >= 1) //1층 이상 있는 경우
        {
            GameObject portalboard1 = floor[0].transform.GetChild(2).gameObject; //floor1를 불러와
            for (int i = 0; i < btl.portal_num.GetLength(0); i++)
            { //portal_num의 크기만큼 포탈을 만든다.
                if (btl.portal_num[i, 3] == 1)
                {
                    int j = Mathf.Abs(btl.portal_num[i, 0]);
                    GameObject instance = Instantiate(portal[j - 1], portalboard1.transform) as GameObject;
                    instance.transform.position += new Vector3(btl.portal_num[i, 1], btl.portal_num[i, 2], 990f);
                }
            }
        }
    }

    void StairSetup() //계단을 배치한다.
    {
        GameObject board = GameObject.Find("Board");
        BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
        if (btl.floorNumber >= 3) //3층 이상 있는 경우
        {
            GameObject stairboard3 = floor[2].transform.GetChild(3).gameObject; //floor3를 불러와
            for (int i = 0; i < btl.stair_num.GetLength(0); i++)
            { //stair_num의 크기만큼 계단을 만든다.
                if (btl.stair_num[i,4] == 3)
                {
                    if (btl.stair_num[i,1] == 1)
                    {
                        GameObject instance = Instantiate(stair1, stairboard3.transform) as GameObject;
                        instance.transform.position += new Vector3(btl.stair_num[i,2], btl.stair_num[i,3], 990f);
                        for (int j = 0; j < 5; j++)
                        {
                            instance.GetComponent<Stair>().is_loc_stair[j] = btl.stair_num[i,j];
                        }
                    }
                    else if (btl.stair_num[i,1] == -1)
                    {
                        GameObject instance = Instantiate(stair2, stairboard3.transform) as GameObject;
                        instance.transform.position += new Vector3(btl.stair_num[i,2], btl.stair_num[i,3], 990f);
                        for (int j = 0; j < 5; j++)
                        {
                            instance.GetComponent<Stair>().is_loc_stair[j] = btl.stair_num[i,j];
                        }
                    }
                }
            }
        }
        if (btl.floorNumber >= 2) //2층 이상 있는 경우
        {
            GameObject stairboard2 = floor[1].transform.GetChild(3).gameObject; //floor2를 불러와
            for (int i = 0; i < btl.stair_num.GetLength(0); i++)
            { //stair_num의 크기만큼 계단을 만든다.
                if (btl.stair_num[i, 4] == 2)
                {
                    if (btl.stair_num[i, 1] == 1)
                    {
                        GameObject instance = Instantiate(stair1, stairboard2.transform) as GameObject;
                        instance.transform.position += new Vector3(btl.stair_num[i, 2], btl.stair_num[i, 3], 990f);
                        for (int j = 0; j < 5; j++)
                        {
                            instance.GetComponent<Stair>().is_loc_stair[j] = btl.stair_num[i, j];
                        }
                    }
                    else if (btl.stair_num[i, 1] == -1)
                    {
                        GameObject instance = Instantiate(stair2, stairboard2.transform) as GameObject;
                        instance.transform.position += new Vector3(btl.stair_num[i, 2], btl.stair_num[i, 3], 990f);
                        for (int j = 0; j < 5; j++)
                        {
                            instance.GetComponent<Stair>().is_loc_stair[j] = btl.stair_num[i, j];
                        }
                    }
                }
            }
        }
        if (btl.floorNumber >= 1) //1층 이상 있는 경우
        {
            GameObject stairboard1 = floor[0].transform.GetChild(3).gameObject; //floor1를 불러와
            for (int i = 0; i < btl.stair_num.GetLength(0); i++)
            { //stair_num의 크기만큼 계단을 만든다.
                if (btl.stair_num[i, 4] == 1)
                {
                    if (btl.stair_num[i, 1] == 1)
                    {
                        GameObject instance = Instantiate(stair1, stairboard1.transform) as GameObject;
                        instance.transform.position += new Vector3(btl.stair_num[i, 2], btl.stair_num[i, 3], 990f);
                        for (int j = 0; j < 5; j++)
                        {
                            instance.GetComponent<Stair>().is_loc_stair[j] = btl.stair_num[i, j];
                        }
                    }
                    else if (btl.stair_num[i, 1] == -1)
                    {
                        GameObject instance = Instantiate(stair2, stairboard1.transform) as GameObject;
                        instance.transform.position += new Vector3(btl.stair_num[i, 2], btl.stair_num[i, 3], 990f);
                        for (int j = 0; j < 5; j++)
                        {
                            instance.GetComponent<Stair>().is_loc_stair[j] = btl.stair_num[i, j];
                        }
                    }
                }
            }
        }
    }

    void ClearTileSetup() //클리어 타일 배치한다.
    {
        GameObject board = GameObject.Find("Board");
        BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
        if (btl.cleartile_loc[2] == 1)
        {
            floor_CL = floor[0];
        } else if (btl.cleartile_loc[2] == 2)
        {
            floor_CL = floor[1];
        } else if (btl.cleartile_loc[2] == 3)
        {
            floor_CL = floor[2];
        }
        GameObject instance = Instantiate(cleartile, floor_CL.transform) as GameObject;
        instance.transform.position += new Vector3(btl.cleartile_loc[0], btl.cleartile_loc[1], 990f);
    }

    void Tutorial(int level) //튜토리얼 만들기
    {
        GameObject prefab = Resources.Load("Prefabs/Tutorial/Tutorial" + level) as GameObject;
        GameObject instance = Instantiate(prefab, cam.transform) as GameObject;
        instance.name = "Tutorial";
        GameManager.instance.is_menu = true;
        if (level == 1) {
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Tutorial1>().tut1 = true;
        }
    }

    void FloorDisplay() //플로어 UI를 보이기
    {
        GameObject board = GameObject.Find("Board");
        BoardTileLoc btl = board.GetComponent<BoardTileLoc>();
        if (btl.floorNumber >= 3)
        {
            floor3.SetActive(true);
        }
        if (btl.floorNumber >= 2)
        {
            floor2.SetActive(true);
        }
        if (btl.floorNumber >= 1)
        {
            floor1.SetActive(true);
        }
        switch (btl.player_loc[2])
        {
            case 1:
                floor1.transform.GetChild(1).gameObject.SetActive(true);
                is_floor = 1;
                break;
            case 2:
                floor2.transform.GetChild(1).gameObject.SetActive(true);
                is_floor = 2;
                break;
            case 3:
                floor3.transform.GetChild(1).gameObject.SetActive(true);
                is_floor = 3;
                break;
            default:
                break;
        }
    }

    IEnumerator CameraZoom(int level)
    {
        GameManager.instance.is_menu = true;
        yield return new WaitForSeconds(1.2f);
        while (vcam.m_Lens.OrthographicSize > 2.5f)
        {
            if (TCIG.start_zoom)
            {
                break;
            }
            vcam.m_Lens.OrthographicSize -= 0.02f;
            cam.transform.localScale -= new Vector3(0.02f / 5, 0.02f / 5, 0f);
            vcam.m_Lens.OrthographicSize = Mathf.Max(vcam.m_Lens.OrthographicSize, 2.5f);
            cam.transform.localScale = new Vector3(Mathf.Max(cam.transform.localScale.x, 0.5f), Mathf.Max(cam.transform.localScale.y, 0.5f), 1f);
            yield return null;
        }
        GameManager.instance.is_menu = false;
        cam.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        if (level == 1 || level == 2 || level == 6 || level == 7 || level == 15 || level == 17) //--> 튜토리얼 늘어날 때마다 추가
        {
            Tutorial(level);
        }
    }

    void BGMPlay(AudioSource audioSource, AudioClip audioClip) //BGM 깔아주는 함수
    {
        SoundManager.instance.SingleSound(audioSource, audioClip);
    }

    public void SetScene(int level) { //보드 만드는 함수
        BoardSetup(level);
        UnitSetup();
        PortalSetup();
        StairSetup();
        ClearTileSetup();
        FloorDisplay();
        BGMPlay(SoundManager.instance.bgm_source, SoundManager.instance.bgm0);
        StartCoroutine("CameraZoom",level);
    }
}
