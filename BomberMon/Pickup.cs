using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    public Transform player;
    public GameObject bomb;
 
    public Tilemap tilemap;
    public BombEx bombsc;
    public GameObject playerbomb;
    public bool hasBomb;
    public bool isplayerbomb;
    private bool onetime;
    public AudioSource getB;
    public AudioSource explodems;
    public AudioSource pick;

    void Start()
    {
        BombEx bombsc = bomb.GetComponent<BombEx>();
 
    }

    private void Update()
    {
        if (hasBomb == true)
        {
            if (!onetime)
            {
                pick.Play();
                onetime = true;
            }
        }

        if (hasBomb == true && Input.GetKeyDown(KeyCode.Z))
        {

            haveBomb();
            hasBomb = false;
            isplayerbomb = true;
            if (!onetime)
            {
                getB.Play();
                onetime = true;
            }
        }
    }


    public void haveBomb()
    {

        Vector3 worldPos = player.transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        Instantiate(playerbomb, cellCenterPos, Quaternion.identity);
        StartCoroutine(playSound());
    }
    IEnumerator playSound()
    {
        yield return new WaitForSeconds(1.7f);
        explodems.Play();
    }



