using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Chest : MonoBehaviour, IInteractables
{
    public UnityEvent OnOpen;
    [SerializeField] Animator animator;
    [SerializeField] GameObject FocusedIcon;

    public bool Focused { get; set; }
    bool interactable = true;

    int openId;
    TagHandle playerTag;
    private void Awake()
    {
        openId = Animator.StringToHash("Open");
        Focused = false;
        playerTag = TagHandle.GetExistingTag("Player");
    }
    void IInteractables.Interact(PlayerController playerController)
    {
        if (interactable)
        {
            OnOpen.Invoke();
            interactable = false;
            animator.SetTrigger(openId);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
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
