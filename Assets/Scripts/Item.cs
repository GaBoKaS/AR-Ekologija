using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject followDestination;
    float rotSpeed;
    public float speed;
    float wRadius = 0f;
    private void Update()
    {
        if(followDestination != null)
        {
            Debug.Log(followDestination.gameObject.tag);
            if (Vector3.Distance(followDestination.transform.position, transform.position) > wRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, followDestination.transform.position, Time.deltaTime * speed);
            }
            transform.rotation = followDestination.transform.rotation;
        }
    }
}
