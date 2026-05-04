using UnityEngine;

public class OpenCloseButton : MonoBehaviour
{
    [SerializeField] GameObject[] elements;
    public void Open()
    {
        foreach (GameObject go in elements)
        {
            go.SetActive(true);
        }
    }
    public void Close()
    {
        foreach(GameObject go in elements)
        {
            go.SetActive(false);
        }
    }
}
