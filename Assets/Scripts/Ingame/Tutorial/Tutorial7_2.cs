using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial7_2 : MonoBehaviour {

    private GameObject pl;
    private PlayerController pl_con;
    private GameObject board;

    private void Start()
    {
        board = GameObject.Find("Board");
    }

    // Update is called once per frame
    void Update () {
        if (transform.GetComponent<Tutorial_Process>().tutorial_process == 5)
        {
            pl = GameObject.Find("Player");
            pl_con = pl.GetComponent<PlayerController>();
            transform.GetComponent<Tutorial_Process>().tutorial_process = 6;
        } else if (transform.GetComponent<Tutorial_Process>().tutorial_process == 6) {
            if (pl_con.boninging)
            {
                board.transform.GetChild(2).gameObject.SetActive(false);
                transform.GetComponent<Tutorial_Process>().tutorial_process = 10;
                return;
            }
        } else if (transform.GetComponent<Tutorial_Process>().tutorial_process == 7)
        {
            if (pl_con.boning && pl_con.bone_num != 0)
            {
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                transform.GetComponent<Tutorial_Process>().tutorial_process = 8;
            }
        } else if (transform.GetComponent<Tutorial_Process>().tutorial_process == 8)
        {
            if (pl_con.boninging && pl.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite == pl.transform.GetChild(0).gameObject.GetComponent<PlayerSprite>().main9)
            {
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetComponent<Tutorial_Process>().tutorial_process = 9;
                return;
            }
            if (pl_con.boninging && pl.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite == pl.transform.GetChild(0).gameObject.GetComponent<PlayerSprite>().main6)
            {
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetComponent<Tutorial_Process>().tutorial_process = 10;
                return;
            }
            if (!pl_con.boning)
            {
                board.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetComponent<Tutorial_Process>().tutorial_process = 7;
                return;
            }
        }
	}
}
