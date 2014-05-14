using UnityEngine;
using System.Collections;

public class UITextAddon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.renderer.sortingLayerName = "UI";
        gameObject.renderer.sortingOrder = 9;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
