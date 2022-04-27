using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    // public GameObject frontDoor;
    // public GameObject backDoor;

    public GameObject Door11;
    public GameObject Door12;
    public GameObject Door13;
    public GameObject Door21;
    public GameObject Door22;
    public GameObject Door31;
    public GameObject Door32;
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING };
    public ActionState state = ActionState.IDLE;
    public Node.Status treeState = Node.Status.RUNNING;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        this.tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal something");
        Leaf goToDiamond = new Leaf("Go to Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go to Van", GoToVan);
        // Leaf goToFrontDoor = new Leaf("Go to Front Door", GoToFrontDoor);
        // Leaf goToBackDoor = new Leaf("Go to Back Door", GoToBackDoor);
        Leaf goToDoor11 = new Leaf("Go to Door 11", GoToDoor11);
        Leaf goToDoor12 = new Leaf("Go to Door 12", GoToDoor12);
        Leaf goToDoor13 = new Leaf("Go to Door 13", GoToDoor13);
        Leaf goToDoor21 = new Leaf("Go to Door 21", GoToDoor21);
        Leaf goToDoor22 = new Leaf("Go to Door 22", GoToDoor22);
        Leaf goToDoor31 = new Leaf("Go to Door 31", GoToDoor31);
        Leaf goToDoor32 = new Leaf("Go to Door 32", GoToDoor32);
        
        // Selector openDoor = new Selector("Open Door");
        // openDoor.AddChild(goToFrontDoor);
        // openDoor.AddChild(goToBackDoor);

        Selector openDoor1 = new Selector("Open Level 1 doors");
        openDoor1.AddChild(goToDoor13);

        Selector openDoor2 = new Selector("Open Level 2 doors");
        openDoor2.AddChild(goToDoor21);
        openDoor2.AddChild(goToDoor22);

        Selector openDoor3 = new Selector("Open Level 3 doors");
        openDoor3.AddChild(goToDoor31);
        openDoor3.AddChild(goToDoor32);

        // steal.AddChild(openDoor);
        steal.AddChild(openDoor1);
        steal.AddChild(openDoor2);
        steal.AddChild(openDoor3);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);

        tree.AddChild(steal);

        // tree.PrintTree();


    }

    public Node.Status GoToDiamond() {
        Node.Status currState = GoToLocation(diamond.transform.position);
        if(currState == Node.Status.SUCCESS){
            diamond.transform.parent = this.transform;
        }
        return currState;
    }

    public Node.Status GoToVan()
    {
        return GoToLocation(van.transform.position);
    }

    public Node.Status GoToDoor(GameObject door){
        Node.Status doorStatus = GoToLocation(door.transform.position);
        if(doorStatus == Node.Status.SUCCESS){
            if(!door.GetComponent<Lock>().isLocked){
                door.SetActive(false);
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        }
        return doorStatus;
    }


    public Node.Status GoToDoor11(){
        return GoToDoor(Door11);
    }

    public Node.Status GoToDoor12()
    {
        return GoToDoor(Door12);
    }

    public Node.Status GoToDoor13()
    {
        return GoToDoor(Door13);
    }

    public Node.Status GoToDoor21()
    {
        return GoToDoor(Door21);
    }

    public Node.Status GoToDoor22()
    {
        return GoToDoor(Door22);
    }

    public Node.Status GoToDoor31()
    {
        return GoToDoor(Door31);
    }

    public Node.Status GoToDoor32()
    {
        return GoToDoor(Door32);
    }

    public Node.Status GoToLocation(Vector3 destination){
        float distanceToTarget = Vector3.Distance(this.transform.position, destination);

        if(state == ActionState.IDLE){
            agent.SetDestination(destination);
            state = ActionState.WORKING;

        }

        else if (Vector3.Distance(agent.pathEndPosition, destination) >= 3) {
            Debug.Log("No way to go to " + destination);
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }

        else if (distanceToTarget < 3){
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        if(treeState == Node.Status.RUNNING){
            treeState = tree.Process();
        }
    }
}
