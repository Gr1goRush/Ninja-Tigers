using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class trening : MonoBehaviour
{
    public string[] name;
    public GameObject panel;

    public TextMeshProUGUI proUGUI;
    public GameObject collisions;// Тригер

    bool isFinal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("TC1"))
        {
            collisions = collision.gameObject;

            panel.SetActive(true);

            proUGUI.text = name[0];

            Time.timeScale = 0f;
        }
        if (collision.gameObject.CompareTag("TC2"))
        {
            collisions = collision.gameObject;

            panel.SetActive(true);

            proUGUI.text = name[1];

            Time.timeScale = 0f;
        }
        if (collision.gameObject.CompareTag("TC21"))
        {
            collisions = collision.gameObject;

            panel.SetActive(true);

            proUGUI.text = name[2];

            Time.timeScale = 0f;
        }
        if (collision.gameObject.CompareTag("TC3"))
        {
            isFinal = true;

            collisions = collision.gameObject;

            panel.SetActive(true);

            proUGUI.text = name[3];

            Time.timeScale = 0f;
        }
    }
    public void TC1()
    {
        if(isFinal)
        {
            SceneManager.LoadScene(2);
        }

        Time.timeScale = 1f;

        panel.SetActive(false);

        collisions.SetActive(false);
    }
}
