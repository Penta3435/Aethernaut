using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enableWhenStartGame;
    [SerializeField] GameObject[] disabeWhenStartGame;

    int cristalVaporCount;
    int fragVaporCount;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftAlt)|| Input.GetKeyUp(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        foreach (var i in enableWhenStartGame)
        {
            i.SetActive(true);
        }
        foreach (var i in disabeWhenStartGame)
        {
            i.SetActive(false);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddOneCristalVaport()
    {
        cristalVaporCount++;
        UiManager.instance.SetCristalVapor(cristalVaporCount);
    }
}
