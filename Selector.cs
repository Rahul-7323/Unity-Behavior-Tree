using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector() { }

    public Selector(string name){
        this.name = name;
    }

    public override Status Process()
    {
        // currentChild = (Mathf.FloorToInt(Random.Range(0, 10)))%(children.Count);
        Status childStatus = children[currentChild].Process();
        if(childStatus == Status.SUCCESS){
            currentChild = 0;
            return Status.SUCCESS;
        }

        if(childStatus == Status.RUNNING){
            return Status.RUNNING;
        }

        currentChild++;

        if(currentChild >= children.Count){
            Debug.Log(name);
            currentChild = 0;
            return Status.FAILURE;
        }

        return Status.RUNNING;
    }
}
