using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DummyMove : MonoBehaviour
{
    private bool isTileOn = false;

    protected Vector3 offset; //클릭 시 위치 유지를 위해 필요한 거리

    protected Vector3 originPosition;
    protected Vector3 objPosition;

    private Vector3Int cellPosition;

    private Tilemap tilem;
    private BoxCollider2D bx;

    private void Start()
    {
        bx = GetComponent<BoxCollider2D>();
    }

    

    protected virtual void OnMouseDown() //누를 때 클릭된 프리팹을 앞으로 옮김
    {
        bx.enabled = false;
        bx.enabled = true;
        originPosition = transform.position;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        offset = originPosition - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (!isTileOn)
        {
            transform.position = objPosition + offset;
        }
        else{
            transform.position = tilem.CellToWorld(cellPosition);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Room")
        {
            isTileOn = true;
            tilem = other.gameObject.GetComponent<Tilemap>();
            cellPosition = tilem.WorldToCell(objPosition + offset);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Room")
        {
            isTileOn = false;
        }
    }
}
