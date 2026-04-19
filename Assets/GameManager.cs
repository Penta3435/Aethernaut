using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enableWhenStartGame;
    [SerializeField] GameObject[] disabeWhenStartGame;

    [SerializeField] GameObject[] disableWhenFinishGame;

    [SerializeField] Animator lastAnimationAnimator;
    [SerializeField] string lasAnimationStateName = "FinalAnim";

    int cristalVaporCount;
    int fragVaporCount = 0;

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
    public void AddOneFragVaport()
    {
        fragVaporCount++;
        UiManager.instance.SetFragVapor(fragVaporCount);
    }
    public void CheckGameCompleted()
    {
        if(cristalVaporCount == 4)
        {
            foreach (var i in disableWhenFinishGame)
            {
                i.SetActive(false);
            }
            if (lastAnimationAnimator != null)lastAnimationAnimator.Play(lasAnimationStateName);
        }
    }
}
