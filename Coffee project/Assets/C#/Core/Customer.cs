using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private int pathStage;
    private bool requestedOrder;
    private CustomersManager manager;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = FindObjectOfType<CustomersManager>();

        StartJourney();
    }
    void StartJourney()
    {

        agent.SetDestination(manager.MiddlePoint.position);
        pathStage = 1;
    }
    public void OrderCompleted()
    {
        pathStage = 3;
    }
    void Update()
    {
        if (pathStage != 0)
        {
            if (agent.remainingDistance < 0.1f)
            {
                if (pathStage == 5 && !agent.pathPending)
                {
                    manager.SpawnNextCustomer();
                    Destroy(gameObject);
                    return;
                }
                if (pathStage == 1)
                {
                    pathStage = 2;
                    agent.SetDestination(manager.DestinationForCustomers.position);
                }
                if (pathStage == 2)
                {
                    if (!requestedOrder)
                    {
                        requestedOrder = true;
                        Debug.Log("Order requested");
                        manager.SetCurrentOrder(manager.GenerateRandomOrder(), this);
                    }
                }
                if (pathStage == 4)
                {
                    pathStage = 5;
                    agent.SetDestination(manager.ReturnPoint.position);
                }
            }
            if (pathStage == 3)
            {
                pathStage = 4;
                agent.SetDestination(manager.MiddlePoint.position);
            }
        }
    }

}