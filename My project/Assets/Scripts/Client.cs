using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour
{
    public clientTypeSO clientPreference; // Probably merge with ClientType? Store that information in one place
    public ClientType _type;
    private float chanceToDrinkMore = 0.5f; //0<= X <= 1
    public int beerCount = 0;
    public TableClients table = null;
    public int sit = -1;
    public float moveSpeed = 3f;
    public float timeToWait = 15f;
    public void setType(ClientType t) { _type = t; }
    public ClientType getType() { return _type;}
    public void setChanceToDrinkMore(float p) { chanceToDrinkMore = p;}

    public bool waiting = false;

    public bool readyToDrink = true;
    private SpriteRenderer _render;
    public bool goingUp = false;
    [SerializeField]private Sprite upSprite;
    [SerializeField]private Sprite downSprite;
    private void Start(){
        _render = GetComponent<SpriteRenderer>();
        RandTimeToWait();
    }

    private void Update(){
        if (goingUp) _render.sprite = upSprite;
        else _render.sprite = downSprite;
    }

    public void RandTimeToWait(){
        timeToWait = Random.Range(2f, 5f);
    }

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

        table.TakeClient(this.gameObject.GetComponent<Client>());
        sit = -1;
        table = null;
        readyToDrink = wantMore();
        //Iść i zdecydować co dalej
        if (readyToDrink) {
            GameObject barGameObject = GameObject.FindWithTag("Bar");
            BarQueue bar = barGameObject.GetComponent<BarQueue>();
            bar.ForceClient(this);
        } else {
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
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}

    public IEnumerator Die() {
        float progress = 0f;
		Vector3 start = transform.position;
        Vector3 target = transform.position + new Vector3(1,0,0);
        goingUp = false;
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += (float)(moveSpeed * Time.deltaTime / 0.5);
			yield return new WaitForSeconds(Time.deltaTime);
		}
        progress = 0f;
		start = target;
        target += transform.position - new Vector3(0.5f,15,0);	
		while (progress < 1f) {
			transform.position = Vector3.Lerp(start,target,progress);
			progress += (float)(moveSpeed * Time.deltaTime / 5.5);
			yield return new WaitForSeconds(Time.deltaTime);
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
			yield return new WaitForSeconds(Time.deltaTime);
		}
        progress = 0f;
		start = target;
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
        StartCoroutine(WaitInQueue(bar));
    }

    private IEnumerator WaitInQueue(BarQueue bar){
        while(timeToWait > 0f && waiting){
            timeToWait -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ShowIndicatorSquare(false);
        // Jeśli wybierzesz stół daj waiting FALSE
        bar.RemoveFirstClient();
        if (waiting) {
            StartCoroutine(Die());
            waiting = false;
        }
    }

    public void ShowIndicatorSquare(bool show){
        GameObject p = transform.Find("Preferences").gameObject;
        p.SetActive(show);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered a trigger");
        if(collision.gameObject.GetComponent<beerPromptSystem>() && readyToDrink)
        {
            readyToDrink = false;
            collision.gameObject.GetComponent<beerPromptSystem>().InitPrompt(this);
        }
    }
}
