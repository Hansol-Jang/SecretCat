using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial12 : MonoBehaviour {

    private GameObject BM;
    private BoardManager BM_SC;

	// Use this for initialization
	void Start () {
        BM = GameObject.Find("BoardManager");
        BM_SC = BM.GetComponent<BoardManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (BM_SC.is_floor == 2)
        {
            GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
	}
}
