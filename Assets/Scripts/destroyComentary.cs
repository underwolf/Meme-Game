using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyComentary : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "Inimigo")
        {
            Destroy(outro.gameObject); 
        }
    }
}
