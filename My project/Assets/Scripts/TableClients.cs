using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableClients : MonoBehaviour
{
    private int clientsCount = 0;
    public bool active = true;
    private List<Client> clients = new List<Client>();
    private List<int> sits = new List<int>();
    private List<Vector3> positionSits = new List<Vector3>();
    // (1,1) (1,-1) (-1,-1) (-1,1)
    private Vector3 pos;

    private void Start(){
        for (int i = 0; i < 4; i++) {
            sits.Add(i);
        }
        positionSits.Add(pos + new Vector3(1, 1, 0));
        positionSits.Add(pos + new Vector3(1, -1, 0));
        positionSits.Add(pos + new Vector3(-1, -1, 0));
        positionSits.Add(pos + new Vector3(-1, 1, 0));
        pos = transform.position;
    }

    public void AddClient(Client new_c) {
        foreach (Client c in clients){
            new_c.ReviewNewClient(c.getType());
            c.ReviewNewClient(new_c.getType());
        }
        clients.Add(new_c);
        clientsCount++;
        new_c.table = this;
        new_c.sit = sits[0];
        //Daj pozycje dla klienta  positionSits[sits[0]]
        sits.RemoveAt(0);
        StartCoroutine(new_c.Drink());
        if (clientsCount > 4) {
            active = false;
        }
    }

    public void TakeClient(Client c) {
        clients.Remove(c);
        sits.Add(c.sit);
        c.sit = -1;
        if (clientsCount < 4) {
            active = true;
        }
    }
}
