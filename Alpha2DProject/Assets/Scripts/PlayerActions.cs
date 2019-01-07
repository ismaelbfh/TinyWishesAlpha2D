using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

	//Script Controlador del Player y HUD:
	PlayerAndHUDController ControladorJugador;
	//Jugador:
	GameObject Jugador;

	void Start(){
		ControladorJugador = GameObject.Find ("HUD").GetComponent<PlayerAndHUDController>();
		Jugador = GameObject.FindGameObjectWithTag("Player");
	}

	public void ActivarAtaquePLayer(){
		ControladorJugador.AtacarPlayer (0, true);
		Jugador.GetComponent<Animator>().SetInteger("nAttack",0);
	}
}
