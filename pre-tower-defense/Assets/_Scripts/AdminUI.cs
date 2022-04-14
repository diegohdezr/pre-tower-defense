using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminUI : MonoBehaviour
{

    public GameObject canvasPrincipal;
    public GameObject menuGameOver;
    public EnemySpawner eSReferencia;
    public Objetivo oReferencia;

    private void OnEnable()
    {
        eSReferencia.EnOlaTerminada += mostrarMenuFinOla;
        oReferencia.EnObjetivoDestruido += MostrarMenuGameOver;
    }
    private void OnDisable()
    {
        eSReferencia.EnOlaTerminada -= mostrarMenuFinOla;
        oReferencia.EnObjetivoDestruido -= MostrarMenuGameOver;
    }

    public void mostrarMenuFinOla()
    {

    }
    public void ocultarMenuFinOla()
    {

    }

    public void MostrarMenuGameOver() 
    {
        menuGameOver.SetActive(true);
    }

    public void OcultarMenuGameOver() 
    {
        menuGameOver.SetActive(true);
    }

    public void FinalizarJuego() 
    {
        Application.Quit();
    }

    public void CargarMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void ReintentarNivel() 
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);
    }

    private void Start()
    {

    }
}
