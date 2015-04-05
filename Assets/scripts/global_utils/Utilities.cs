using System.Collections.Generic;
using UnityEngine;

public class Utilities{
	
	//clamp a float
	public static float ClampFloat(float val, float min, float max){
		return (val > max) ? max : ((val < min) ? min : val);
	}
	
	//the ray from a camera to the mouse
	public static Ray mouseRay(){
		return Camera.main.ScreenPointToRay( Input.mousePosition );
	}
	
	public static GameObject CreateText(string text, Vector3 position){
		GameObject textMeshPrefab = (GameObject)Resources.Load("text_mesh");
		GameObject g = (GameObject)GameObject.Instantiate(textMeshPrefab);
		TextMesh tm = g.GetComponent<TextMesh>();
		//MeshRenderer mr = g.GetComponent<MeshRenderer>();
		tm.text = text;
		//Font ft = (Font)Resources.Load("Arial");
		//Debug.Log(ft);
		g.transform.position = position;
		return g;
		
	}
}
