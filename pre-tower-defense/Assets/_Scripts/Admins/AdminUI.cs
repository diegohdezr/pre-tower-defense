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
    public GameObject menuFinOla;
    public GameObject MensajeUltimoEnemigo;
    public EnemySpawner eSReferencia;
    public AdminJuego referenciaAdminJuego;
    public Objetivo oReferencia;
    public TMPro.TMP_Text textoRecursos;
    public TMPro.TMP_Text textoOleada;
    public TMPro.TMP_Text textoEnemigos;
    public TMPro.TMP_Text textoJefes;

    private void OnEnable()
    {
        referenciaAdminJuego.EnRecursosModificados += ActualizarRecursos;
        eSReferencia.EnOlaGanada+= mostrarMenuFinOla;
        eSReferencia.enOlaEmpezada += ActualizarOla;
        eSReferencia.EnOlaTerminada += MostrarMensajeUltimoEnemigo;
        oReferencia.EnObjetivoDestruido += MostrarMenuGameOver;
    }
    private void OnDisable()
    {
        referenciaAdminJuego.EnRecursosModificados -= ActualizarRecursos;
        eSReferencia.EnOlaGanada -= mostrarMenuFinOla;
        eSReferencia.enOlaEmpezada -= ActualizarOla;
        eSReferencia.EnOlaTerminada -= MostrarMensajeUltimoEnemigo;
        oReferencia.EnObjetivoDestruido -= MostrarMenuGameOver;
    }

    private void MostrarMensajeUltimoEnemigo()
    {
        MensajeUltimoEnemigo.SetActive(true);
        Invoke("OcultarMensajeUltimoEnemigo", 2);
    }

    private void OcultarMensajeUltimoEnemigo() 
    {
        MensajeUltimoEnemigo.SetActive(false);
    }

    private void ActualizarOla()
    {
        textoOleada.text = ($"Ola: {eSReferencia.ola}");
        ocultarMenuFinOla();
    }

    private void ActualizarRecursos()
    {
        textoRecursos.text = $"Recursos: {referenciaAdminJuego.recursos}";
    }

    public void mostrarMenuFinOla()
    {
        textoEnemigos.text = $"Enemigos Derrotados: \t{referenciaAdminJuego.enemigosBaseDerrotados}";
        textoJefes.text = $"Jefes Derrotados: \t{ referenciaAdminJuego.enemigosJefeDerrotados}";
        //posible error por condicion de carrera dado que este texto no se encuentra activo
        menuFinOla.SetActive(true);
    }
    public void ocultarMenuFinOla()
    {
        menuFinOla.SetActive(false);
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

}
