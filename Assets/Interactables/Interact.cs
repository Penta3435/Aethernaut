using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour, IInteractables
{
    public UnityEvent OnInteract;
    public bool infiniteInteracts = false;
    [SerializeField] GameObject FocusedIcon;
    [SerializeField] private Animator animator;
    [SerializeField] string animationStateName;


    public bool Focused { get; set; }

    TagHandle playerTag;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactables");
        Focused = false;
        playerTag = TagHandle.GetExistingTag("Player");
    }
    void IInteractables.Interact()
    {
        OnInteract?.Invoke();
        if(animator != null) animator.Play(animationStateName);
        if (!infiniteInteracts)
        {
            FocusedIcon.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && Focused == true)
        {
            FocusedIcon.SetActive(true);
            Focused = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            FocusedIcon.SetActive(false);
        }
    }
}
