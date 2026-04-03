using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] float damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Damage(damage);
        }
    }
}
