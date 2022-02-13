using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile destructibleTile;
    private bool onetime = false;

    public GameObject explosionPrefab;
    public bool explosion = true;
    private NavBaker baker;
    float shakeMagnitude = 0.2f;
    float shakeDuration = .2f;
    public AudioSource explodems;

    public void Explode(Vector2 worldPos)
    {
        if (explosion == true)
        {
            Vector3Int originCell = tilemap.WorldToCell(worldPos);

            ExplodeCell(originCell);
            if (ExplodeCell(originCell + new Vector3Int(1, 0, 0)))
            {
                ExplodeCell(originCell + new Vector3Int(2, 0, 0));
            }

            if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
            {
                ExplodeCell(originCell + new Vector3Int(0, 2, 0));

            }
            if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
            {
                ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
            }

            if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
            {
                ExplodeCell(originCell + new Vector3Int(0, -2, 0));
            }
            StartCoroutine(Screenshake.ShakeCoroutine(Camera.main, shakeMagnitude, shakeDuration));


            StartCoroutine(playSound());

        }

    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile)
        {
            return false;
        }
        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
            baker.bake();
        }
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        return true;
    }
    IEnumerator playSound()
    {
        if (!onetime)
        {
            onetime = true;
            explodems.Play();
        }
        yield return new WaitForSeconds(1f);
        onetime = false;
    }

    // Update is called once per frame
    void Start()
    {
        baker = GameObject.Find("EventSystem").GetComponent<NavBaker>();
    }
}
