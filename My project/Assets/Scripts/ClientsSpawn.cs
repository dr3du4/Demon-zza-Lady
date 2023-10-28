using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsSpawn : MonoBehaviour
{
    [SerializeField] BarQueue bar;
    [SerializeField] List<GameObject> clientPrefabs = new List<GameObject>();
    private void Update() {
        if (Input.GetKeyDown("space")) SpawnClient(Random.Range(0,clientPrefabs.Count));
    }
    private void SpawnClient(int i){
        Client c = Instantiate(clientPrefabs[i],transform.position, transform.rotation).GetComponent<Client>();
        bar.AddClient(c);
    }
}
