using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2_2 : MonoBehaviour {

    private GameObject board;

    private void Start()
    {
        board = GameObject.Find("Board");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            board.transform.GetChild(4).gameObject.SetActive(false);
        }
    }
}
