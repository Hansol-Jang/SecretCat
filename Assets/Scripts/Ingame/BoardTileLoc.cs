using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DogLoc
{
    //보드의 왼쪽 아래부터 0,0이다
    public int[] dog_loc = new int[5]; //0 : 개의 종류, 1 : 개의 방향, 2 : 개의 X축 위치, 3: 개의 Y축 위치, 4: 개의 층 위치

}

[System.Serializable]
public class PortalLoc
{
    public int[] portal_loc = new int[4]; //0 : 포탈 인덱스, 1 : 포탈 1의 X축 위치, 2 : 포탈 1의 Y축 위치, 3 : 포탈의 층 위치
}

[System.Serializable]
public class StairLoc
{
    public int[] stair_loc = new int[5]; //0 : 계단 인덱스, 1 : 계단 상하 2 : 계단의 X축 위치, 3 : 계단의 Y축 위치, 4 : 계단의 층 위치
}

public class BoardTileLoc : MonoBehaviour {

    public float[] camera_loc = new float[2]; //0 : 카메라 X축 위치, 1 : 카메라 Y축 위치
    public int floorNumber; //층 갯수
    public int three_star_point; //3성 점수 하한
    public int two_star_point; //2성 점수 하한
    public int bone_limit; //주어진 뼈 갯수
    public int[] player_loc = new int[3]; //0 : 고양이의 X축 위치, 1: 고양이의 Y축 위치, 2: 고양이의 층 위치
    public int[] cleartile_loc = new int[3]; //0: X축, 1: Y축, 2: 층 위치
    public DogLoc[] dog_num; //적 숫자
    public PortalLoc[] portal_num; //포탈 숫자
    public StairLoc[] stair_num; //계단 숫자
}