using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;


public class Client : MonoBehaviour
{
    public clientTypeSO _type; // Probably merge with ClientType? Store that information in one place
    // public ClientType _type;
    private float chanceToDrinkMore = 0.5f; //0<= X <= 1
    public int beerCount = 0;
    public TableClients table = null;
    public int sit = -1;
    public float moveSpeed = 3f;
    public float timeToWait = 15f;
    public void setType(clientTypeSO t) { _type = t; }
    public clientTypeSO getType() { return _type;}
    public void setChanceToDrinkMore(float p) { chanceToDrinkMore = p;}

    public bool waiting = false;

    public bool readyToDrink = false;
    private SpriteRenderer _render;
    public bool goingUp = false;
    public MoveToClick mtc;
    private List<clientTypeSO.ClientType> companyPreferences;
    public bool staryWstal = false;
    public preferenceHelper preferenceHelper;
    public GameObject pintOfBeer;

    // Bool that is set right before we kill everyone when the day is over (so that no other object tries to do a coroutine on our guys)
    public bool dayOver = false;


    private void Start(){
        _render = GetComponent<SpriteRenderer>();
        RandTimeToWait();
        companyPreferences = _type.companyPreference;
    }

    

    private void Update(){
        if (goingUp) _render.sprite = _type.upSprite;
        else _render.sprite = _type.downSprite;
    }

    public void RandTimeToWait(){
        timeToWait = Random.Range(2f, 5f);
    }

    public void ReviewNewClient(clientTypeSO new_client) {
        
        
        if (_type.companyPreference.Contains(new_client.clientType)) {
            if (table)
                table.gameObject.GetComponent<TipController>().spawnTip(Random.Range(5, 10));
            chanceToDrinkMore += 0.2f / (beerCount + 1);
        }
        else {
            chanceToDrinkMore -= 0.2f * (beerCount + 1);
        }

        chanceToDrinkMore = Mathf.Clamp01(chanceToDrinkMore);
    }


    public bool wantMore() {
        return (Random.Range(0f,1f) < chanceToDrinkMore);
    }

    public IEnumerator Drink() {
        float time = Random.Range(20f, 40f);
        while (time > 0f){
			time -= Time.deltaTime;
            yield return null;
			//Dodać animacje pasek
		}
        if (dayOver) // SPRAWDZIĆ KTO DO CHUJA ODPALA TĄ KURUTYNĘ
            yield break;
        Transform cObj = transform.Find("Beer");
        cObj.gameObject.SetActive(false);
        table.TakeClient(this.gameObject.GetComponent<Client>());
        table = null;
        readyToDrink = wantMore();
        
        //Iść i zdecydować co dalej
        if(mtc)
            mtc.reach = false;
        staryWstal = true;
        if (readyToDrink) {
            GameObject barGameObject = GameObject.FindWithTag("Bar");
            BarQueue bar = barGameObject.GetComponent<BarQueue>();
            bar.ForceClient(this);
        } else {
            if(!dayOver)
                StartCoroutine(GoOut());
        }

    }

	public IEnumerator MoveTo(Vector3 target){
		float progress = 0f;
		Vector3 start = transform.position;
		float dis = Vector3.Distance(start,target);	
        if (target.y > start.y) goingUp = true;
        else goingUp = false;	
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += moveSpeed * Time.deltaTime / dis;
            yield return null;
		}
	}

    public IEnumerator Die() {
        if (preferenceHelper.PreferencesActive())
            preferenceHelper.HidePreferences();

        if (!dayOver && beerCount <= 1 && sit == -1) {
            GameObject mg = GameObject.FindWithTag("GameController");
            GameManager managerG  = mg.GetComponent<GameManager>();
            if (managerG.klienciCoS == 0) managerG.GetTutorial().ActivateTutorial(5);
            managerG.klienciCoS++;
        }
        float progress = 0f;
		Vector3 start = transform.position;
        Vector3 target = transform.position;
        target.x = -1;
        goingUp = false;
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += (float)(moveSpeed * Time.deltaTime / 0.5);
            yield return null;
		}
        progress = 0f;
		start = target;
        target += transform.position - new Vector3(0.5f,15,0);	
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += (float)(moveSpeed * Time.deltaTime / 5.5);
			yield return null;
		}
        Destroy(this.gameObject);
    }

    public IEnumerator GoOut() {
        float progress = 0f;
		Vector3 start = transform.position;
        Vector3 target = transform.position;
        target.x = -1;
        goingUp = false;
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += (float)(moveSpeed * Time.deltaTime / 1.5);
            yield return null;
		}
        progress = 0f;
		start = target;
        pintOfBeer.SetActive(false);
        target += transform.position - new Vector3(0.5f,15,0);	
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += (float)(moveSpeed * Time.deltaTime / 5.5);
			yield return new WaitForSeconds(Time.deltaTime);
		}
        Destroy(this.gameObject);
    }

    public void StartTimer(BarQueue bar) {
        waiting = true;
        if(!dayOver)
            StartCoroutine(WaitInQueue(bar));
    }

    private IEnumerator WaitInQueue(BarQueue bar){
        readyToDrink = true;
        while(timeToWait > 0f && waiting){
            timeToWait -= Time.deltaTime;
            yield return null;
        }
        ShowIndicatorSquare(false);
        
        // Jeśli wybierzesz stół daj waiting FALSE
        bar.RemoveFirstClient();
        if (waiting) {
            if(!dayOver)
                StartCoroutine(Die());
            waiting = false;
        }
    }


    public void ShowIndicatorSquare(bool show){
        //GameObject p = transform.Find("Preferences").gameObject;
        if (show)
        {
            ShufflePreference();
            preferenceHelper.ShowPreferences(companyPreferences);
        }
        else
            preferenceHelper.HidePreferences();
        //p.SetActive(show);
    }

    public void Soulless()
    {
        _render.color = new Color(0.2f, 0.2f, 0.2f);
    }

    private void ShufflePreference()
    {
        List<clientTypeSO.ClientType> types = new List<clientTypeSO.ClientType> { clientTypeSO.ClientType.FARMER, clientTypeSO.ClientType.RYCERZ, clientTypeSO.ClientType.SZLACHCIC };
        int i = Random.Range(0, 3);
        for (int j = 0; j < i; j++)
        {
            types.RemoveAt(Random.Range(0, types.Count));
        }
        companyPreferences = types;
    }

}
