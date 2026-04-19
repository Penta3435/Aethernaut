using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Agent1 : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject hitbox;
    [SerializeField] Transform hitboxPos;

    int AtackState;
    private void Start()
    {
        StartCoroutine("Run");
        AtackState = Animator.StringToHash("Atack");
    }
    void Update()
    {
        Debug.DrawRay(transform.position+Vector3.up, new Vector3(0,0,6), Color.red);
        Debug.DrawRay(transform.position+Vector3.up, new Vector3(0,0,-13), Color.blue);
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AtackState)
        {
            animator.SetFloat("X", agent.velocity.normalized.x);
            animator.SetFloat("Y", agent.velocity.normalized.y);
        }
        foreach(var i in Physics.OverlapSphere(transform.position, 6))
        {
            if (i.CompareTag("Player") && animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AtackState)
            {
                transform.forward = (i.transform.position - transform.position).normalized;
                Atack();
                print("Atack");
            }
        }

    }
    IEnumerator Run()
    {
        while(true) 
        {
            if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AtackState)
            {
                Vector3 pos = transform.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));
                foreach (var i in Physics.OverlapSphere(transform.position, 13))
                {
                    if (i.CompareTag("Player"))
                    {
                        pos = i.transform.position;
                        break;
                    }
                }
                agent.SetDestination(pos);
            }
            yield return new WaitForSeconds(3);
        }
    }
    void Atack()
    {
        agent.ResetPath();
        animator.SetTrigger("Atack");
    }
    public void InstantiateHitbox()
    {
        Instantiate(hitbox,hitboxPos.position,transform.rotation);
    }
}
