using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLifeAndPlates : MonoBehaviour {

	public bool ModoChapa;
	public bool ModoVida;
	public int CantidadAAgregar;
	private PlayerAndHUDController ControladorJugador;

	void Start(){
		if (ModoChapa) {
			ModoVida = false;
		} else {
			ModoVida = true;
		}
		ControladorJugador = GameObject.Find ("HUD").GetComponent<PlayerAndHUDController>();
	}

	void OnTriggerEnter2D(Collider2D colision){
		if (colision.tag == "Player") {

			if (ModoChapa) {
				ControladorJugador.AgregarChapas (CantidadAAgregar);
				//Funcion de Salto de chapa
				Destroy (this.gameObject);
			} else {
				ControladorJugador.AgregarVida (CantidadAAgregar);
				//Funcion de Salto de vida
				Destroy (this.gameObject);
			}

		}

	}

}
