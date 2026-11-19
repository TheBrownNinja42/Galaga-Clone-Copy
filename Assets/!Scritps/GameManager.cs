// Gives access to List
using System.Collections.Generic;
// Gives access to IEnumerator
using System.Collections;
// Gives access to Unity's types like MonoBehaviour
using UnityEngine;

// ": MonoBehaviour" makes this a Unity component that can sit on a GameObject
public class GameManager : MonoBehaviour
{
    // static: one Instance for the whole game, reached as GameManager.Instance
    // get: any script can read it; private set: only GameManager can set it
    public static GameManager Instance { get; private set; }

    // The player's ship in the scene
    [SerializeField] private Player player;
    // The enemy prefabs to pick from (Bee and Butterfly)
    [SerializeField] private Enemy[] enemyPrefabs;
    // Where each enemy in a wave appears
    [SerializeField] private Transform[] spawnPoints;
    // Seconds between enemy dives
    [SerializeField] private float diveInterval = 3f;
    // Seconds between each enemy appearing
    [SerializeField] private float spawnDelay = 0.3f;

    // Every enemy still alive; SpawnEnemy fills it, so it's no longer set in the Inspector
    private List<Enemy> enemies = new List<Enemy>();
    // Which wave we're on
    private int waveNumber;

    // Runs before any Start, so Instance is ready for every other script
    private void Awake()
    {
        // This object is the one GameManager
        Instance = this;
    }

    // Runs once, when the scene starts
    private void Start()
    {
        // Every game starts with a score of 0
        Score.ResetPoints();
        // Start the game flow; it runs for the rest of the level
        StartCoroutine(WaveLoop());
    }

    // The whole game flow: spawn a wave, send every enemy diving, repeat
    private IEnumerator WaveLoop()
    {
        // Keep going forever; this is safe ONLY because there's a yield inside
        while (true)
        {
            // Next wave
            waveNumber++;
            // Show it in the Console
            Debug.Log("Wave " + waveNumber);

            // Spawn the whole wave, and wait here until it's finished
            yield return StartCoroutine(SpawnWave());
            // Give the player a moment before the first dive
            yield return new WaitForSeconds(diveInterval);

            // Send divers until no one is left in formation
            while (enemies.Count > 0)
            {
                // Send one diving
                SendRandomDiver();
                // Wait before the next one
                yield return new WaitForSeconds(diveInterval);
            }
        }
    }

    // Puts one enemy on every spawn point
    private IEnumerator SpawnWave()
    {
        // Visit every spawn point in the array
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Make an enemy there
            SpawnEnemy(spawnPoint);
            // Pause before the next one, so they appear one after another
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Creates one random enemy at a spawn point and adds it to the list
    private void SpawnEnemy(Transform spawnPoint)
    {
        try
        {
            // Pick a random prefab: Bee or Butterfly
            Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            // Make a copy of it at the spawn point, with no rotation
            Enemy newEnemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            // Remember it, so it can be sent diving
            enemies.Add(newEnemy);
        }
        catch(System.Exception e)
        {
            Debug.Log("Couldn't Spawn the enemy, Check Game manager");
        }
    }

    // Picks one living enemy and starts its dive
    private void SendRandomDiver()
    {
        // No enemies left: nothing to send
        if (enemies.Count == 0)
        {
            return;
        }

        // A random position in the list, from 0 up to Count - 1
        int index = Random.Range(0, enemies.Count);
        // That enemy starts diving
        enemies[index].StartDive();
        // It's left the formation, so it can't be picked again
        enemies.RemoveAt(index);
    }

    // Lets enemies find the player without each one needing its own reference
    public Player GetPlayer()
    {
        // Hand back the player; Unity treats it as null once it's been destroyed
        return player;
    }

    // Called by an enemy as it dies, so it is never picked to dive again
    public void EnemyDestroyed(Enemy enemy)
    {
        // Take it out of the list
        enemies.Remove(enemy);
    }
}