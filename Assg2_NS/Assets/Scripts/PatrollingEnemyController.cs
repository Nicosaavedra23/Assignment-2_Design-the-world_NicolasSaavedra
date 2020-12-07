using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyController : EnemyController
{
    [SerializeField] private Transform[] m_wayPoints = null;

    private int m_wayPointIndex = 0;

    protected override Vector3 GetNextDestination()
    {
        Vector3 destination = m_wayPoints[m_wayPointIndex].position;

        m_wayPointIndex = (m_wayPointIndex + 1) % m_wayPoints.Length;

        return destination;
    }

}