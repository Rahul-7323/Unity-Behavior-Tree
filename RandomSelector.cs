using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelector : Node
{
    public enum ActionState { IDLE, WORKING };
    public ActionState state = ActionState.IDLE;
    List<int> visitedChildren = new List<int>();
    public RandomSelector() { }

    public RandomSelector(string name)
    {
        this.name = name;
    }

    public override Status Process()
    {
        if(state == ActionState.IDLE){
            if(visitedChildren.Count == children.Count){
                return Status.FAILURE;
            }
            else{
                currentChild = Mathf.FloorToInt(Random.Range(0, 10)) % (children.Count);
                Debug.Log("The currentChild is "+ currentChild);
                while(visitedChildren.Contains(currentChild)){
                    currentChild = (currentChild+1)%(children.Count);
                }
                state = ActionState.WORKING;
                return children[currentChild].Process();
            }
            
        }
        else{
            Status childStatus = children[currentChild].Process();
            if(childStatus == Status.SUCCESS){
                state = ActionState.IDLE;
                return Status.SUCCESS;
            }
            if(childStatus == Status.FAILURE){
                visitedChildren.Add(currentChild);
                state = ActionState.IDLE;
            }

            return Status.RUNNING;
        }
    }
}
