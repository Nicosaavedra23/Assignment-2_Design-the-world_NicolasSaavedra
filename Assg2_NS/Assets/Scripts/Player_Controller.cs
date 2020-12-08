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

    [SerializeField]private int m_collectablesTotalCount, m_collectablesCounter;
    [SerializeField] private int count;

    private float m_movementX;
    private float m_movementY;

    private GameObject m_gameover;
     
    private Stopwatch m_stopwatch;

    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();

        m_collectablesTotalCount =  GameObject.FindGameObjectsWithTag("PickUp").Length;

        count = m_collectablesCounter;

        m_collectablesCounter = m_collectablesCounter++;

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

        m_collectablesCounter = m_collectablesCounter++;
        
        countText.text = "Coins: " + m_collectablesCounter.ToString() + " / " + m_collectablesTotalCount.ToString();

        if (count == 7 )
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

            countText.text = "Coins: " + m_collectablesCounter.ToString() + " / " + m_collectablesTotalCount.ToString();

            SetCountText();

            m_collectablesCounter++;

            if (m_collectablesCounter == 7)
            {
                winTextObject.SetActive(true);
                UnityEngine.Debug.Log("Winner!!!");
                UnityEngine.Debug.Log($"Your time is {m_stopwatch.Elapsed} you get {m_collectablesTotalCount} coins.");
                StartCoroutine(waitALittleBit());

           }
            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} coins!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("GAME OVER");

            gameoverTextObject.SetActive(true);

            StartCoroutine(waitALittleBit());

        }
    }
    public IEnumerator waitALittleBit()
    {
        yield return new WaitForSeconds(5);
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}