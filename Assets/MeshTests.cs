using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTests : MonoBehaviour {

	Vector3[] verts;
	int sizeOfList;
	public float degreeOfDistortion;
	public float regularOffset;
	// Use this for initialization
	void Start () {
		verts = this.GetComponent<MeshFilter> ().mesh.vertices;
		sizeOfList = verts.Length;
		EditVertices (verts);
		
	}

	// Update is called once per frame
	void Update () {
		
		
	}

	//Edit the vertices of the original mesh

	void EditVertices(Vector3[] vertices) {
		for(int i = 0; i < sizeOfList; i++) {
			vertices[i].x *= Random.value * degreeOfDistortion;
			vertices[i].y *= Random.value * degreeOfDistortion;
			vertices[i].z *= Random.value * degreeOfDistortion;

		}

		this.GetComponent<MeshFilter> ().mesh.vertices = vertices;
		this.GetComponent<MeshFilter> ().mesh.RecalculateBounds ();

	}
		

}
