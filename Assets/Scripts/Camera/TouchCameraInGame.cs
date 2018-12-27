// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCameraInGame : MonoBehaviour
{

    public GameObject Collider_Left; //가장 왼쪽
    public GameObject Collider_Right; //가장 오른쪽
    public GameObject Collider_Up; //가장 위쪽
    public GameObject Collider_Down; //가장 아래쪽

    public float orthoZoomSpeed = 0.0000001f; // The rate of change of the orthographic size in orthographic mode.

    Camera cam; //카메라
    Vector2 oldTouchPosition; //기존 포지션

    private void Awake()
    {
        cam = transform.GetComponent<Camera>();
        oldTouchPosition = new Vector2(-9999f, -9999f);
    }

    void FixedUpdate()
    {
        if (Input.touchCount == 0)
        { //터치를 끝냈을 때
            oldTouchPosition = new Vector2(-9999f, -9999f);
        }
        else if (Input.touchCount == 1)
        {
            if (!GameManager.instance.is_menu)
            { //메뉴가 켜져있지 않으면
                if (oldTouchPosition == new Vector2(-9999f, -9999f))
                {
                    oldTouchPosition = Input.GetTouch(0).position; //처음에는 터치한 곳을 받아온다
                }
                else
                {
                    Vector2 newTouchPosition = Input.GetTouch(0).position;
                    if ((newTouchPosition - oldTouchPosition).sqrMagnitude < 15000f)
                    { //처음 터치한 곳과 터치를 움직인 곳의 거리가 얼마 차이 안 날때
                        if (oldTouchPosition.x >= newTouchPosition.x && !Collider_Right.GetComponent<StageColliderRight>().right) //오른쪽으로 스와이프했을 경우 + 가장 오른쪽이 아닌 경우
                        {
                            transform.position += transform.TransformDirection(((oldTouchPosition.x - newTouchPosition.x) * cam.orthographicSize / cam.pixelHeight * 2f), 0f, 0f); //카메라 달린 오브젝트가 x축으로 움직인다.
                        }
                        if ((oldTouchPosition.x < newTouchPosition.x) && !Collider_Left.GetComponent<StageColliderLeft>().left) //왼쪽으로 스와이프했을 경우 + 가장 왼쪽이 아닌 경우
                        {
                            transform.position += transform.TransformDirection(((oldTouchPosition.x - newTouchPosition.x) * cam.orthographicSize / cam.pixelHeight * 2f), 0f, 0f); //카메라 달린 오브젝트가 x축으로 움직인다.
                        }
                        if (oldTouchPosition.y >= newTouchPosition.y && !Collider_Up.GetComponent<StageColliderTop>().top) //위쪽으로 스와이프했을 경우 + 가장 위쪽이 아닌 경우
                        {
                            transform.position += transform.TransformDirection(0f, ((oldTouchPosition.y - newTouchPosition.y) * cam.orthographicSize / cam.pixelHeight * 2f), 0f); //카메라 달린 오브젝트가 y축으로 움직인다.
                        }
                        if (oldTouchPosition.y < newTouchPosition.y && !Collider_Down.GetComponent<StageColliderBottom>().bot) //아래쪽으로 스와이프했을 경우 + 가장 아래쪽이 아닌 경우
                        {
                            transform.position += transform.TransformDirection(0f, ((oldTouchPosition.y - newTouchPosition.y) * cam.orthographicSize / cam.pixelHeight * 2f), 0f); //카메라 달린 오브젝트가 y축으로 움직인다.
                        }
                    }
                    oldTouchPosition = newTouchPosition; //터치 정보를 조금씩 옮기기
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            if (!GameManager.instance.is_menu)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // ... change the orthographic size based on the change in distance between the touches.
                cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                cam.transform.localScale += new Vector3(deltaMagnitudeDiff * orthoZoomSpeed / 5, deltaMagnitudeDiff * orthoZoomSpeed / 5, 1f);

                // Make sure the orthographic size never drops below zero.
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, 2.5f);
                cam.transform.localScale = new Vector3(Mathf.Max(cam.transform.localScale.x, 0.5f), Mathf.Max(cam.transform.localScale.y, 0.5f), 1f);
                cam.orthographicSize = Mathf.Min(cam.orthographicSize, 5.0f);
                cam.transform.localScale = new Vector3(Mathf.Min(cam.transform.localScale.x, 1f), Mathf.Min(cam.transform.localScale.y, 1f), 1f);
            }
        }
    }
}
