using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clonadorScript : MonoBehaviour
{
    public GameObject[] comentario =new GameObject[0]; // Armazenará o prefab Asteroide

    // Variável para conhecer quão rápido nós devemos criar novos Asteroides
    public float spawnTime;
    private int randomizer;

    void Start()
    {
        // Chamar a função 'addEnemy' a cada 'spawnTime' segundos
        InvokeRepeating("addEnemy", spawnTime, spawnTime);
    }
    void addEnemy()
    {
        randomizer = Random.Range(0, comentario.Length);

        Renderer renderer = GetComponent<Renderer>();
        var x1 = transform.position.x - renderer.bounds.size.x / 2;
        var x2 = transform.position.x + renderer.bounds.size.x / 2;

        var spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);
        Debug.Log(randomizer);

        try
        {
            Instantiate(comentario[randomizer], spawnPoint, Quaternion.identity);

        } catch (System.Exception e) {
            Debug.Log(e.ToString());
        }
        
        
    }
}
