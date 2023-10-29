using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsSpawn : MonoBehaviour
{
    [SerializeField] BarQueue bar;
    [SerializeField] List<clientTypeSO> clientTypes = new List<clientTypeSO>();
    [SerializeField] GameObject clientPrefab;

    float timer = 5.0f;
    float delayMax = 15f;
    private void Update() {
        //if (Input.GetKeyDown("space")) SpawnClient(Random.Range(0,clientTypes.Count));
        if (Time.time > timer)
        {
            SpawnClient(Random.Range(0, clientTypes.Count));
            timer += Random.Range(3f, delayMax);
        }
        }
        private void SpawnClient(int i){
        Client c = Instantiate(clientPrefab,transform.position, transform.rotation).GetComponent<Client>();
        c.setType(clientTypes[i]);
        bar.AddClient(c);
    }
}
