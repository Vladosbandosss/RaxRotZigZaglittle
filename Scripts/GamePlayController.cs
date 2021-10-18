using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [HideInInspector]
    public bool gamePlaying;

    [SerializeField] GameObject tile;

    [SerializeField] GameObject rot;

    private Vector3 currentTilePos;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentTilePos = new Vector3(-2, 0, 3);

        
    }

    private void Start()
    {
        
    }

    void CreateTiles()
    {
        Vector3 newTilePos = currentTilePos;
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            newTilePos.x -= 1f;
        }
        else
        {
            newTilePos.z += 1f;
        }

        currentTilePos = newTilePos;
        Instantiate(tile, currentTilePos, Quaternion.identity);

        int randTocreate = Random.Range(0, 10);
        if (randTocreate > 8)
        {
            Vector3 rotPos = new Vector3(currentTilePos.x, rot.transform.position.y, currentTilePos.z);
            Instantiate(rot, rotPos, rot.transform.rotation);
        }
        
    }

    IEnumerator SpawnTiles()
    {
        yield return new WaitForSeconds(0.2f);
        
        while (gamePlaying)
        {
            CreateTiles();

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ActivateSpawn()
    {
        StartCoroutine(nameof(SpawnTiles));
    }

   
}
