using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Transform targetImage;
    public Transform enemy;
    public double enemyNum = 1;
    public float waveInterval = 4f;

    private float countdown = 2f;
    private float timeBetweenEnemies = .5f;
    private Vector3 resize = new Vector3 (10, 10, 10);

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;

    public bool spawn2Active = false;
    public bool spawn3Active = false;
    public bool spawn4Active = false;

    // Update is called once per frame
    void Update () {
        if (countdown <= 0f) {
            StartCoroutine (SpawnWave ());
            countdown = waveInterval;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave () {
        enemyNum *= 1.5;
        waveInterval += 1f;

        for (int i = 0; i < enemyNum - 1; i++) {
            SpawnEnemy ();
            yield return new WaitForSeconds (timeBetweenEnemies);
        }

        if (enemyNum > 3  && !spawn4Active) {
            spawn4Active = true;
            enemyNum *= 0.5;
        }

        if (enemyNum > 8 && !spawn3Active) {
            spawn3Active = true;
            enemyNum *= 0.2;
        }

        if (enemyNum > 10 && !spawn2Active) {
            spawn2Active = true;
            enemyNum *= 0.2;
        }
    }

    void SpawnEnemy () {
        Transform createdEnemy;
        createdEnemy =
            Instantiate (enemy, spawnPoint1.position, spawnPoint1.rotation, targetImage);
        createdEnemy.localScale = resize;

        if (spawn2Active) {
            createdEnemy =
                Instantiate (enemy, spawnPoint2.position, spawnPoint2.rotation, targetImage);
            createdEnemy.localScale = resize;
        }

        if (spawn3Active) {
            createdEnemy =
                Instantiate (enemy, spawnPoint3.position, spawnPoint3.rotation, targetImage);
            createdEnemy.localScale = resize;
        }

        if (spawn4Active) {
            createdEnemy =
                Instantiate (enemy, spawnPoint4.position, spawnPoint4.rotation, targetImage);
            createdEnemy.localScale = resize;
        }
    }
}