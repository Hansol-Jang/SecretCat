using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial6 : MonoBehaviour {

    private GameObject board;

    private void Start()
    {
        board = GameObject.Find("Board");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            board.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
