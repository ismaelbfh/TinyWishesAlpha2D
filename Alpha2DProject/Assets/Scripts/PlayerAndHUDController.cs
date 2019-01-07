using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAndHUDController : MonoBehaviour {

    //Variables del Jugador:
    private GameObject _Jugador;
    private float _TamañoX;
    private Rigidbody2D _cuerpoJugador;
    private Animator _animatorJugador;
    private Image _BarraVida;
	private bool _EnSuelo;
	private float _RadioDetectarSuelo = 0.1f;
	private bool _Saltar;
	private int _ContadorSalto = 0;
	private int _ChapasJugador;
	private int _VidasJugador;

    //PLAYER:
    public float velocidadJugador = 2f;
    public float fuerzaSalto;
    public float VidaJugador;
	public GameObject posicionadorProyectil;
	public GameObject PrefabCentella;
	public Transform VerificadorSuelo; 
	public LayerMask LayerSuelo;
    //HUD:
    public bool PulsoIz = false;
    public bool PulsoDe = false;

	private Text _TextoChapas;
	private Text _TextoVidas;
	//Variables Generales del Nivel:
	public int ChapasMinimasARecoger;

    //GETTERS Y SETTERS:
    public GameObject Jugador
    {
        get
        {
            return _Jugador;
        }

        set
        {
            _Jugador = value;
        }
    }
    public float TamañoX
    {
        get
        {
            return _TamañoX;
        }

        set
        {
            _TamañoX = value;
        }
    }
    public Rigidbody2D CuerpoJugador
    {
        get
        {
            return _cuerpoJugador;
        }

        set
        {
            _cuerpoJugador = value;
        }
    }
    public Animator AnimatorJugador
    {
        get
        {
            return _animatorJugador;
        }

        set
        {
            _animatorJugador = value;
        }
    }
    public Image BarraVida
    {
        get
        {
            return _BarraVida;
        }

        set
        {
            _BarraVida = value;
        }
    }

	/// <summary>
	/// Inicialización de variables buscando sus respectivos objetos y componentes.
	/// </summary>
    private void Start()
    {
        //Jugador:
        Jugador = GameObject.FindGameObjectWithTag("Player"); //Objeto Jugador
        CuerpoJugador = Jugador.GetComponent<Rigidbody2D>(); //Cuerpo del jugador
        AnimatorJugador = Jugador.GetComponent<Animator>(); //Animador del jugador
        //HUD
        BarraVida = GameObject.Find("FondoVida").GetComponent<Image>();
		TamañoX = Jugador.transform.localScale.x;
		_TextoChapas = GameObject.Find ("TextoChapas").GetComponent<Text> ();
		_TextoVidas = GameObject.Find ("TextoVidas").GetComponent<Text> ();
		_TextoChapas.text = "0/" + ChapasMinimasARecoger;
		//_TextoVidas.text = Variable global que contenga las vidas del jugador.
    }

	/// <summary>
	/// Método de fisicas, utilizado para hacer que el jugador camine y que detecte si esta en el suelo o no.
	/// </summary>
    private void FixedUpdate()
    {

		//Verificar si esta tocando el suelo o no:

		_EnSuelo = Physics2D.OverlapCircle (new Vector2(VerificadorSuelo.position.x,VerificadorSuelo.position.y), _RadioDetectarSuelo, LayerSuelo);
		if (_EnSuelo) {
			_ContadorSalto = 0;
		}

		if (PulsoDe) { //Si hemos pulsado el boton de la derecha le cambiamos escala y le aplicamos una fuerza
			CuerpoJugador.velocity = new Vector2 (velocidadJugador, CuerpoJugador.velocity.y);
			//Jugador.transform.localScale = new Vector3(TamañoX, Jugador.transform.localScale.y, 0f);
			Jugador.GetComponent<SpriteRenderer> ().flipX = false;
			AnimatorJugador.SetFloat ("speed", 0.2f);
		} else if (PulsoIz) { //Si pulsamos el de la izquierda se lanzara esto
			CuerpoJugador.velocity = new Vector2 (-velocidadJugador, CuerpoJugador.velocity.y);
			//Jugador.transform.localScale = new Vector3(-TamañoX, Jugador.transform.localScale.y, 0f);
			Jugador.GetComponent<SpriteRenderer> ().flipX = true;
			AnimatorJugador.SetFloat ("speed", 0.2f);
		} else {
			AnimatorJugador.SetFloat ("speed", 0);
			CuerpoJugador.velocity = new Vector2 (0, CuerpoJugador.velocity.y);
		}

		if (_Saltar && (_EnSuelo || _ContadorSalto <= 1)) {
			CuerpoJugador.velocity = new Vector2 (0, fuerzaSalto);
			AnimatorJugador.SetFloat ("vSpeed", Mathf.Abs (CuerpoJugador.velocity.y));
			_Saltar = false;
		} else if(_EnSuelo) {
			AnimatorJugador.SetFloat ("vSpeed", 0);
		}
    
    }

    /// Funcion para activar/desactivar movimiento --> Se activan mediante un objeto "Event Trigger" en los Canvas de movimiento (BotonMovimientoIz y BotonMovimientoDe)
    /// 1 es Izquierda, 2 es detenido y 3 es derecha.
    /// </summary>
    public void ActivarDesactivarMovimiento(int direccion)
    {
        PulsoDe = direccion == 1 ? false : (direccion == 2 ? false : true);
        PulsoIz = direccion == 1 ? true : false;   
    }
    /// <summary>
    /// Funcion de Salto que se activarán al pulsar encima del objeto Canvas llamado "AreaSaltar" con un objeto "Event Trigger"
    /// </summary>
    public void SaltarJugador()
    {
		_Saltar = true;
		_ContadorSalto++;
    }
		
    
    /// <summary>
    /// Función llamada desde los objetos o armas que causen daño al jugador, le restan la vida al mismo y bajan la barra.
    /// </summary>
    public void BajarVida(float daño)
    {
        VidaJugador -= daño;
        BarraVida.fillAmount = VidaJugador;
    }

	/// <summary>
	/// Agrega la cantidad de vidas indicada al player, la activa el script "AddLifeAndPlates" de los objetos vida y chapas.
	/// </summary>
	public void AgregarVida(int cantidad){
		_VidasJugador += cantidad;
		_TextoVidas.text = _VidasJugador.ToString();
	}
	/// <summary>
	/// /// Agrega la cantidad de chapas indicada al player, la activa el script "AddLifeAndPlates" de los objetos vida y chapas
	/// </summary>
	public void AgregarChapas(int cantidad){
		_ChapasJugador += cantidad;
		_TextoChapas.text = _ChapasJugador + "/" + ChapasMinimasARecoger;
	}

	public void AtacarPlayer(int numeroAnimacion, bool Cuchillo){

		if (!Cuchillo) {
			AnimatorJugador.SetInteger ("nAttack", numeroAnimacion);
		} else {
/*			float PosX = posicionadorProyectil.transform.position.x;
			float PosY = posicionadorProyectil.transform.position.y;
			float Posz = posicionadorProyectil.transform.position.z;*/

			if (Jugador.GetComponent<SpriteRenderer> ().flipX) {
				//posicionadorProyectil.transform.position = Mathf.Abs (PosX) == PosX ? new Vector3 (-PosX,PosY,Posz): posicionadorProyectil.transform.position;
				posicionadorProyectil.transform.localPosition = new Vector3(-0.8990002f,posicionadorProyectil.transform.localPosition.y,posicionadorProyectil.transform.localPosition.z);
				GameObject Centella = Instantiate (PrefabCentella,posicionadorProyectil.transform.position,posicionadorProyectil.transform.rotation,this.gameObject.transform);
				Centella.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-10, 0), ForceMode2D.Impulse);
			} else {
				posicionadorProyectil.transform.localPosition = new Vector3(0.8990002f,posicionadorProyectil.transform.localPosition.y,posicionadorProyectil.transform.localPosition.z);
				GameObject Centella = Instantiate (PrefabCentella,posicionadorProyectil.transform.position,posicionadorProyectil.transform.rotation,this.gameObject.transform);
				Centella.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (10, 0), ForceMode2D.Impulse);
			}


		}
	}
}
