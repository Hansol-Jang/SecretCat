using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    // Use this for initialization
    void Awake () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Screen.SetResolution(Screen.width, Screen.width * 9/16, true);
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
