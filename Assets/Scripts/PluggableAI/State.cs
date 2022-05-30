using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI
{
    [CreateAssetMenu(fileName = "State", menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;
        public Color sceneGizmosColor = Color.gray;

        public void UpdateState(Enemy controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        public void DoActions(Enemy controller)
        {
            for (int i = 0; i < actions.Length; i++)
                actions[i].Act(controller);
        }

        private void CheckTransitions(Enemy controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceeded = transitions[i].decition.Decide(controller);

                if (decisionSucceeded)
                    controller.EnemyState.TransitionToState(transitions[i].trueState);
                else
                    controller.EnemyState.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
