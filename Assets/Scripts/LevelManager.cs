using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public GameObject[] obstaclePrefabs;
    public GameObject groundPrefab;


    private float groundLevel = -0.01f;
    private float maximumHeight = 30f;
    public float deathHeight = -1f;
    private float delta = 0.01f;
    public Transform playerTransform;
    private Vector3 spawnPos = -30.0f * Vector3.forward;
    private float tileLength = 120f;
    //private float safeZone = 200f;

    private int difficulty = 0;
    private int[] diffLength = { 120, 90 , 80, 75, 70, 65, 60, 55, 50 , 45, 40 };
    private float renderDistance = 1000f;
    private int lastPrefabIndex = 0;

    private int jumpTileDelay = 3;
    private int prevJump = 0; 

    private List<GameObject> activeTiles;

	// Use this for initialization
    void Awake()
    {
        instance = this;
    }
	void Start () {
        activeTiles = new List<GameObject>();
        //activeGrounds = new List<GameObject>();
        playerTransform = PlayerMovement.instance.transform;
        for(int i = 0; i < 5;i++)
        {
            SpawnGround();
            SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (playerTransform.position.z > (spawnPos.z - renderDistance))
        {
            UpdateDifficulty();
            SpawnGround();
            SpawnTile();
        }
        if(playerTransform.position.z > activeTiles[2].transform.position.z)
        {
            DeleteTile();
        }
	}

    private void SpawnTile(int prefabIndex = -1)
    {

        //Create obstacle
        GameObject go;
        if (prefabIndex == -1)
        {
            int rndIndex = 0;
            bool isValid = false;
            while(isValid == false)
            {
                rndIndex = Random.Range(0, obstaclePrefabs.Length);
                isValid = checkPrefabValidity(rndIndex);            
            }
            GameObject pref = obstaclePrefabs[rndIndex];   
            prefabIndex = rndIndex;

        }
        go = Instantiate(obstaclePrefabs[prefabIndex]) as GameObject;

        ObstacleProperties ob = go.GetComponent<ObstacleProperties>();
        prevJump = ob.isJump ? prevJump = 0 : prevJump + 1;
        lastPrefabIndex = prefabIndex;

        go.transform.position = spawnPos;
        spawnPos += ob.newCentre;
        activeTiles.Add(go);

        AdjustForTileBug();

    }
    private void SpawnGround(float length = -1.0f)
    {
        
        GameObject go;
        go = Instantiate(groundPrefab) as GameObject;
        Vector3 groundScale = go.transform.localScale;
        if (length == -1.0f) {
            groundScale.z = tileLength;
        }
        else
        {
            groundScale.z = length;
        }    
        go.transform.localScale = groundScale;
        spawnPos.z += groundScale.z / 2;
        go.transform.position = spawnPos;
        spawnPos.z += groundScale.z / 2;
        //tileLength *= Mathf.Pow(0.99f, (difficulty + 1)); // Increase difficulty
        tileLength = difficulty < diffLength.Length ? diffLength[difficulty] : diffLength[diffLength.Length - 1];
        activeTiles.Add(go);

        AdjustForTileBug();

    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

       // Destroy(activeGrounds[0]);
        //activeGrounds.RemoveAt(0);
    }
    private bool checkPrefabValidity(int index)
    {
        if (index == lastPrefabIndex) return false;

        GameObject tempGo = obstaclePrefabs[index];
        ObstacleProperties ob = tempGo.GetComponent<ObstacleProperties>();

        
        if (ob.isJump && prevJump < jumpTileDelay) return false;
        if (!ob.isJumpable && prevJump == 0) return false;
        if (ob.difficulty > difficulty) return false;
        if (spawnPos.y + ob.newCentre.y < groundLevel) return false;
       // if (spawnPos.y + ob.newCentre.y > maximumHeight) return false;
        return true;
    }
    private void UpdateDifficulty()
    {
        if (spawnPos.z > (1 + difficulty) * 1000f)
        {
            difficulty++;
        }
    }
    private void AdjustForTileBug()
    {
        groundLevel -= delta;
        deathHeight -= delta;
        spawnPos.y -= delta;
        maximumHeight -= delta;
    }
}
