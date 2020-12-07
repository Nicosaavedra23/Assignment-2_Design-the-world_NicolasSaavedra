using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null;

    private Vector3 m_offset;

    private void Start()
    {
        m_offset = gameObject.transform.position - m_playerObject.transform.position;
    }

    private void Update()
    {
        gameObject.transform.position = m_playerObject.transform.position + m_offset;
    }

}
