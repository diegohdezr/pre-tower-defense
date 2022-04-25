using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> EnemyPrefabs;
    public int ola;
    public List<int> enemigosPorOla;

    private int enemigosDuranteEstaOla;

    public bool IsWaveStarted;
    public List<GameObject> Enemigos;

    public delegate void OlaEmpezada();
    public event OlaEmpezada enOlaEmpezada;

    public delegate void OlaTerminada();
    public event OlaTerminada EnOlaTerminada;

    public delegate void OlaGanada();
    public event OlaGanada EnOlaGanada;

    // Start is called before the first frame update
    void Start()
    {
        ola = 0;
    }

    private void FixedUpdate()
    {
        if (IsWaveStarted && Enemigos.Count == 0) 
        {
            GanarOla();
        }
    }

    public void TerminarOla() 
    {
        if (EnOlaTerminada != null)
        {
            EnOlaTerminada();
        }
    }

    public void EmpezarOleada()
    {
        IsWaveStarted = true;
        if (enOlaEmpezada != null) 
        {
            enOlaEmpezada();
        }
        ConfigurarCantidadEnemigos();
        InstanciarEnemigo();
    }

    public void ConfigurarCantidadEnemigos()
    {
        enemigosDuranteEstaOla = enemigosPorOla[ola];
    }

    public void GanarOla() 
    {
        if (IsWaveStarted && EnOlaGanada!=null) 
        {
            EnOlaGanada();
            IsWaveStarted = false;
        }
        return;
    }
    public void InstanciarEnemigo() 
    {
        int indiceAleatorio = Random.Range(0, EnemyPrefabs.Count);
        var tmpEnemigo = Instantiate<GameObject>(EnemyPrefabs[indiceAleatorio], transform.position, Quaternion.identity);
        Enemigos.Add(tmpEnemigo);
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
