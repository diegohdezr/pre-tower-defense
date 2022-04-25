using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : EnemigoBase
{
    public void Awake()
    {
        vida = 200;
        _dano = 40;
    }
    private void OnDestroy()
    {
        ReferenciaAdminJuego.enemigosJefeDerrotados++;
        ReferenciaAdminJuego.ModificarRecursos(1000);
        referenciaEnemySpawner.Enemigos.Remove(this.gameObject);
    }
}
