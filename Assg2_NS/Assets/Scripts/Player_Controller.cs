using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Diagnostics;
using TMPro;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;

    [SerializeField] private TextMeshProUGUI countText = null;
    [SerializeField] private GameObject winTextObject = null;
    [SerializeField] private GameObject gameoverTextObject = null;
 
    private Rigidbody m_playerRigidbody = null;

    private int m_collectablesTotalCount, m_collectablesCounter;
    private int count;

    private float m_movementX;
    private float m_movementY;

    private GameObject m_gameover;
     
    private Stopwatch m_stopwatch;

    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();

        count = 0;

        m_collectablesTotalCount = 0;

        m_collectablesCounter = 0;

        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("PickUp").Length;

        SetCountText();

        winTextObject.SetActive(false);

        gameoverTextObject.SetActive(false);

        m_stopwatch = Stopwatch.StartNew();
             
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();

        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count>= 31 )
        {
            winTextObject.SetActive(true);
            gameoverTextObject.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY);

        m_playerRigidbody.AddForce(movement * m_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();

            m_collectablesCounter--;
            if (m_collectablesCounter == 0)
            {
                UnityEngine.Debug.Log("Winner!!!");
                UnityEngine.Debug.Log($"Your time is {m_stopwatch.Elapsed} you get {m_collectablesTotalCount} diamont.");
#if UNITY_EDITOR 
                UnityEditor.EditorApplication.ExitPlaymode();
#endif

            }
            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} diamont!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("GAME OVER");

            gameoverTextObject.SetActive(true);


#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif

        }
    }
   
}