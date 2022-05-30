using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fallingObjects;
    public GameObject player;
    private float spawnPosX;
    public float spawnPosY = 300;
    private float spawnLimitX = 13.5f;
    private Vector3 spawnPosition;
    private int objectToSpawn;
    private float randomSpawnNumber;
    public float spawnRate;
    public float dropChance;
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public TextMeshProUGUI scoreText;
    public int score;
    public GameObject helpText;
    public GameObject gameOver;
    private int startPos;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnObjects", 0.5f, spawnRate);
        StartCoroutine(SpawnObjects());
        StartCoroutine(SpawnBadObjects());
        LoadObjects(20);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObjects()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            spawnPosX = Random.Range(-spawnLimitX, spawnLimitX);
            spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
            objectToSpawn = 0;
            
            // If Player doesn't exists, return
            if (GameObject.Find("Player") == null)
            {
                break;
            }

            if (player.transform.position.y > (spawnPosY - 80))
            {   
                startPos = Mathf.FloorToInt(spawnPosY);
                spawnPosY += 500;
                LoadObjects(startPos);
            } 
            else 
            {
                Instantiate(fallingObjects[objectToSpawn], spawnPosition, fallingObjects[objectToSpawn].transform.rotation);
            }
        }
    }

    IEnumerator SpawnBadObjects()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            spawnPosX = Random.Range(-spawnLimitX, spawnLimitX);
            spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);

            randomSpawnNumber = Random.Range(0f, 100f);

            if (randomSpawnNumber <= dropChance)
            {
                Instantiate(fallingObjects[1], spawnPosition, fallingObjects[1].transform.rotation);
            }
        }
        
    }

    public void SpawnObject(float positionY)
    {
        spawnPosX = Random.Range(-spawnLimitX, spawnLimitX);
        spawnPosition = new Vector3(spawnPosX, positionY, 0);
        Instantiate(fallingObjects[0], spawnPosition, fallingObjects[0].transform.rotation);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        helpText.SetActive(false);
        isGameStarted = true;
    }

    public void LoadObjects(int start)
    {
        for (int i = start; i < spawnPosY; i += 10)
        {

            SpawnObject(i);
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
