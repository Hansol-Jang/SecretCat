using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject CM; //카메라 무브 게임 오브젝트를 받음

    public float[] cm_x;
    public float[] cm_y;

    // Use this for initialization
    void Start () {
        CM.transform.position = new Vector3(cm_x[GameManager.instance.world_level],cm_y[GameManager.instance.world_level],0f);
	}
}
