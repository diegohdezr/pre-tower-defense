using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemigoBase
{
    public void Awake()
    {
        vida = 50;
        _dano = 5;
    }

    private void OnDestroy()
    {
        ReferenciaAdminJuego.enemigosBaseDerrotados++;
        ReferenciaAdminJuego.ModificarRecursos(200);
        referenciaEnemySpawner.Enemigos.Remove(this.gameObject);
    }
}
