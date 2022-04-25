using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour,IAtacante
{
    public Vector3 destino;
    public float speed = 20;
    private GameObject enemigo;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemigo") 
        {
            enemigo = collision.gameObject;
            Danar();
            Destroy(this.gameObject);
        }
    }
    public void Danar(int dano = 10)
    {
        enemigo.GetComponent<EnemigoBase>().RecibirDano(dano);
    }

    // Start is called before the first frame update
    void Start()
    {
        destino.y += 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, destino, step);
        if (Vector3.Distance(transform.position, destino) < 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
}
