using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog1X_Rotation : MonoBehaviour
{
    public int origin_loc; //초기 위치
    public Sprite[] sp = new Sprite[4];

    private int loc; //초기 위치 + 플레이어 이동 횟수
    private PlayerController pc;
    private Dog1X_Ray ray;
    private SpriteRenderer dog_sr;
    private Animator dog_ani;

    private void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        ray = GetComponent<Dog1X_Ray>();
        dog_sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        dog_ani = transform.GetChild(0).GetComponent<Animator>();
        dog_ani.SetInteger("rotate", origin_loc);
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.moving)
        {
            loc = origin_loc + pc.mv_num;
            switch (loc % 4)
            {
                case 0:
                    if (dog_sr.sprite != sp[loc % 4])
                    {
                        dog_ani.SetInteger("rotate", loc % 4);
                        transform.GetChild(0).localPosition = new Vector3(0.03f, 0.27f, 0f);
                        transform.GetChild(1).transform.localPosition = new Vector3(-1f, 0f, 0f);
                        transform.GetChild(1).transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,0f));
                        ray.end = ray.start + new Vector2(-9f, 0f);
                    }
                    break;
                case 1:
                    if (dog_sr.sprite != sp[loc % 4])
                    {
                        dog_ani.SetInteger("rotate", loc % 4);
                        transform.GetChild(0).localPosition = new Vector3(0.058f, 0.25f, 0f);
                        transform.GetChild(1).transform.localPosition = new Vector3(0f, 1f, 0f);
                        transform.GetChild(1).transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
                        ray.end = ray.start + new Vector2(0f, 9f);

                    }
                    break;
                case 2:
                    if (dog_sr.sprite != sp[loc % 4])
                    {
                        dog_ani.SetInteger("rotate", loc % 4);
                        transform.GetChild(0).localPosition = new Vector3(0.02f, 0.27f, 0f);
                        transform.GetChild(1).transform.localPosition = new Vector3(1f, 0f, 0f);
                        transform.GetChild(1).transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -180f));
                        ray.end = ray.start + new Vector2(9f, 0f);
                    }
                    break;
                case 3:
                    if (dog_sr.sprite != sp[loc % 4])
                    {
                        dog_ani.SetInteger("rotate", loc % 4);
                        transform.GetChild(0).localPosition = new Vector3(-0.074f, 0.269f, 0f);
                        transform.GetChild(1).transform.localPosition = new Vector3(0f, -1f, 0f);
                        transform.GetChild(1).transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -270f));
                        ray.end = ray.start + new Vector2(0f, -9f);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}