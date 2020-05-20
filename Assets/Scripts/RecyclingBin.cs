using UnityEngine;

public class RecyclingBin : MonoBehaviour
{   //Correct type of trash thrown here
    public string trashType;
    // Score award and penalty values
    public int RightScoreBonus;
    public int WrongScorePenalty;
    // GaneController
    private GameObject GameController;
    
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").gameObject; 
    }
    // On collision delete item and if it is right 
    private void OnCollisionEnter(Collision collision)
    {
        string objectType = collision.gameObject.tag;
        if(trashType == objectType)
        {
            checkIfSame(collision.gameObject);
            Destroy(collision.gameObject);
            GameController.GetComponent<GameController>().UpdateScore(RightScoreBonus);
        }
        else if ( "" != objectType && collision.gameObject.GetComponent<Item>().followDestination != null)
        {
            checkIfSame(collision.gameObject);
            Destroy(collision.gameObject);
            GameController.GetComponent<GameController>().UpdateScore(WrongScorePenalty);
        }
    }

    void checkIfSame(GameObject g)
    {
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ItemHeld == g)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().HoldingSomething = false;
        }
    }
}
