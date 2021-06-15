using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPlayer : MonoBehaviour
{
    private Vector3 m_Velocity = Vector3.zero;
    public SpriteRenderer spriteRenderer;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    public GameObject bala; // instancia o projetil
    public int speed = 5;
    public int dashSpeed = 5;
    public AudioClip hurt;
    public int vida=5;
    private float proximoDash;
    public float cooldown = 2;
    public float cooldownDash = 1;
    private float proximoTiro = 0;
    private float proximoPulo = 0;
    public float atkRange = .5f;
    public float jumpForce = 3f;
    private BoxCollider2D boxCollider2d;
    private float proximoPuloDuplo = 0;
    private float lastDamagedTime = 0;
    public float invincibilityLength = 0.05f;
    public Sprite[] mascaras;
    private int mascarasIndex = 0;
    public Transform firepoint;
    private bool m_FacingRight = true, isHurting = false;
    private Rigidbody2D m_Rigidbody2D;
    float horizontalMove = 0f;
    [SerializeField] private LayerMask platformLayerMask, enemyLayers;
    private bool canDoubleJump;
    public Animator animator;
    public bool derp;
    public GameObject gameManager,boss;
    private bool bossHit=false;
    private float startTime;
    private void Awake()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        mover();
        atirar();
        changeMask();
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if (Input.GetKeyDown("r"))
        {
            startTime = Time.time;
        }
        if (Input.GetKeyUp("r"))
        {
            if (Time.time - startTime > 0.6f)
            {
                death();
            }
            
        }
    }
    void mover()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        Vector3 targetVelocity = new Vector2(horizontalMove, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (horizontalMove > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        else if (horizontalMove < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        if (IsGrounded() && mascarasIndex == 1)
        {
            canDoubleJump = true;
        }
        if (Input.GetKeyDown("space"))
        {
            if (IsGrounded())
            {
                animator.SetTrigger("jump");
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                animator.SetTrigger("jump");
            }
            else if (canDoubleJump == true && mascarasIndex == 1)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, 2.5f), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
        if (mascarasIndex == 0)
        {
            if (Input.GetKeyDown("f") && Time.time > proximoDash)
            {
                if (m_FacingRight)
                {
                    proximoDash = Time.time + cooldownDash;
                    m_Rigidbody2D.AddForce(new Vector2(1000f, 0));
                }
                else
                {
                    proximoDash = Time.time + cooldownDash;
                    m_Rigidbody2D.AddForce(new Vector2(-1000f, 0));
                }

            }
        }
    }
    void atirar()
    {
        if (Time.time > proximoTiro)
        {
            if ((Input.GetButton("Fire1")) && Time.time > proximoTiro)
            {
                if (mascarasIndex == 1)
                {
                    bossHit = false;
                    animator.SetTrigger("attack");
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firepoint.position, atkRange, enemyLayers);
                    
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log(enemy);
                        if (enemy.tag == "miniBoss")
                        {
                            bossHit = true;
                        }
                        else
                        {
                            Destroy(enemy.gameObject);
                        } 
                    }
                    animator.SetTrigger("attack");
                    if (bossHit)
                    {
                        boss.GetComponent<ScriptBoss>().damage();
                    }
                    proximoTiro = Time.time + cooldown;
                }
                else if (mascarasIndex == 0)
                {
                    animator.SetTrigger("attack");
                    proximoTiro = Time.time + cooldown;
                    Instantiate(bala, firepoint.position, firepoint.rotation);
                    animator.SetTrigger("attack");
                }

            }
        }
    }


    //TROCAR AS MASCARAS DOS BONECO
    void changeMask()
    {
        if (derp)
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("E;");

                if (mascarasIndex == mascaras.Length - 1)
                {

                }
                else if (mascarasIndex > mascaras.Length - 1)
                {
                    mascarasIndex = mascarasIndex - 1;
                    spriteRenderer.sprite = mascaras[mascarasIndex];
                    Debug.Log("Setando mascara 0;");
                    animator.SetInteger("mask",0);
                }
                else if (mascarasIndex < mascaras.Length - 1)
                {
                    mascarasIndex++;
                    spriteRenderer.sprite = mascaras[mascarasIndex];
                    Debug.Log("Setando mascara 1;");
                    animator.SetInteger("mask", 1);

                }
            }
            if (Input.GetKeyDown("q"))
            {
                Debug.Log("q;");
                if (mascarasIndex == 0)
                {
                }
                else if (mascarasIndex == mascaras.Length - 1)
                {
                    mascarasIndex = mascarasIndex - 1;
                    spriteRenderer.sprite = mascaras[mascarasIndex];
                    Debug.Log("Setando mascara 0;");
                    animator.SetInteger("mask", 0);
                }
                else if (mascarasIndex != mascaras.Length - 1)
                {
                    mascarasIndex = mascarasIndex - 1;
                    spriteRenderer.sprite = mascaras[mascarasIndex];
                    Debug.Log("Setando mascara 0 segunda vez no q;");
                    animator.SetInteger("mask", 0);
                }

            }
        }
    }
    public void death()
    {
        speed = 0;
        proximoTiro = 9999;
        gameManager.GetComponent<levelManager>().Respawn();
    }
    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {

        if (outro.gameObject.tag == "Death")
        {
            death();
        }

        if (outro.tag == "Finish")
        {
            derp = true;
            Destroy(outro.gameObject);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlataform"))
        {
            this.transform.parent = collision.transform;
        }

        if (collision.gameObject.tag.Equals("Inimigo") || collision.gameObject.tag.Equals("miniBoss"))
        {
            if (vida <= 0)
            {
                death();
            }
            else
            {
                ApplyDamageIfNotInvincible();
            }
           
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlataform"))
        {
            this.transform.parent = null;
        }

    }

    public void ApplyDamageIfNotInvincible()
    {
        if (lastDamagedTime + invincibilityLength < Time.time && isHurting == false)
        {
            StartCoroutine(Hurt());
            lastDamagedTime = Time.time;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            vida = vida - 1;
            if (vida > 0)
            {
                Debug.Log(vida);
                GameObject.Find("LifeManager").gameObject.GetComponent<lifeManager>().destroyHeart(vida);
            }

            Debug.Log("DANO");
        }
    }
    IEnumerator Hurt()
    {
        isHurting = true;
        m_Rigidbody2D.AddForce(new Vector2(-500f, 150f));
        animator.SetTrigger("jump");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("jump");

        isHurting = false;
    }

    private bool IsGrounded()
    {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);
        return raycastHit.collider != null;
    }
    private void OnDrawGizmosSelected()
    {
        if (firepoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(firepoint.position, atkRange);
    }


}
