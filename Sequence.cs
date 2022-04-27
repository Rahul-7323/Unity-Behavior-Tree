using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() { }

    public Sequence(string name) {
        this.name = name;
    }

    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();
        if (childStatus == Status.FAILURE){
            Debug.Log(children[currentChild].name + " Failed");
            currentChild = 0;
            return Status.FAILURE;
        }
        if (childStatus == Status.RUNNING){
            return Status.RUNNING;
        }

        currentChild++;

        if (currentChild >= children.Count){
            currentChild = 0;
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }
}
