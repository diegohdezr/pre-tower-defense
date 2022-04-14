using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{

    public GameObject targetGO;
    public int vida = 100;

    public Animator bossAnim;
    // Start is called before the first frame update

    private void OnEnable()
    {
        targetGO = GameObject.Find("Objetivo");
        targetGO.GetComponent<Objetivo>().EnObjetivoDestruido += Detener;
            
    }

    private void OnDisable()
    {
        targetGO.GetComponent<Objetivo>().EnObjetivoDestruido -= Detener;
    }

    private void Detener()
    {
        bossAnim.SetTrigger("OnObjectiveDestroyed");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }

    void Start()
    {
        
        GetComponent<NavMeshAgent>().SetDestination(targetGO.transform.position);
        bossAnim = GetComponent<Animator>();
        bossAnim.SetBool("IsWalking", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objetivo") 
        {
            bossAnim.SetBool("IsWalking", false);
            bossAnim.SetTrigger("OnObjectiveReached");
            
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (vida <= 0) 
        {
            bossAnim.SetTrigger("OnDeath");
            GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
            Destroy(gameObject, 3);
        }
        
    }

    public void Danar() 
    {
        targetGO?.GetComponent<Objetivo>().RecibirDano(40);
    }

    public void RecibirDano(int dano = 5) 
    {
        vida -= dano;
    }
    
}
