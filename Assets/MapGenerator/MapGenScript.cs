using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenScript : MonoBehaviour {

    public Transform trans;
    public GameObject[] prefabF;
    public GameObject[] prefabR;
    public GameObject[] prefabL;
    public GameObject[] prefabD;
	public GameObject[] prefabC;
    public Texture2D texture;

    private int width;
    private int height;

    const float multiplierFactor = 4.0f + float.Epsilon;

    public void PressButon() {
        Debug.Log("Me clicaram");
        GenerateMap();
    }

    private void GenerateMap() {
        width = texture.width;
        height = texture.height;
        Color[] pixels = texture.GetPixels();
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                Color pixelColor = pixels[i * height + j];
                if (pixelColor == Color.white) { //chao
                    GameObject inst = GameObject.Instantiate(randomPrefab(prefabF), trans);
                    inst.transform.position = new Vector3(j* multiplierFactor, 0, i* multiplierFactor);
                }
                if (pixelColor == Color.red) //parede reta
                { 
                    GameObject inst = GameObject.Instantiate(randomPrefab(prefabR), trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
					//inst.transform.Rotate(new Vector3(0, FindRotationR(pixels, i, j), 0));
                }
                if (pixelColor == Color.green) //Curva L regular
                {
                    GameObject inst = GameObject.Instantiate(randomPrefab(prefabL), trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
					//inst.transform.Rotate(new Vector3(0, FindRotationL(pixels, i, j), 0));
                }
                if (pixelColor == Color.blue) //Diagonal
                {
                    GameObject inst = GameObject.Instantiate(randomPrefab(prefabD), trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
					//inst.transform.Rotate(new Vector3(0, 0, FindDiagonalRotation(pixels, i, j)));
                }
				if (pixelColor == Color.black) //Coluna
				{
					GameObject inst = GameObject.Instantiate(randomPrefab(prefabC), trans);
					inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
					//inst.transform.Rotate(new Vector3(0, FindRotationC(pixels, i, j), 0));
				}
            }
        }
    }

    //Find wall rotation for a given wall at i and j
	private float FindRotationR(Color[] pixels, int i, int j) {
        //Floor is in the Right
        float rotation = 0;
        //Floor is in the Left
        if ((pixels[i * height + j - 1] == Color.yellow))
            rotation = 180;
        //Floor is below
        if ((pixels[(i+1) * height + j] == Color.yellow))
            rotation = 90;
        if ((pixels[(i - 1) * height + j] == Color.yellow))
            rotation = -90;
        return rotation;
    }

    //Find a diagonal wall rotation
    private float FindRotationC(Color[] pixels, int i, int j) {
		//Floor is in the Left and Bellow
		float rotation = 0;
		//Floor is in the Right And Bellow
		if (((pixels[i * height + j - 1] == Color.black)||(pixels[i * height + j - 1] == Color.white))&&((pixels[(i - 1) * height + j] == Color.black)||(pixels[(i - 1) * height + j] == Color.white)))
			rotation = 90;
		//Floor is Up And Left
		if (((pixels[i * height + j + 1] == Color.black)||(pixels[i * height + j + 1] == Color.white))&&((pixels[(i + 1) * height + j] == Color.black)||(pixels[(i + 1) * height + j] == Color.white)))
			rotation = -90;
		//Floor is Up And Right
		if (((pixels[i * height + j + 1] == Color.black)||(pixels[i * height + j + 1] == Color.white))&&((pixels[(i + 1) * height + j] == Color.black)||(pixels[(i + 1) * height + j] == Color.white)))
			rotation = 180;
		return rotation;
	}

	private float FindRotationL(Color[] pixels, int i, int j) {
		//Floor is in the Right And Bellow
		float rotation = 0;
		//Floor is in the Left And Up
		if ((pixels[i * height + j - 1] == Color.yellow) && (pixels[(i - 1) * height + j] == Color.yellow))
			rotation = 180;
		//Floor is below And Left
		if ((pixels[i * height + j - 1] == Color.yellow) && (pixels[(i+1) * height + j] == Color.yellow))
			rotation = 90;
		if ((pixels[i * height + j + 1] == Color.yellow) && (pixels[(i - 1) * height + j] == Color.yellow))
			rotation = -90;
		return rotation;
	}


    //Return a random prefab from a given array of prefabs
    private GameObject randomPrefab(GameObject[] prefabArray) {
        return prefabArray[Random.Range(0, prefabArray.Length-1)];
    }
}
