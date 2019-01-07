using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionesGenericas : MonoBehaviour {

    public float PorcentajeRestarCollider;

	public void DestruirEsteObjeto(){
		Destroy (this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.name.Contains("Centellaprefab"))
        {
            BoxCollider2D BX2D = this.GetComponent<BoxCollider2D>();
            //Calcula el porcentaje a sumar para agrandar el collider del enemigo y hacerlo mas agil:
            float calculoTotal = (BX2D.size.x / 100) * PorcentajeRestarCollider;
            BX2D.size = new Vector2(BX2D.size.x + calculoTotal,BX2D.size.y);
            EnemySkeletonController ScriptSkeleton = transform.parent.GetComponent<EnemySkeletonController>();
            ScriptSkeleton.Estatico = true;
            if (ScriptSkeleton.GetComponent<Animator>().GetFloat("vspeed") == 0)
            {
                ScriptSkeleton.VoltearRespectoAObjeto(otro.transform.position.x);
            }
            ScriptSkeleton.SaltarEsqueleto();
        }

    }
}
