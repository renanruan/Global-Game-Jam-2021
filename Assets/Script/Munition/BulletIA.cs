using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIA : MonoBehaviour
{
    public enum Size { Small, Medium, Big};

    [Header("Settings")]
    public float Velocity;
    public float TimeSpawn;
    public Vector3 Direction = Vector3.zero;
    public float RNGAngle;
    public Size Tamanho;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  INITIAL CONFIGURATION */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private void Start()
    {
        GetComponent<Animator>().SetInteger("Size", (int)Tamanho - 1);
        GetComponent<Animator>().SetFloat("Random", Random.Range(0, 1));
    }

    public void ConfigureDirection(Vector3 Direction)
    {
        RNGAngle = Random.Range(-RNGAngle, RNGAngle);
        this.Direction = Quaternion.Euler(0, 0, RNGAngle) * Direction;
    }

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    /*  MOVIMENT  */
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    void Update()
    {
        if(Direction != Vector3.zero)
        {
            transform.position += Velocity * Direction * Time.deltaTime;
        }

        TimeSpawn -= Time.deltaTime;
        if(TimeSpawn <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
