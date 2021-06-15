using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBala : MonoBehaviour
{
    public float speed = 0;
    private ScriptPlayer player;



    // É chamado apenas uma vez quando a bala é criada
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<ScriptPlayer>();//Acessa o script do player para retirar vida;
        // Ajusta a velocidade Y para fazer a bala se mover para cima
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*speed;

    }
    // Quando a bala ficar invisível será destruída
    void OnBecameInvisible()
    {
        // Destroi a bala quando já está fora da tela
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Inimigo")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);

        }
    }
}
