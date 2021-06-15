using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMiniboss : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    private float dist, IdleTimer, jumpForce;
    private bool moveRight, attackCooldown,playerHit;
    public int healthPoints = 10;
    public float speed = 2f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;
    public GameObject player, enemy,derp;

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 1.6f;
    public  LayerMask playerLayers; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        moveRight = true;
        attackCooldown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        IdleTimer -= Time.deltaTime;

        if (attackCooldown == true)
        {
            if(IdleTimer < 0)
            {
                attackCooldown = false;
                animator.SetBool("IsIdle", false);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                speed = speed+0.3f;
            }
        }
        else
        {
            PlayerInRange();
            MoveDirection();
        }
    }

    private void MoveDirection()
    {

        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(2, 2);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-2, 2);
        }

        if((Random.Range(0,100) == 1) && IsGrounded())
        {
            jumpForce = 10f;

            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            healthPoints--;
            Destroy(collision.gameObject);
            Debug.Log(healthPoints);
            Debug.Log("not poggers dude");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            if (healthPoints <= 0)
            {
                derp.SetActive(true);
                Destroy(this.gameObject);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flipGate")
        {
            moveRight = !moveRight;
        }

        if(collision.gameObject.tag == "ceiling")
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }

    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.5f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);

        return raycastHit.collider != null;
    }

    private void PlayerInRange()
    {
        dist = Vector3.Distance(player.transform.position, enemy.transform.position);

        if (dist < 1f)
        {
            attackCooldown = true;
            AttackPlayer();
            animator.SetBool("IsIdle", true);

            if(IdleTimer < 0)
            {
                IdleTimer = 2.0f;
            }
        }
        else
        {
            attackCooldown = false;
        }
    }

    private void AttackPlayer()
    {
        speed = 0f;

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitPlayer)
        {
            //player take damage now
            if (enemy.tag == "miniBoss")
            {
                animator.SetTrigger("Attack");
                playerHit = true;
            }
            
        }
        if (playerHit)
        {
            player.GetComponent<ScriptPlayer>().ApplyDamageIfNotInvincible();
        }
        animator.SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
