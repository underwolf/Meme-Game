using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comentarioScript : MonoBehaviour
{
    // Contem a velocidade do asteroide
    public float speed = -0.5f;
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.transform.Rotate(new Vector3(0, 0, Random.Range(0, 180)));

    }
    // Chamada quando o asteroide é criado
    void Start()
    { 
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed*Time.deltaTime);
    }
    


}
