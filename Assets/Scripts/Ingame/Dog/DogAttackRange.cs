using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttackRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Dog>().Attack();
        }
    }
}
