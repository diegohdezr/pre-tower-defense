using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> EnemyPrefabs;
    public int ola;
    public List<int> enemigosPorOla;

    private int enemigosDuranteEstaOla;

    public delegate void OlaTerminada();
    public event OlaTerminada EnOlaTerminada;

    // Start is called before the first frame update
    void Start()
    {
        ola = 0;
        ConfigurarCantidadEnemigos();
        InstanciarEnemigo();
    }

    public void TerminarOla() 
    {
        if (EnOlaTerminada != null)
        {
            EnOlaTerminada();
        }
    }
    public void ConfigurarCantidadEnemigos()
    {
        enemigosDuranteEstaOla = enemigosPorOla[ola];
    }
    public void InstanciarEnemigo() 
    {
        int indiceAleatorio = Random.Range(0, EnemyPrefabs.Count);
        Instantiate<GameObject>(EnemyPrefabs[indiceAleatorio], transform.position, Quaternion.identity);
        enemigosDuranteEstaOla--;
        if (enemigosDuranteEstaOla < 0) 
        {
            ola++;
            ConfigurarCantidadEnemigos();
            TerminarOla();
            return;
        }
        Invoke("InstanciarEnemigo", 2);
        
    }
}
