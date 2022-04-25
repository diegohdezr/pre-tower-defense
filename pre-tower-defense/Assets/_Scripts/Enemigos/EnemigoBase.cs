using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBase : MonoBehaviour, IAtacable,IAtacante
{
    public GameObject targetGO;
    public AdminJuego ReferenciaAdminJuego;
    public int vida = 100;
    public int _dano = 5;
    

    public Animator bossAnim;
    protected EnemySpawner referenciaEnemySpawner;
    // Start is called before the first frame update

    private void OnEnable()
    {
        referenciaEnemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        targetGO = GameObject.Find("Objetivo");
        targetGO.GetComponent<Objetivo>().EnObjetivoDestruido += Detener;
        ReferenciaAdminJuego = GameObject.Find("AdminJuego").GetComponent<AdminJuego>();

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

    public void Danar(int dano )
    {
        if (dano == 0) dano = _dano;
        targetGO?.GetComponent<Objetivo>().RecibirDano(dano);
    }

    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }
}
