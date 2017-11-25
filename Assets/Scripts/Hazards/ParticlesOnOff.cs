using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnOff : MonoBehaviour {

	//Vetor de todos os particleSystems
	public ParticleSystem [] systems;
	public Light _light;

	public void ParticleSystemsPlay(){
		for (int i = 0; i < systems.Length; i++) {
			systems [i].Play ();
		}
		if (_light != null) {
			_light.enabled = true;
		}
	}

	public void ParticleSystemsStop(){
		for (int i = 0; i < systems.Length; i++) {
			systems [i].Stop ();
		}
		if (_light != null) {
			_light.enabled = false;
		}
	}
}
