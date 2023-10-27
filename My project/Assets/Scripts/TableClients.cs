using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableClients : MonoBehaviour
{
    private int clientsCount = 0;
    public bool active = true;
    private List<Client> clients = new List<Client>();
    
    public void AddClient(Client new_c) {
        foreach (Client c in clients){
            new_c.ReviewNewClient(c.getType());
            c.ReviewNewClient(new_c.getType());
        }
        clients.Add(new_c);
        clientsCount++;
        new_c.table = this;
        StartCoroutine(new_c.Drink());
        if (clientsCount > 4) {
            active = false;
        }
    }

    public void TakeClient(Client c) {
        clients.Remove(c);
        if (clientsCount < 4) {
            active = true;
        }
    }
}
