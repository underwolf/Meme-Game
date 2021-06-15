using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemy : MonoBehaviour
{
    private float speed = 2f, IdleTimer, dist;
    private bool moveRight, isIdle, isAggro;

    public Animator animator;

    public GameObject player, enemy;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        moveRight = true;
        isAggro = false;
    }

    // Update is called once per frame
    void Update()
    {

        IdleTimer -= Time.deltaTime;

        AggroCheck();

        if(isAggro == true)
        {
            MoveDirection();
        }
        else
        {
            if (IdleTimer < 0 && isIdle == false)
            {
                IdleCheck();
            }
            else if (IdleTimer < 0 && isIdle == true)
            {
                isIdle = false;
                animator.SetBool("IsIdle", false);
                speed = 2f;
            }

            if (isIdle == true)
            {
                speed = 0f;
            }
            else
            {
                MoveDirection();
            }
        }
    }

    private void MoveDirection()
    {

        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed,0,0);
            transform.localScale = new Vector2(1.5f, 1.5f);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1.5f, 1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "flipGate")
        {
            moveRight = !moveRight;
        }
    }

    private void IdleCheck(){
        if(Random.Range(0,3) == 1)
        {
            isIdle = true;
            animator.SetBool("IsIdle", true);
            IdleTimer = Random.Range(1.0f, 4.0f);
        }
        else
        {
            isIdle = false;
            IdleTimer = Random.Range(1.0f, 4.0f);
        }
    }

    private void AggroCheck()
    {
        dist = Vector3.Distance(player.transform.position, enemy.transform.position);
        
        if(dist < 10.0f)
        {
            isIdle = false;
            animator.SetBool("IsIdle", false);

            animator.SetBool("IsAggro", true);
            isAggro = true;
            speed = 4f;
        }
        else
        {
            animator.SetBool("IsAggro", false);
            isAggro = false;
            speed = 2f;
        }
    }
}
