using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectToSpawn;
    public Transform spawnPoint;    
    public float spawnInterval = 2f; 
    public int value;
    public int direction;
    private bool spawning = true;
    public List<GameObject> spawnedNPC;
    public int npcAmount = 1;

    void Start()
    {
        StartCoroutine(SpawnNPC());
    }

    IEnumerator SpawnNPC()
    {
        yield return new WaitForSeconds(spawnInterval);

        if(spawnedNPC.Count < npcAmount)
        {
            
            value = Random.Range(0, 5);
            
            switch(value)
            { 
                case 0:
                    spawnedNPC.Add(Instantiate(objectToSpawn[value], spawnPoint.position, spawnPoint.rotation));
                    yield return new WaitForSeconds(spawnInterval);
                    break;
                case 1:
                    spawnedNPC.Add(Instantiate(objectToSpawn[value], spawnPoint.position, spawnPoint.rotation));
                    yield return new WaitForSeconds(spawnInterval);
                    break;
                case 2:
                    spawnedNPC.Add(Instantiate(objectToSpawn[value], spawnPoint.position, spawnPoint.rotation));
                    yield return new WaitForSeconds(spawnInterval);
                    break;
                case 3:
                    spawnedNPC.Add(Instantiate(objectToSpawn[value], spawnPoint.position, spawnPoint.rotation));
                    yield return new WaitForSeconds(spawnInterval);
                    break;
                case 4:
                    spawnedNPC.Add(Instantiate(objectToSpawn[value], spawnPoint.position, spawnPoint.rotation));
                    yield return new WaitForSeconds(spawnInterval);
                    break;
                default:
                    Debug.Log(value);
                    Debug.Log("XDDDDDD"+value);
                break;
            }
            
            
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
