// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TouchCameraInGame : MonoBehaviour
{
    public bool nonbutton = false;

    public CinemachineVirtualCamera vcam;
    public CinemachineFramingTransposer vcam_com;
    public GameObject pl_cam; //플레이어 카메라 게임오브젝트

    public float orthoZoomSpeed; // The rate of change of the orthographic size in orthographic mode.

    Camera cam; //카메라
    Vector2 oldTouchPosition; //기존 포지션

    private void Awake()
    {
        cam = transform.GetComponent<Camera>();
        vcam_com = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        oldTouchPosition = new Vector2(-9999f, -9999f);
    }

    void FixedUpdate()
    {
        if (Input.touchCount == 0)
        { //터치를 끝냈을 때
            oldTouchPosition = new Vector2(-9999f, -9999f);
        }
        else if (Input.touchCount == 1 && !nonbutton)
        {
            if (!GameManager.instance.is_menu)
            { //메뉴가 켜져있지 않으면
                if (oldTouchPosition == new Vector2(-9999f, -9999f))
                {
                    oldTouchPosition = Input.GetTouch(0).position; //처음에는 터치한 곳을 받아온다
                    pl_cam.transform.position = transform.position;
                }
                else
                {
                    Vector2 newTouchPosition = Input.GetTouch(0).position;
                    if ((newTouchPosition - oldTouchPosition).sqrMagnitude < 15000f)
                    { //처음 터치한 곳과 터치를 움직인 곳의 거리가 얼마 차이 안 날때
                        vcam_com.m_DeadZoneWidth = 0f;
                        vcam_com.m_DeadZoneHeight = 0f;
                        pl_cam.transform.localPosition += transform.TransformDirection(((oldTouchPosition.x - newTouchPosition.x) * cam.orthographicSize / cam.pixelHeight * 2f), ((oldTouchPosition.y - newTouchPosition.y) * cam.orthographicSize / cam.pixelHeight * 2f), 0f);
                    }
                    oldTouchPosition = newTouchPosition; //터치 정보를 조금씩 옮기기
                }
            }
        }
        else if (Input.touchCount == 2 && !nonbutton)
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
                vcam.m_Lens.OrthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                cam.transform.localScale += new Vector3(deltaMagnitudeDiff * orthoZoomSpeed / 5, deltaMagnitudeDiff * orthoZoomSpeed / 5, 0f);

                // Make sure the orthographic size never drops below zero.
                vcam.m_Lens.OrthographicSize = Mathf.Max(vcam.m_Lens.OrthographicSize, 2.5f);
                cam.transform.localScale = new Vector3(Mathf.Max(cam.transform.localScale.x, 0.5f), Mathf.Max(cam.transform.localScale.y, 0.5f), 1f);
                vcam.m_Lens.OrthographicSize = Mathf.Min(vcam.m_Lens.OrthographicSize, 4.5f);
                cam.transform.localScale = new Vector3(Mathf.Min(cam.transform.localScale.x, 0.9f), Mathf.Min(cam.transform.localScale.y, 0.9f), 1f);
            }
        }
    }
}
