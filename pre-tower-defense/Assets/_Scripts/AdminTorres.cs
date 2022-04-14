using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminTorres : MonoBehaviour
{
    public AdminToques adminToques;
    public enum TorreSeleccionada 
    {
        torre1,torre2,torre3
    }
    public TorreSeleccionada torreSeleccionada;
    public List<GameObject> prefabsTorres;

    private void OnEnable()
    {
        adminToques.EnSoporteTocado += CrearTorre;
    }
    private void OnDisable()
    {
        adminToques.EnSoporteTocado -= CrearTorre;
    }

    public void CrearTorre(GameObject soporte) 
    {
        if (soporte.transform.childCount == 0)
        {
            Debug.Log("Creando torre");
            int indiceTorre = (int)torreSeleccionada;
            Vector3 posParaInstanciar = soporte.transform.position;
            posParaInstanciar.y += 0.5f;
            GameObject torreInstanciada = Instantiate<GameObject>(prefabsTorres[indiceTorre], posParaInstanciar, Quaternion.identity);
            torreInstanciada.transform.SetParent(soporte.transform);
        }
        //escribir el codigo dentro del if y despues el if para validar que no se sobre escriban las torres
        
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
