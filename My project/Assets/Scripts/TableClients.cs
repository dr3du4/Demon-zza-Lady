using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableClients : MonoBehaviour
{
    [SerializeField]private int clientsCount = 0;
    public bool active = true;
    public List<Client> clients = new List<Client>();
    [SerializeField]private List<int> sits = new List<int>();
    private List<Vector3> positionSits = new List<Vector3>();
    // (1,1) (1,-1) (-1,-1) (-1,1)
    private Dictionary<Vector3, int> sitPosOrderInLayer = new Dictionary<Vector3, int>();
    private Vector3 pos;

    private void Start(){
        RestartTable();
    }


    // Fix, because the table was inactive after the day is over, later on probably create a function "DayOver" or smth to setup the table again
    private void Update()
    {
        if (clientsCount < 4 && !active)
            active = true;
    }


    public KeyValuePair<Vector3, int> AddClient(Client new_c) {
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
        if (!new_c.dayOver)
        {
            Debug.Log("i'm in table clients!");
            Debug.Log(new_c);
            StartCoroutine(new_c.Drink());
        }
        if (clientsCount > 3) {
            active = false;
        }
        //Daj pozycje dla klienta  positionSits[sits[0]]
        return new KeyValuePair<Vector3, int>(positionSits[new_c.sit], sitPosOrderInLayer[positionSits[new_c.sit]]);
    }

    public void TakeClient(Client c) {
        Debug.Log("PUT OUT");
        clients.Remove(c);
        clientsCount--;
        sits.Add(c.sit);
        active = true;
    }

    public bool IsClient(Client c) {
        return clients.Contains(c);
    }

    public void RestartTable()
    {
        sits = new List<int>() {0, 1, 2, 3};
        sitPosOrderInLayer = new Dictionary<Vector3, int>();
        pos = transform.position;
        positionSits.Add(pos + new Vector3(1, 1, 0));
        positionSits.Add(pos + new Vector3(1, -1, 0));
        positionSits.Add(pos + new Vector3(-1, -1, 0));
        positionSits.Add(pos + new Vector3(-1, 1, 0));

        sitPosOrderInLayer.Add(pos + new Vector3(1, 1, 0), 1);
        sitPosOrderInLayer.Add(pos + new Vector3(-1, 1, 0), 1);
        sitPosOrderInLayer.Add(pos + new Vector3(-1, -1, 0), 4);
        sitPosOrderInLayer.Add(pos + new Vector3(1, -1, 0), 4);
        
        clients = new List<Client>();
        clientsCount = 0;
        active = true;
    }
}
