using UnityEngine;

public class RandomAnimation : StateMachineBehaviour
{
    public int animCount;
    public string variableName;

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        int rand = Random.Range(0, animCount);
        animator.SetInteger(variableName, rand);
    }
}
