using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-4)]
public class Objetivo : MonoBehaviour
{

    public int vida = 100;

    public delegate void ObjetivoDestruido();
    public event ObjetivoDestruido EnObjetivoDestruido;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0) 
        {
            if (EnObjetivoDestruido != null)
            {
                EnObjetivoDestruido();
            }
            Destroy(gameObject,0.5f);
        }
    }
    //TODO: implementar interfaz IDanable
    public void RecibirDano(int dano =20) 
    {
        vida -= dano;
    }
}
