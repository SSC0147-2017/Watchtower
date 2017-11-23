using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnOff : MonoBehaviour {

	//Vetor de todos os particleSystems
	public ParticleSystem [] systems;
	public Light light;

	public void ParticleSystemsPlay(){
		for (int i = 0; i < systems.Length; i++) {
			systems [i].Play ();
		}
		if (light != null) {
			light.enabled = true;
		}
	}

	public void ParticleSystemsStop(){
		for (int i = 0; i < systems.Length; i++) {
			systems [i].Stop ();
		}
		if (light != null) {
			light.enabled = false;
		}
	}
}
