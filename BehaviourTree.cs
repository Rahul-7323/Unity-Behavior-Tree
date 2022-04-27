using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    public BehaviourTree(){
        name = "Tree";
    }

    public BehaviourTree(string name){
        this.name = name;
    }

    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();
        if (childStatus == Status.FAILURE)
        {
            return Status.FAILURE;
        }
        if (childStatus == Status.RUNNING)
        {
            return Status.RUNNING;
        }

        currentChild++;

        if (currentChild >= children.Count)
        {
            currentChild = 0;
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }

    public void PrintTree(){
        List<Node> queue = new List<Node>();
        queue.Add(this);
        while(queue.Count != 0){
            Node node = queue[0];
            queue.RemoveAt(0);
            Debug.Log("Children of " + node.name);
            string children = "";
            foreach(Node child in node.children){
                queue.Add(child);
                children += child.name + ", ";
            }
            Debug.Log(children);
        }
    }
}
