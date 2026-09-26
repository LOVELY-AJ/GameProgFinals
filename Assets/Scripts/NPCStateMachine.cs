using System.Net.NetworkInformation;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    [Header("Points")]
    public Transform counterPoint;
    public Transform exitPoint;
    public NPCMovement movement;
    private Coroutine waitCoroutine;
    private bool leaveCoroutineRunning = false;
    public NPCStateEnum currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = NPCStateEnum.Entering;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case NPCStateEnum.Entering:
                if (movement.MoveTo(counterPoint.position) && !movement.IsMoving())
                {
                    currentState = NPCStateEnum.Ordering;
                }
                break;
            case NPCStateEnum.Ordering:
                Order();
                break;
            case NPCStateEnum.Exiting:
                if (movement.MoveTo(exitPoint.position) && !movement.IsMoving())
                {
                    currentState = NPCStateEnum.Satisfied;
                    Destroy(gameObject, 0.2f);
                }
                    break;
            case NPCStateEnum.Ghost:
                Ghost();
                break;
        }
    }

    public void Order() { }
    public void Ghost() { }

    }
