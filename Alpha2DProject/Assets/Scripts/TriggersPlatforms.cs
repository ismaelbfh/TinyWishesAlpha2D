using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersPlatforms : MonoBehaviour {

    private EnemySkeletonController controladorEsqueleto;

    public GameObject EsqueletoAsociado;
    public bool EsEnemigo;
    
    private void Start()
    {
       controladorEsqueleto = EsqueletoAsociado.GetComponent<EnemySkeletonController>();
    }

    /// <summary>
    /// Revisa las colisiones que haga la plataforma para de esta manera verificar si esta entrando el jugador y activar la funciòn atacar o si es el esqueleto para
	/// activar la función de voltearse.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D Colision)
    {
		//Si el collider esta configurado para verificar si el player esta en la plataforma y el player entra en el collider activa la función de ataque del enemigo y 
		//le da la posición del player cuando entró al collider.
        if (!EsEnemigo)
        {
			if (Colision.tag == "Player" && EsqueletoAsociado != null)
            {
				
                controladorEsqueleto.JugadorEnPlataforma = true;
                //Voltear hacia la posición del esqueleto 1 vez:
                //if (Colision.transform.position.x < EsqueletoAsociado.transform.position.x) {
                //	controladorEsqueleto.MirarHaciaIzquierda = true;
                //	controladorEsqueleto.GetComponent<SpriteRenderer>().flipX = false;
                //} else if (Colision.transform.position.x > EsqueletoAsociado.transform.position.x) {
                //	controladorEsqueleto.MirarHaciaIzquierda = false;
                //	controladorEsqueleto.GetComponent<SpriteRenderer>().flipX = true;
                //}
                controladorEsqueleto.VoltearRespectoAObjeto(Colision.transform.position.x);
            }
		}//Si el collider esta activado como verificador de limite de caminar del enemigo y si es el enemigo que entra en el trigger del collider activa la función 
		//de voltearse y le dice la posición del jugador cuando entro al collider.
        else if (EsEnemigo)
        {

			if (EsqueletoAsociado != null && Colision.name == EsqueletoAsociado.name)
            {
                controladorEsqueleto.Voltear = true;
            }

        }
    }

    /// <summary>
    /// Verifica cuando el jugador sale del collider para poder desactivar la función de ataque del jugador, además le quita la posición dicha anteriormente y
	/// le activa la función de voltearse.
    /// </summary>
    private void OnTriggerExit2D(Collider2D Colision)
    {
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = false;
                //controladorEsqueleto.VoltearCaminar = true;
                controladorEsqueleto.VoltearRespectoAObjeto(Colision.transform.position.x);
            }
        }
    }

}
