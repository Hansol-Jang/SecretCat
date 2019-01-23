using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial7_1 : MonoBehaviour
{
    private GameObject board;

    private void Start()
    {
        board = GameObject.Find("Board");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject tut = GameObject.Find("Tutorial");
            Tutorial_Process tut_pr = tut.GetComponent<Tutorial_Process>();
            GameObject pl = GameObject.Find("Player");
            PlayerController pl_con = pl.GetComponent<PlayerController>();
            if (tut_pr.tutorial_process != 9 && tut_pr.tutorial_process != 8 && tut_pr.tutorial_process != 10)
            {
                tut_pr.tutorial_process = 7;
                board.transform.GetChild(2).gameObject.SetActive(false);
                if (pl_con.bone_num != 0)
                {
                    GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject tut = GameObject.Find("Tutorial");
            Tutorial_Process tut_pr = tut.GetComponent<Tutorial_Process>();
            if (tut_pr.tutorial_process != 9 && tut_pr.tutorial_process != 8 && tut_pr.tutorial_process != 10)
            {
                tut_pr.tutorial_process = 6;
                board.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
