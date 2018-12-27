// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCamera_X : MonoBehaviour
{
    Camera cam; //카메라
    Vector2 oldTouchPosition; //기존 포지션

    public GameObject arrow_Left; //왼쪽 화살표
    public GameObject arrow_Right; //오른쪽 화살표
    [HideInInspector] public GameObject Collider_Left; //가장 왼쪽
    [HideInInspector] public GameObject Collider_Right; //가장 오른쪽

    private void Awake()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
        oldTouchPosition = new Vector2(-9999f, -9999f);
    }

    void FixedUpdate()
    {
        if (!Collider_Left.GetComponent<StageColliderLeft>().left) //가장 왼쪽이 아니라면
        {
            arrow_Left.SetActive(true);
        }
        else //가장 왼쪽이라면
        {
            arrow_Left.SetActive(false);
        }
        if (!Collider_Right.GetComponent<StageColliderRight>().right) //가장 오른쪽이 아니라면
        {
            arrow_Right.SetActive(true);
        }
        else //가장 오른쪽이라면
        {
            arrow_Right.SetActive(false);
        }
        
            if (Input.touchCount == 0) //터치를 끝냈을 때
            {
                oldTouchPosition = new Vector2(-9999f, -9999f);
            }
            else if (Input.touchCount == 1)
            {
                if (!GameManager.instance.is_menu){ //메뉴가 안 켜져있을 때
                    if (oldTouchPosition == new Vector2(-9999f, -9999f))
                    {
                        oldTouchPosition = Input.GetTouch(0).position; //처음에는 터치한 곳을 받아온다
                    }
                    else
                    {
                        Vector2 newTouchPosition = Input.GetTouch(0).position; //터치를 움직인 곳을 받아온다.
                        if ((newTouchPosition - oldTouchPosition).sqrMagnitude < 15000f) //처음 터치한 곳과 터치를 움직인 곳의 거리가 얼마 차이 안 날때
                        {
                            if (oldTouchPosition.x >= newTouchPosition.x && !Collider_Right.GetComponent<StageColliderRight>().right) //오른쪽으로 스와이프했을 경우 + 가장 오른쪽이 아닌 경우
                            {
                                transform.position += transform.TransformDirection(((oldTouchPosition.x - newTouchPosition.x) * cam.orthographicSize / cam.pixelHeight * 2f), 0f, 0f); //카메라 달린 오브젝트가 움직인다.
                            }
                            else if ((oldTouchPosition.x < newTouchPosition.x) && !Collider_Left.GetComponent<StageColliderLeft>().left) //왼쪽으로 스와이프했을 경우 + 가장 왼쪽이 아닌 경우
                            {
                                transform.position += transform.TransformDirection(((oldTouchPosition.x - newTouchPosition.x) * cam.orthographicSize / cam.pixelHeight * 2f), 0f, 0f); //카메라 달린 오브젝트가 움직인다.
                            }
                    }
                        oldTouchPosition = newTouchPosition; //터치 정보를 조금씩 옮기기
                    }
                }
        }
    }
}