using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private TextMesh _textMesh;

	// Use this for initialization
	void Start () {
        gameObject.renderer.sortingLayerName = "UI";

        _textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetText(string message)
    {
        _textMesh.text = message;
    }
}
