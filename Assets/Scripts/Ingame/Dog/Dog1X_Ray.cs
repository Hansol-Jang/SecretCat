using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog1X_Ray : MonoBehaviour
{

    public int xDir;
    public int yDir;
    public LayerMask blokingLayer1;
    public LayerMask blokingLayer2;

    private BoxCollider2D boxCollider;
    private Vector2 start;
    private Vector2 end;
    private Vector2 end2;
    private RaycastHit2D hit;
    private RaycastHit2D hit2;

    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        start = transform.position;
        end = start + new Vector2(xDir, yDir);
    }

    // Update is called once per frame
    void Update()
    {
        if (Move(out hit)) {
            if (hit.transform.gameObject.tag == "Player")
            {
                GameObject pc = hit.transform.gameObject;
                end2 = start + new Vector2(pc.transform.position.x - transform.position.x, pc.transform.position.y - transform.position.y);
                if (Move2(out hit2))
                {
                    transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    protected bool Move(out RaycastHit2D hit) //움직임 함수
    {
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blokingLayer1);
        boxCollider.enabled = true;
        if (hit)
        {
                return true;
        }

        else return false;
    }
    protected bool Move2(out RaycastHit2D hit)
    {
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end2, blokingLayer2);
        boxCollider.enabled = true;
        if (!hit)
        {
            return true;
        }

        else return false;
    }
}