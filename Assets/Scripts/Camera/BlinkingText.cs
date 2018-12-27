using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlinkingText : MonoBehaviour {

    Text blinkingText;

    // Use this for initialization
    void Start() {

        blinkingText = GetComponent<Text>();
        StartCoroutine(BlinkText());

    }

    // Update is called once per frame
    public IEnumerator BlinkText() {
        while (true)
        {
            blinkingText.text = "";
            yield return new WaitForSeconds(.5f);
            blinkingText.text = "한 번 눌러보라냥!";
            yield return new WaitForSeconds(.5f);
        }
    }
}