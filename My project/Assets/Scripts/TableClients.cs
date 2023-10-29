using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableClients : MonoBehaviour
{
    private int clientsCount = 0;
    public bool active = true;
    public List<Client> clients = new List<Client>();
    private List<int> sits = new List<int>();
    private List<Vector3> positionSits = new List<Vector3>();
    // (1,1) (1,-1) (-1,-1) (-1,1)
    private Vector3 pos;

    private void Start(){
        pos = transform.position;
        for (int i = 0; i < 4; i++) {
            sits.Add(i);
        }
        positionSits.Add(pos + new Vector3(1, 1, 0));
        positionSits.Add(pos + new Vector3(1, -1, 0));
        positionSits.Add(pos + new Vector3(-1, -1, 0));
        positionSits.Add(pos + new Vector3(-1, 1, 0));
    }

    public Vector3 AddClient(Client new_c) {
        Debug.Log("PUT IN");
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
        if (clientsCount > 3) {
            active = false;
        }
        //Daj pozycje dla klienta  positionSits[sits[0]]
        return positionSits[new_c.sit];
    }

    public void TakeClient(Client c) {
        Debug.Log("PUT OUT");
        clients.Remove(c);
        clientsCount--;
        sits.Add(c.sit);
        if (clientsCount < 4) {
            active = true;
        }
    }

    public bool IsClient(Client c) {
        return clients.Contains(c);
    }
}
