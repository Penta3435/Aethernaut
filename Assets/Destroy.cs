using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float time = 3f;
    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
