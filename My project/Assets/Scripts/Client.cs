using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour
{
    private ClientType _type;
    private float chanceToDrinkMore = 0.5f; //0<= X <= 1
    public int beerCount = 0;
    public TableClients table = null;
    public int sit = -1;
    public void setType(ClientType t) { _type = t; }
    public ClientType getType() { return _type;}
    public void setChanceToDrinkMore(float p) { chanceToDrinkMore = p;}
    public void ReviewNewClient(ClientType new_client) {
        if (Mathf.Abs(_type - new_client) < 2) {
            chanceToDrinkMore += 0.2f / (beerCount + 1);
        }
        else {
            chanceToDrinkMore -= 0.2f * (beerCount + 1);
        }

        if (chanceToDrinkMore < 0f) chanceToDrinkMore = 0f;
        if (chanceToDrinkMore > 1f) chanceToDrinkMore = 1f;
    }

    public bool wantMore() {
        return (Random.Range(0f,1f) < chanceToDrinkMore);
    }

    public IEnumerator Drink() {
        float time = Random.Range(20f, 40f);
        while (time > 0f){
			time -= Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
			//Dodać animacje pasek
		}
        table.TakeClient(this);
        sit = -1;
		beerCount++;
        table = null;
		//Iść i zdecydować co dalej
    }
}
