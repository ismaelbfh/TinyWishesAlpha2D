using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackOfPlayerButton : MonoBehaviour {

	//Variables de Identificación de Ataques:
	public enum TipoDeAtaque {LluviaDeCentellas,MaldicionDelRayo};
	public TipoDeAtaque AtaqueARealizar = TipoDeAtaque.LluviaDeCentellas;
	private bool Activo = true;
	private Animator BotonAnimator;
	//Variables comúnes:
	PlayerAndHUDController ControladorJugador;

	void Start(){
		ControladorJugador = GameObject.Find("HUD").GetComponent<PlayerAndHUDController>();
		BotonAnimator = this.GetComponent<Animator>();
	}

	//Evento activado mediante un "EventTrigger" en el respectivo boton que activa la animación de recarga y hace la función de ataque.
	public void Pulsado(){
		//Activar Animación de Recarga.
		//Activar Método de Ataque del Player según cada ataque
		switch (AtaqueARealizar) {
		case TipoDeAtaque.LluviaDeCentellas:
			ControladorJugador.AtacarPlayer (1, false);
			BotonAnimator.SetBool("Recargar",true);
			Activo = false;
			break;
		default:
			Debug.Log ("No hay ataque seleccionado");
			break;
		}
	}

	public void ActivarBoton(){
		Activo = true;
		this.GetComponent<Button>().interactable = true;
		BotonAnimator.SetBool("Recargar",false);
	}

	void Update(){
		if(!Activo){
			this.GetComponent<Button>().interactable = false;
		}
	}

}
