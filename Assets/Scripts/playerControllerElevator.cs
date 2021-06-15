using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerElevator : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed,verticalSpeed;
    public int vidas;
    public AudioClip hurt;
    public GameObject gameManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed *Time.deltaTime;
        transform.Translate(horizontal, verticalSpeed*Time.deltaTime, 0);
    }
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "Inimigo")
        {
            vidas = vidas - 1; // Cada colisao perde uma vida
            Destroy(outro.gameObject); // Destroi o Asteroid que colidiu com a Nave
            Debug.Log("Hit");
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            if (vidas > 0)
            {
                Debug.Log(vidas);
                GameObject.Find("LifeManager").gameObject.GetComponent<lifeManager>().destroyHeart(vidas);
            }
            if (vidas == 0)
            {
                gameManager.GetComponent<levelManager>().Respawn();
                Destroy(this.gameObject);
            }
        }

        if(outro.tag == "Stopper")
        {
            Debug.Log("Parou");
            Destroy(GameObject.Find("Clonador").gameObject);
        }

        if(outro.tag == "Finish")
        {
            Debug.Log("dasjidas");
        }
    }


}
