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
        GameObject cObj = Instantiate(clientPrefabs[i],transform.position, transform.rotation);
        Client c = cObj.GetComponent<Client>();
        bar.AddClient(c);
    }
}
