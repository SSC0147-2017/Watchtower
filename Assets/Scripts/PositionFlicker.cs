using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFlicker : MonoBehaviour {
	/**
	 * Baseado no código "FireLight.cs" usado pelo prefab "FireComplex" do Standard Assets
	 */

	//posição original relativa ao Pai
	private Vector3 origPos;
	private float m_Rnd;

	// Use this for initialization
	void Start () {
		m_Rnd = Random.value*100;
		origPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.PerlinNoise(m_Rnd + 0 + Time.time*2, m_Rnd + 1 + Time.time*2) - 0.5f;
		float y = Mathf.PerlinNoise(m_Rnd + 2 + Time.time*2, m_Rnd + 3 + Time.time*2) - 0.5f;
		float z = Mathf.PerlinNoise(m_Rnd + 4 + Time.time*2, m_Rnd + 5 + Time.time*2) - 0.5f;
		//transform.localPosition = Vector3.up + new Vector3(x, y, z)*1;

		transform.localPosition = origPos + new Vector3(x, y, z)*1;
	}
}
