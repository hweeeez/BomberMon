using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ghostPrefab;
    public GameObject[] ghosts;
    public GameObject gameOver;
    public GameObject losesc;
    private PlayerLife lifesc;
    public AudioSource losems;
    public AudioSource winms;

    public AudioSource levelms;
    private bool onetime = false;
    private bool isLose = true;


    void Start()
    {
        lifesc = GameObject.Find("Player").GetComponent<PlayerLife>();
        gameOver.SetActive(false);
        losesc.SetActive(false);
        levelms.Play();
    }
    // Update is called once per frame
    void Update()
    {
        ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        if (ghosts.Length == 0 && isLose == true)
        {
            StartCoroutine(Winsc());
            if (!onetime)
            {
                StartCoroutine(WinT());
                onetime = true;
            }


        }
        if (lifesc.numberOfLives == 0)
        {
            isLose = false;
            StartCoroutine(Losesc());
            if (!onetime)
            {
                StartCoroutine(LoseT());
                onetime = true;
            }


    }
    IEnumerator Winsc()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(false);

        }
        Time.timeScale = 0;
    }
    IEnumerator Losesc()
    {

        yield return new WaitForSeconds(1);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(false);
        }
        Time.timeScale = 0;
    }
    IEnumerator LoseT()
    {
        yield return new WaitForSeconds(0.5f);
        levelms.Stop();
        losesc.SetActive(true);
        losems.PlayOneShot(losems.clip);
        yield return new WaitForSeconds(3);
        levelms.Play();


    }
    IEnumerator WinT()
    {
        yield return new WaitForSeconds(0.5f);
        levelms.Stop();
        gameOver.SetActive(true);
        winms.PlayOneShot(winms.clip);
        yield return new WaitForSeconds(4);
        levelms.Play();


    }
}


