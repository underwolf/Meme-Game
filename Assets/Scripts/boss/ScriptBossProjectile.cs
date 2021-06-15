using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossProjectile : MonoBehaviour
{
    public float speed = 6f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(0, speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);

            Debug.Log("you got poggered");
        }

    }
}
