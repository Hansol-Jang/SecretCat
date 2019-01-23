using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour {

    [HideInInspector] public bool tut1 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (tut1 == true)
            {
                GameObject tut = GameObject.Find("Tutorial");
                Tutorial_Process tut_pr = tut.GetComponent<Tutorial_Process>();
                if (tut_pr.tutorial_process == 3)
                {
                    GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
