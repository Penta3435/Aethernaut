using UnityEngine;
using UnityEngine.Events;

public class CubePuzzleMain : MonoBehaviour
{
    [SerializeField] CubePuzzle[] allCubes;
    [SerializeField] float animDuration = 1f;

    public UnityEvent OnInteract;
    [SerializeField] private Animator animator;
    [SerializeField] string animationStateName;

    bool solved = false;
    private void Awake()
    {
        foreach (var cube in allCubes)
        {
            cube.puzzleMain = this;
            cube.animDuration = animDuration;
        }
    }
    public void CheckSolvedAfterAnim()
    {
        Invoke("CheckSolved", animDuration + 0.05f);
    }
    void CheckSolved()
    {
        if (solved) return;

        foreach (CubePuzzle cube in allCubes)
        {
            if (cube.currentState != 0)
            {
                return;
            }
        }
        solved = true;
        OnInteract?.Invoke();
        if (animator != null) animator.Play(animationStateName);
    }
}
