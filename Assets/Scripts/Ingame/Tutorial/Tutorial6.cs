using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial6 : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject tut = GameObject.Find("Tutorial");
            Tutorial_Process tut_pr = tut.GetComponent<Tutorial_Process>();
            tut.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
