using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour {

    public GameObject[] availableBackgrounds;
    public List<GameObject> currentBackgrounds;

    private float screenHeightInPoints;

    void Start ()
    {
        float width = 2.0f * Camera.main.orthographicSize;
        screenHeightInPoints = width * Camera.main.aspect;
	}
	
	void add3Background(float furthestBackgroundEndY)
    {
        int randomIndex = Random.Range(0, availableBackgrounds.Length);

        GameObject background = (GameObject)Instantiate(availableBackgrounds[randomIndex]);

    }
}
