using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [HideInInspector]
    public bool gamePlaying;

    [SerializeField] GameObject tile;

    [SerializeField] GameObject rot;

    private Vector3 currentTilePos;

    private AudioSource _audioSource;

    
    //измен цвета
    [SerializeField] private Material tileMat;

    [SerializeField] private Light dayLight;

    private Camera mainCamera;

    private bool camColorLerp;

    private Color camaraColor;

    private Color[] tileColorDay;
    private Color tileColorNight;
    private int tileColorIndex;

    private Color tileTrueColor;

    private float timer;
    private float timerInterval = 5f;//как чатсо цвет мен

    private float canLerpTimer;
    private float camLerpinterval = 1f;

    private int direction=1;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentTilePos = new Vector3(-2, 0, 3);
        
        _audioSource = GetComponent<AudioSource>();
        
        mainCamera=Camera.main;

        camaraColor = mainCamera.backgroundColor;

        tileTrueColor = tileMat.color;

        tileColorIndex = 0;
        tileColorDay = new Color[3];
        tileColorDay[0] = new Color(10 / 256f, 139 / 256f, 203 / 256f);
        tileColorDay[1] = new Color(10 / 256f, 200 / 256f, 20 / 256f);
        tileColorDay[2] = new Color(220/256f, 170/ 256f, 45 / 256f);
        tileColorNight = new Color(0, 8 / 256f, 11 / 256f);
        
        tileMat.color = tileColorDay[0];
    }

    private void OnDisable()
    {
        tileMat.color = tileTrueColor;
    }

    private void Update()
    {
        CheckLerpTimer();
    }

    void CheckLerpTimer()
    {
        timer += Time.deltaTime;

        if (timer > timerInterval)
        {
            timer -= timerInterval;
            camColorLerp = true;
            canLerpTimer = 0f;
        }

        if (camColorLerp)
        {
            canLerpTimer += Time.deltaTime;
            float percent = canLerpTimer / camLerpinterval;

            if (direction == 1)
            {
                mainCamera.backgroundColor = Color.Lerp(camaraColor, Color.black, percent);
                tileMat.color = Color.Lerp(tileColorDay[tileColorIndex], tileColorNight, percent);
                dayLight.intensity = 1f - percent;
                
            }
            else
            {
                mainCamera.backgroundColor = Color.Lerp(Color.black, camaraColor, percent);
                tileMat.color = Color.Lerp(tileColorNight, tileColorDay[tileColorIndex], percent);
                dayLight.intensity = percent;
            }

            if (percent > 0.98f)
            {
                canLerpTimer = 1f;
                direction *= -1;
                camColorLerp = false;

                if (direction == -1)
                {
                    tileColorIndex = Random.Range(0, tileColorDay.Length);
                }
            }
        }
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
        yield return new WaitForSeconds(0.13f);
        
        while (gamePlaying)
        {
            CreateTiles();

            yield return new WaitForSeconds(0.13f);
        }
    }

    public void ActivateSpawn()
    {
        StartCoroutine(nameof(SpawnTiles));
    }

   
}
