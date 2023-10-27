using UnityEngine;

public class Client : MonoBehaviour
{
    private ClientType _type;
    public void setType(ClientType t) { _type = t; }
    public ClientType getType() { return _type;}
    private float chanceToDrinkMore = 0.5f; //0<= X <= 1
    private void setChanceToDrinkMore(float p) { chanceToDrinkMore = p;}
    public int beerCount = 0;
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

    public bool wantMore()
    {
        return (Random.Range(0f,1f) < chanceToDrinkMore);
    }
}
