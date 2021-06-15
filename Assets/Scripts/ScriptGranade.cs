using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGranade : MonoBehaviour
{
    public float speed = 0;



    // É chamado apenas uma vez quando a bala é criada
    void Start()
    {
        // Ajusta a velocidade Y para fazer a bala se mover para cima
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

    }
    // Quando a bala ficar invisível será destruída
    void OnBecameInvisible()
    {
        // Destroi a bala quando já está fora da tela
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Debug.Log("Entrou");
            Vector2 explosionPos = transform.position;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos,100f);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null && collision.collider.tag != "Player ")
                {
                    Debug.Log(hit);
                    if (collision.collider.tag != "bullet" && collision.collider.tag != "Player ")
                    {
                        rb.AddExplosionForce(350, rb.transform.position, .01f,10);
                    }
                    
                }
            }
            Destroy(this.gameObject);
        }
    }
}
public static class Rigidbody2DExt
{

    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }
}
