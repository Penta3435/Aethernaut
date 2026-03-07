using UnityEngine;

public class Island3EnableInteractableCristal : MonoBehaviour
{
    [SerializeField] GameObject InteractObj;
    public void Enable()
    {
        InteractObj.SetActive(true);
    }
}
