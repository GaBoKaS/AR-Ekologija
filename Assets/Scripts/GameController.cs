using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score;
    public bool GameStarted;
    private string[] ItemTags = { "Glass", "Plastic", "Paper" };
    GameObject  UI;
    void Start()
    {
        Score = 0;
        HoldingSomething = false;
        UI = GameObject.FindGameObjectWithTag("UI");
        GameStarted = false;
    }
    void Update()
    { // Spawn Items stop time and change raycast direction for PC testing
        if (Input.GetKeyDown(KeyCode.W)) SpawnObjects();
        if (Input.GetKeyDown(KeyCode.R)) Time.timeScale = 0.0f;
        if (Input.GetKeyDown(KeyCode.T)) Time.timeScale = 1.0f;
        if (Input.GetKeyDown(KeyCode.E) && GameObject.FindGameObjectWithTag("GameController").GetComponent<Selection>().TestingMode)
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Selection>().TestingMode = false;
        else if (Input.GetKeyDown(KeyCode.E) && !GameObject.FindGameObjectWithTag("GameController").GetComponent<Selection>().TestingMode)
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Selection>().TestingMode = true;
        // Click for grab and throw 
        if (Input.GetKeyDown(KeyCode.Mouse0) && !( UI.GetComponent<Menu>().PlacingEnabled || UI.GetComponent<Menu>().MenuOpened ) )
            MouseControl();
        //check if placing mode is not enabled
    }
    public GameObject SpawnPoint;
    public GameObject[] PrefabList;
    public GameObject PrefabParent;
    public void SpawnObjects()
    { //spawn a prefab with a offset of spawnpoint
        float x = SpawnPoint.gameObject.transform.position.x + Random.Range(-1.5f, 1.5f);
        float y = SpawnPoint.gameObject.transform.position.y;
        float z = SpawnPoint.gameObject.transform.position.z + Random.Range(-1.5f, 1.5f);
        Vector3 offset = new Vector3(x,y,z);
        Instantiate(PrefabList[Random.Range(0, PrefabList.Length)], offset, Random.rotation);
        //Assign prefabs to a parent
        foreach(string itemTag1 in ItemTags)
        {
            GameObject[] item1 = GameObject.FindGameObjectsWithTag(itemTag1);
            foreach(GameObject item in item1)
            {
                if(item.transform.parent != PrefabParent)
                {
                    item.transform.parent = PrefabParent.transform;
                }
            }
        }
    }
    public void UpdateScore(int pointAmount)
    {
        if (pointAmount > 0)
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DisplayScore>().ChangeColor("Green");
        else GameObject.FindGameObjectWithTag("GameController").GetComponent<DisplayScore>().ChangeColor("Red");
        //positive score to add and negative to subtract
        Score += pointAmount;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<DisplayScore>().Score = Score;
    }
    // Actions with items -------------------------------------
    public GameObject itemHolder;
    public GameObject ItemHeld;
    public bool HoldingSomething;
    public void MouseControl()
    {
        if ( !(UI.GetComponent<Menu>().PlacingEnabled || HoldingSomething) )
        {
            PickUp();
        }
        else if (!UI.GetComponent<Menu>().PlacingEnabled && HoldingSomething)
        {
            Place();
        }
    }
    private void PickUp()
    { // Get target from raycast and order to follow hand
        try
        {
            if (this.gameObject.GetComponent<Selection>().selection.gameObject != null && !HoldingSomething)
            {
                GameObject target = this.gameObject.GetComponent<Selection>().selection.gameObject;
                if (target.tag == "Paper" || target.tag == "Glass" || target.tag == "Plastic")
                {
                    target.gameObject.GetComponent<Item>().followDestination = itemHolder.gameObject;
                    target.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    ItemHeld = target;
                    HoldingSomething = true;
                }
            }
        }
        catch (System.Exception ex) { }
    }
    private void Place()
    {
        GameObject target = this.gameObject.GetComponent<Selection>().selection.gameObject;
        if(target.tag == "Bin")
        {
            ItemHeld.gameObject.GetComponent<Item>().followDestination = target.gameObject;
            ItemHeld = target;
            HoldingSomething = false;
        }
    }
}
