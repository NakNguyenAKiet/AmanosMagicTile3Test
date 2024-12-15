using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPositions;
    [SerializeField] GameObject spawnPosition;
    public Tile tilePrefab;            // Tile Prefab
    public float spawnIntervalMin = 1f;   // Time between tile spawns
    public float spawnIntervalMax = 2f;   // Time between tile spawns
    public int poolSize = 10;          // Size of the object pool

    private List<Tile> tilePool;      // Pool of reusable tiles
    private Coroutine spawnCO;         // Reference to the spawn coroutine

    private Transform currentPos;
    private void Start()
    {
        currentPos = spawnPositions[0];
        InitializePool();
        MyGameEvent.Instance.OnEndGame += StopSpawn;
        spawnPosition.SetActive(false);
    }

    // Initialize the tile pool
    private void InitializePool()
    {
        tilePool = new ();

        for (int i = 0; i < poolSize; i++)
        {
            Tile tile = Instantiate(tilePrefab, transform);
            tile.gameObject.SetActive(false);  // Deactivate tile initially
            tilePool.Add(tile);           // Add to the pool
        }
    }

    // Start spawning tiles
    public void StartSpawn()
    {
        if (spawnCO != null)
        {
            StopCoroutine(spawnCO);
        }
        spawnPosition.SetActive(true);
        spawnCO = StartCoroutine(SpawnTiles());
    }

    // Spawn tiles at intervals
    private IEnumerator SpawnTiles()
    {
        //wait to unity init gameobjects position
        yield return new WaitForSeconds(spawnIntervalMin);

        while (true)
        {
            //Random double spawn tile with 30% rate
            bool isDouble = Random.Range(0, 9) > 5;

            if (isDouble)
            {
                SpawnNomalTile();
            }
            SpawnNomalTile();

            float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawn or reuse a tile from the pool
    private void SpawnNomalTile()
    {
        Tile tile = GetTile();  // Get a tile from the pool

        int randomIndex = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[randomIndex].position;

        while(currentPos.position == spawnPos)
        {
            randomIndex = Random.Range(0, spawnPositions.Count);
            spawnPos = spawnPositions[randomIndex].position;
        }
        currentPos = spawnPositions[randomIndex];

        Debug.Log("::: Spawn at Pos: " + randomIndex);

        tile.transform.position = spawnPos;  // Set tile position

        tile.gameObject.SetActive(true);     // Activate the tile
        tile.OnTitleReturn += ReturnTileToPool;
        tile.AllowToMove(true);              // Enable its movement
    }

    Tile GetTile()
    {
        Tile tile = tilePool.Find(x => !x.gameObject.activeSelf);
        if (tile != null)
        {
            return tile;
        }
        tile = Instantiate(tilePrefab, transform);
        tile.gameObject.SetActive(false);  // Deactivate tile initially
        tilePool.Add(tile);

        return tile;
    }

    // Return a tile back to the pool
    public void ReturnTileToPool(Tile tile)
    {
        tile.gameObject.SetActive(false);  // Deactivate tile
    }

    void StopSpawn()
    {
        if (spawnCO != null)
        {
            StopCoroutine(spawnCO);
            spawnPosition.SetActive(false);
        }
    }
}
