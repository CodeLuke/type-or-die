using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    }
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public AudioClip enemyDeathSound;
    
    public Text waveText;

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;
    
    public float timeBetweenWaves = 5f;
    private float waveCountdown = 0;

    private float searchCountdown = 1f;
    private float playerSearchCountdown = 10f;

    private SpawnState state = SpawnState.COUNTING;

    public Sprite spriteA;
    public Sprite spriteC;
    public Sprite spriteL;
    public Sprite spriteM;
    public Sprite spriteN;
    public Sprite spriteP;
    public Sprite spriteQ;
    public Sprite spriteR;
    public Sprite spriteU;
    public Sprite spriteZ;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        
        
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points referenced");
        }

    }

    private void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (! EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (!PlayerIsAlive())
        {
            Destroy(gameObject);
        }
        
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete. Looping");
        }
        else
        {
            nextWave++;
        }
        
        
    }

    bool EnemyIsAlive()
    {

        searchCountdown -= Time.deltaTime;
        
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    private bool PlayerIsAlive()
    {
        playerSearchCountdown -= Time.deltaTime;
        
        if (playerSearchCountdown <= 0)
        {
            playerSearchCountdown = 10f;
            if (GameObject.FindGameObjectWithTag("Player") == null || GameObject.FindWithTag("NPC") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        waveText.text = wave.name;
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }
        
        state = SpawnState.WAITING;
        
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        _enemy.GetComponent<Enemy>().button = _sp.gameObject.name.ToLower();
        _enemy.GetComponent<Enemy>().scoreManager =
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        _enemy.GetComponent<Enemy>().deathSource =
            GameObject.FindGameObjectWithTag("EnemyDeathAudioSource").GetComponent<AudioSource>();
        _enemy.GetComponent<Enemy>().deathClip = enemyDeathSound;
        Sprite keySprite = spriteA;
        switch (_sp.gameObject.name.ToLower())
        {
            case "a":
                keySprite = spriteA;
                break;
            case "c":
                keySprite = spriteC;
                break;
            case "l":
                keySprite = spriteL;
                break;
            case "m":
                keySprite = spriteM;
                break;
            case "n":
                keySprite = spriteN;
                break;
            case "p":
                keySprite = spriteP;
                break;
            case "q":
                keySprite = spriteQ;
                break;
            case "r":
                keySprite = spriteR;
                break;
            case "u":
                keySprite = spriteU;
                break;
            case "z":
                keySprite = spriteZ;
                break;
        }

        _enemy.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = keySprite;
        Instantiate(_enemy, _sp.position, _sp.rotation);
        Debug.Log(_enemy.GetComponent<Enemy>().button);
        
        
    }
    
}
