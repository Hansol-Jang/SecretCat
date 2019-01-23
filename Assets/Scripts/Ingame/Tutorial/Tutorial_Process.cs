using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Process : MonoBehaviour {

    public int tutorial_process = 0;

    public void GoProcess()
    {
        switch (GameManager.instance.level)
        {
            case 1:
                switch (tutorial_process)
                {
                    case 1:
                        transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "화살표 버튼을 터치하여 이동할 수 있습니다.\n한 번에 한 칸씩만 이동 가능합니다.";
                        break;
                    case 2:
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "이동하여 목적지까지 도달해보세요!";
                        break;
                    case 3:
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(0).gameObject.SetActive(false);
                        GameObject board = GameObject.Find("Board");
                        board.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                        GameObject finger = GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
                        finger.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        GameManager.instance.is_menu = false;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (tutorial_process)
                {
                    case 1:
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "강아지가 보지 못하는 옆이나 뒤에서 접근하면\n강아지를 공격할 수 있습니다.";
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(true);
                        break;
                    case 2:
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "이번에도 목적지까지 도달해보세요!";
                        break;
                    case 3:
                        transform.GetChild(0).gameObject.SetActive(false);
                        GameObject board = GameObject.Find("Board");
                        board.transform.GetChild(3).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        GameManager.instance.is_menu = false;
                        break;
                    default:
                        break;
                }
                break;
            case 6:
                switch (tutorial_process)
                {
                    case 1:
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "포탈을 타고 목적지까지 이동하세요.";
                        break;
                    case 2:
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        GameObject board = GameObject.Find("Board");
                        board.transform.GetChild(2).gameObject.SetActive(true);
                        GameManager.instance.is_menu = false;
                        break;
                    default:
                        break;
                }
                break;
            case 7:
                switch (tutorial_process)
                {
                    case 1:
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "뼈다귀를 던지려면 던지기 버튼을 터치하고";
                        break;
                    case 2:
                        transform.GetChild(3).gameObject.SetActive(false);
                        transform.GetChild(4).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "화살표 버튼을 터치하면 됩니다.";
                        break;
                    case 3:
                        transform.GetChild(4).gameObject.SetActive(false);
                        transform.GetChild(5).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "뼈다귀가 강아지한테 맞았다면,\n그 강아지는 뼈다귀에 한 눈이 팔려 공격하지 않는 상태가 될 것입니다.";
                        break;
                    case 4:
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "뼈다귀를 이용하여 목적지까지 무사히 도착하세요.";
                        break;
                    case 5:
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(5).gameObject.SetActive(false);
                        GameObject board = GameObject.Find("Board");
                        board.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        GameManager.instance.is_menu = false;
                        break;
                    default:
                        break;
                }
                break;
            case 15:
                switch (tutorial_process)
                {
                    case 1:
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "다른 강아지나 벽을 이용해 그들의 눈에 띄지 마세요!";
                        break;
                    case 2:
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        GameManager.instance.is_menu = false;
                        break;
                    default:
                        break;
                }
                break;
            case 17:
                switch (tutorial_process)
                {
                    case 1:
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "그러나 위에는 뭐가 있을지 몰라요.\n한 번 확인해보고 가시겠어요?";
                        break;
                    case 2:
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        GameManager.instance.is_menu = false;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
