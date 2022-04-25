using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminTorres : MonoBehaviour
{
    public AdminToques adminToques;
    public AdminJuego referenciaAdminJuego;
    public EnemySpawner referenciaES;
    public GameObject Objetivo;
    public enum TorreSeleccionada 
    {
        torre1,torre2,torre3
    }
    public TorreSeleccionada torreSeleccionada;
    public List<GameObject> prefabsTorres;

    public List<GameObject> torres;

    public delegate void EnemigoObjetivoActualizado();
    public event EnemigoObjetivoActualizado EnEnemigoObjetivoActualizado;

    private void OnEnable()
    {
        adminToques.EnSoporteTocado += CrearTorre;
        referenciaES.enOlaEmpezada += ActualizarObjetivo;
        torres = new List<GameObject>();
    }


    private void OnDisable()
    {
        adminToques.EnSoporteTocado -= CrearTorre;
        referenciaES.enOlaEmpezada -= ActualizarObjetivo;
    }

    private void ActualizarObjetivo()
    {
        if (referenciaES.IsWaveStarted) 
        {
            float distanciaMasCorta = float.MaxValue;
            GameObject enemigoMasCercano = null;
            foreach (GameObject enemigo in referenciaES.Enemigos) 
            {
                float dist = Vector3.Distance(enemigo.transform.position, Objetivo.transform.position);
                if (dist < distanciaMasCorta) 
                {
                    distanciaMasCorta = dist;
                    enemigoMasCercano = enemigo;
                }
            }
            if (enemigoMasCercano != null) 
            {
                foreach (GameObject torre in torres) 
                {
                    torre.GetComponent<TorreBase>().enemigo = enemigoMasCercano;
                    torre.GetComponent<TorreBase>().Disparar();
                }
                if (EnEnemigoObjetivoActualizado != null)
                {
                    EnEnemigoObjetivoActualizado();
                }
            }
            
        }
        Invoke("ActualizarObjetivo", 3);
    }

    public void CrearTorre(GameObject soporte) 
    {
        if (soporte.transform.childCount == 0)
        {
            Debug.Log("Creando torre");
            int costo = torreSeleccionada switch
            {
                TorreSeleccionada.torre1 => 400,
                TorreSeleccionada.torre2 => 600,
                TorreSeleccionada.torre3 => 800,
                _ => 0
            };
            referenciaAdminJuego.ModificarRecursos(-costo);
            int indiceTorre = (int)torreSeleccionada;
            Vector3 posParaInstanciar = soporte.transform.position;
            posParaInstanciar.y += 0.5f;
            GameObject torreInstanciada = Instantiate<GameObject>(prefabsTorres[indiceTorre], posParaInstanciar, Quaternion.identity);
            torreInstanciada.transform.SetParent(soporte.transform);
            torres.Add(torreInstanciada);
        }
        
    }

    public void SetTorre(int torre) 
    {
        if (Enum.IsDefined(typeof(TorreSeleccionada), torre))
        {
            torreSeleccionada = (TorreSeleccionada)torre;
        }
        else 
        {
            Debug.LogError("esa torre no existe!!!");
        }
    }
}
