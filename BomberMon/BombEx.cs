using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEx : MonoBehaviour
{
    public float countdown = 2f;
    public bool getBomb;
    private Pickup picksc;
    public GameObject player;
    // private NavBaker baker;
    public GameObject explosionPrefab;
    private AudioSource pick;
    private bool onetime;
    public AudioSource explodems;
    public bool explode = false;
    // Start is called before the first frame update
    void Start()
    {
        picksc = GameObject.Find("Player").GetComponent<Pickup>();
        pick = GetComponent<AudioSource>();
        //baker = GameObject.Find("EventSystem").GetComponent<NavBaker>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            getBomb = true;
            Debug.Log("collided");

        }


    }
    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            explode = true;
            FindObjectOfType<MapDestroyer>().Explode(transform.position);
            StartCoroutine(DestroyB());

        }

        if (getBomb == true && Input.GetKeyDown(KeyCode.Space))
        {

            if (!onetime)
            {
                pick.Play();
                onetime = true;
            }

            Destroy(this.gameObject);
            //gameObject.SetActive(false);
            picksc.hasBomb = true;

        }
    }
    IEnumerator DestroyB()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

}