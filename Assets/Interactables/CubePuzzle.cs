using UnityEngine;
using UnityEngine.Events;

public class CubePuzzle : MonoBehaviour
{
    [HideInInspector]public CubePuzzleMain puzzleMain;
    [SerializeField] float[] stateAngles;
    public int currentState;
    [SerializeField] CubePuzzle[] otherCubesToSpinWith;
    [HideInInspector] public float animDuration;




    [SerializeField] private Animator animator;

    public UnityEvent solvedEvent;
    [SerializeField] string solvedAnimationStateName;

    public UnityEvent unsolvedEvent;
    [SerializeField] string unsolvedAnimationStateName;



    bool changeState = false;
    int nextState;
    float lerpValue = 0; 

    private void Start()
    {
        if (currentState >= stateAngles.Length) currentState = 0;

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, stateAngles[currentState], transform.eulerAngles.z);
        nextState = currentState;
        SetNextState();
    }
    public void CubePuzzleInteract()
    {
        NextState();
        foreach (CubePuzzle cube in otherCubesToSpinWith)
        {
            cube.NextState();
        }
    }
    public void NextState()
    {
        print(gameObject.name);
        if (changeState == false)
        {
            changeState = true;
            lerpValue = 0;
        }
    }
    private void Update()
    {
        if (changeState) 
        {
            lerpValue += Time.deltaTime / animDuration;
            if(nextState != 0)
                this.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Lerp(stateAngles[currentState], stateAngles[nextState], lerpValue), transform.eulerAngles.z);
            else
                this.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Lerp(stateAngles[currentState], stateAngles[nextState]+360, lerpValue), transform.eulerAngles.z);
            
            if (lerpValue >= animDuration)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, stateAngles[nextState], transform.eulerAngles.z);
                changeState = false;
                currentState = nextState;
                SetNextState();
                lerpValue = 0;


                if(currentState == 0)
                { 
                    solvedEvent?.Invoke();
                    if (animator != null) animator.Play(solvedAnimationStateName);
                }
                else
                {
                    unsolvedEvent?.Invoke();
                    if (animator != null) animator.Play(unsolvedAnimationStateName);
                }
            }


            
        }
    }
    void SetNextState()
    {
        if (nextState + 1 < stateAngles.Length) nextState++;
        else nextState = 0;
    }
}
