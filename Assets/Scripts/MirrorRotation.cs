using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    public GameObject target;
    public bool mirror = true;
    // Update is called once per frame
    void Update()
    {
        if (mirror)
        {
            Transform rot = target.gameObject.transform;
            this.gameObject.transform.rotation = new Quaternion(0f, rot.rotation.y, 0f, rot.rotation.w);
        }
    }
}
