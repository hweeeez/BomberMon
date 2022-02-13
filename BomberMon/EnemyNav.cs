using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    //public GameObject bombSpawn;
    public AudioSource explodems;
    public Tilemap tilemap;
    public GameObject bombPrefab;
    private Pickup picksc;
    public BombEx bS;
    private void Awake()
    {
        picksc = GameObject.Find("Player").GetComponent<Pickup>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        InvokeRepeating("dropBomb", 2.0f, 4.0f);
        //GameObject[] bombsAlive = GameObject.FindGameObjectsWithTag("bomb");
    }

    private void Update()
    {

        navMeshAgent.destination = movePositionTransform.position;

    }

    public void dropBomb()
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 worldPos = transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);

    }




}
