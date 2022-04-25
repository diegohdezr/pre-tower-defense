using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBase : MonoBehaviour
{

    public GameObject enemigo;
    public GameObject prefabBala;
    public List<GameObject> puntasCanon;

    // Update is called once per frame
    void Update()
    {
        if (enemigo != null) 
        {
            Apuntar();
        }
        
    }

    public void Apuntar() 
    {
        transform.LookAt(enemigo.transform);
    }

    public virtual void Disparar() 
    {
        foreach (GameObject puntaCanon in puntasCanon) 
        {
            var tempBala = Instantiate<GameObject>(prefabBala, puntaCanon.transform.position, Quaternion.identity);
            
            tempBala.transform.rotation = Quaternion.Euler((enemigo.transform.position - puntaCanon.transform.position).normalized);
            tempBala.GetComponent<Bala>().destino = enemigo.transform.position;
        }
    }
}
