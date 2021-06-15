using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScriptBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;

    public float speed = 2f;
    private float attackTimer, idleTimer;
    private bool moveRight, phaseChange, attackCooldown;
    public int healthPoints = 10;
    public AudioClip hurt;

    public Animator phaseChangeAnimator, animator;

    public ScriptBossProjectileCloner projectileClonerL, projectileClonerR;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        moveRight = true;
        attackCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {

        attackTimer -= Time.deltaTime;

        //isso eh uma gambiarra me dsclp eu n faço ideia de como fazer um jogo meu deus do ceu
        if(attackTimer < 2 && attackCooldown == false)
        {
            animator.SetTrigger("Attack");
        }

        if (attackTimer < 0 && attackCooldown == false)
        {
            AttackPlayer();
        }
        else
        {
            if(idleTimer > 0 && attackCooldown == true)
            {
                idleTimer -= Time.deltaTime;
            }
            else if(idleTimer < 0 && attackCooldown == true)
            {
                attackCooldown = false;
                attackTimer = StartAttackTimer();
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation| RigidbodyConstraints2D.FreezePositionY;
            }
        }

        if(attackCooldown == false)
        {
            MoveDirection();
        }


    }

    private void MoveDirection()
    {

        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    private float StartAttackTimer()
    {
        return (Random.Range(5f ,8f));
    }

    private void AttackPlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        projectileClonerL.ShootProjectile();
        projectileClonerR.ShootProjectile();
        idleTimer = 2f;
        attackCooldown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("Boss Hitado");
            Destroy(collision.gameObject);
            damage();
            Debug.Log(healthPoints);
        }
        if (collision.gameObject.tag == "flipGate")
        {
            moveRight = !moveRight;
        }

        if (collision.gameObject.tag == "ceiling")
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            damage();
        }
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void damage()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        if (healthPoints > 0)
        {
            healthPoints--;
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }

        Debug.Log(healthPoints);
    }


}
