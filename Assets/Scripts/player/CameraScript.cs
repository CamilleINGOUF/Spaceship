using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject target;

    private Vector3 offset;

	void Start ()
    {
        offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + offset.y, transform.position.z);
	}
}
