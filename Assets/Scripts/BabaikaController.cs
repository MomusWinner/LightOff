using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class BabaikaController : MonoBehaviour
{
    [SerializeField] GameObject _target;
    
    private NavMeshAgent _agent;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }
}
