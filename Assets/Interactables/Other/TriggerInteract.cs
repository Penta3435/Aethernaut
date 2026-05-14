using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class TriggerInteract : MonoBehaviour
{
    public UnityEvent OnInteract;
    [SerializeField] private Animator animator;
    [SerializeField] string animationStateName;
    [SerializeField] bool infiniteInteracts = false;
    [SerializeField] AudioClip audioOnInteract;

    TagHandle playerTag;
    private void Awake()
    {
        playerTag = TagHandle.GetExistingTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            OnInteract?.Invoke();
            if (animator != null) animator.Play(animationStateName);

            if(audioOnInteract != null && SFXManager.instance != null)SFXManager.instance.ReproduceAudioClip(audioOnInteract);

            if (!infiniteInteracts)
            {
                Destroy(gameObject);
            }

        }
    }
}
