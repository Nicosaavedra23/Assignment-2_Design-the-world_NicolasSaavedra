using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null;
    [SerializeField] private float m_detectionRadius = 4f;

    [SerializeField] private Material m_idleMaterial = null;
    [SerializeField] private Material m_chasingMaterial = null;

    private NavMeshAgent m_agent;
    private Vector3 m_initialPosition;

    protected virtual Vector3 GetNextDestination()
    {
        return m_initialPosition;
    }

    private void Start()
    {
        m_agent = gameObject.GetComponent<NavMeshAgent>(); 
        m_initialPosition = gameObject.transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(m_playerObject.transform.position, gameObject.transform.position) < m_detectionRadius)  
        {
            m_agent.GetComponent<Renderer>().material = m_chasingMaterial; 
            m_agent.SetDestination(m_playerObject.transform.position); 
            return;
        }

        m_agent.GetComponent<Renderer>().material = m_idleMaterial; 
        if (m_agent.remainingDistance < 0.5f) 
            m_agent.SetDestination(GetNextDestination());
    }
}
