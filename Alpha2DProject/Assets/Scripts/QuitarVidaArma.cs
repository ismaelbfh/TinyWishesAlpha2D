using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(BoxCollider2D))]

public class QuitarVidaArma : MonoBehaviour
{
    //Variables
    private float contadorEliminar = 1.5f;

    public float DañoACausar;
	public bool DañarAPlayer;
	private PlayerAndHUDController ScriptPlayer;
     
	/// <summary>
	/// Busca el objeto HUD y el script que maneja la HUD y al player.
	/// </summary>
     void Awake()
     {
         ScriptPlayer = GameObject.Find("HUD").GetComponent<PlayerAndHUDController>();
     }

	/// <summary>
	/// Cuando su trigger se encuentre con el jugador activará la función de bajar vida del mismo y destruirá el objeto en cuestión.
	/// </summary>
     void OnTriggerEnter2D(Collider2D colision)
     {
		if (colision.tag == "Player" && DañarAPlayer)
         {
             ScriptPlayer.BajarVida(DañoACausar);
             Destroy(this.gameObject);
         }else if (colision.tag == "Enemy" && !DañarAPlayer) {
            EnemySkeletonController EsqueletoSC = colision.GetComponent<EnemySkeletonController>();
            EsqueletoSC.EraEstatico = true;
            EsqueletoSC.BajarVida(DañoACausar);
			Instantiate (Resources.Load("Explosion"),this.transform.position,this.transform.rotation);
			TriggersPlatforms[] Activadores = GameObject.FindObjectsOfType (typeof(TriggersPlatforms)) as TriggersPlatforms[];

			foreach(TriggersPlatforms CActivador in Activadores){
				if (CActivador.EsqueletoAsociado == colision.gameObject && !CActivador.EsEnemigo) {
					CActivador.GetComponent<BoxCollider2D> ().size = new Vector2 (GameObject.Find("Fondo").transform.localScale.x * 4784,CActivador.GetComponent<BoxCollider2D> ().size.y);
				}
			}


			Destroy(this.gameObject);
        }
        else if(!colision.isTrigger)
        {
            Destroy(this.gameObject);
        }
     }

	/// <summary>
	/// Contador que elimina el objeto despúes de un tiempo definido anteriormente.
	/// </summary>
     void FixedUpdate()
     {
        if (contadorEliminar <= 0)
         {
             Destroy(this.gameObject);
         }
          contadorEliminar -= Time.deltaTime;
     }


}
