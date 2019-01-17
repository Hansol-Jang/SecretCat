using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CMLOC
{
    public float[] cm_x;

}

public class StageManager : MonoBehaviour
{
    public int touchStage;
    public TouchCamera_X TCX;
    public GameObject CM;

    public CMLOC[] cm_world;

    // Use this for initialization
    void Start()
    {
        Setup_ST(GameManager.instance.world_level);
        CM.transform.position = new Vector3(cm_world[GameManager.instance.world_level].cm_x[GameManager.instance.level- GameManager.instance.sv_start_number],0f,0f);
    }

    void StageSetup(int w)
    { //월드에 맞는 스테이지를 가져온다.
        GameObject prefab = Resources.Load("Prefabs/World" + w + "_Stage") as GameObject;
        GameObject stg = Instantiate(prefab, transform) as GameObject;
        stg.name = "WorldStage";
    }
    void ColliderSetup() //컬라이더 설정
    {
        TCX.Collider_Left = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        TCX.Collider_Right = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
    }

    void Setup_ST(int wl)
    {
        StageSetup(wl);
        ColliderSetup();
    }

    public void ActiveInfo() //터치된 스테이지 하이라이트 되게하기
    {
        for (int i = 0; i < GameManager.instance.sv_stage_number; i++)
        {
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.GetComponent<StageSelect>().touched = false;
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(touchStage).gameObject.GetComponent<StageSelect>().touched = true;
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(touchStage).gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void NonActiveInfo() //하이라이트 없애는 함수
    {
        for (int i = 0; i < GameManager.instance.sv_stage_number; i++)
        {
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.GetComponent<StageSelect>().touched = false;
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}