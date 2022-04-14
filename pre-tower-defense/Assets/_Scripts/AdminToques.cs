using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-5)]
public class AdminToques : MonoBehaviour
{

    public InputActionAsset inputs;

    private InputAction toque;
    private InputAction TouchPosition;
    private Camera cam;

    public delegate void SoporteTocado(GameObject soporte);
    public event SoporteTocado EnSoporteTocado;

    private void OnEnable()
    {
        TouchSimulation.Enable();
        inputs.Enable();
        toque = inputs.FindAction("Touch1");
        TouchPosition = inputs.FindAction("TouchPosition");
        toque.performed += Toque;
    }

    private void OnDisable()
    {
        inputs.Disable();
        TouchSimulation.Disable();
        toque.performed -= Toque;
    }

    private void Start()
    {
        cam = Camera.main;
        
    }

    public void Toque(InputAction.CallbackContext context) 
    {
        Vector2 poseToque2D = TouchPosition.ReadValue<Vector2>();
        Vector3 poseToque3D = new Vector3(poseToque2D.x, poseToque2D.y, cam.farClipPlane);
        Ray screenRay = cam.ScreenPointToRay(poseToque3D);
        Debug.Log("pantalla tocada");
        Debug.Log($"el toque fue en: {poseToque2D}");
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "Soporte")
            {
                Debug.Log("Soporte tocado");
                if (EnSoporteTocado != null)
                {
                    EnSoporteTocado(hit.transform.gameObject);
                }
            }
        }
        else 
        {
            Debug.Log("no hubo hit del raycast");
        }
    }
}
