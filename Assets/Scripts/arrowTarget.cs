using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowTarget : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "arrow")
        {
            Destroy(outro.gameObject);
        }
    }
}
