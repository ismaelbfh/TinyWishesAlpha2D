using System.Collections;
using UnityEngine;

public class ResizeScreen : MonoBehaviour {

    //    private Object[] Sprites;
    //    private static Vector2 aspectRatio;

    //    // Use this for initialization
    //    void Start()
    //    {
    //        aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);
    //        Camera.main.orthographicSize = (1080 * (aspectRatio.y / 9f) / 2) / 100;
    //        Sprites = FindObjectsOfType(typeof(GameObject));
    //        foreach (GameObject Sprit in Sprites)
    //        {
    //            if (Sprit.GetComponent<SpriteRenderer>() && !Sprit.transform.parent)
    //            {
    //                Sprit.transform.localScale = new Vector3(Sprit.transform.localScale.x * (aspectRatio.x / 16f), Sprit.transform.localScale.y * (aspectRatio.y / 9f), Sprit.transform.localScale.z);
    //                Sprit.transform.position = new Vector3(Sprit.transform.position.x * (aspectRatio.x / 16f), Sprit.transform.position.y * (aspectRatio.y / 9f), Sprit.transform.position.z);
    //            }
    //        }
    //    }

    //    public static Vector2 getTransVel(Vector2 Velocity)
    //    {
    //        aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);
    //        return new Vector2(Velocity.x * (aspectRatio.x / 16f), Velocity.y * (aspectRatio.y / 9f));
    //    }
    //}

    //    public static class AspectRatio
    //    {
    //        public static Vector2 GetAspectRatio(int x, int y)
    //        {
    //            float f = (float)x / (float)y;
    //            int i = 0;
    //            while (true)
    //            {
    //                i++;
    //                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
    //                    break;
    //            }
    //            return new Vector2((float)System.Math.Round(f * i, 2), i);
    //        }
    //        public static Vector2 GetAspectRatio(Vector2 xy)
    //        {
    //            float f = xy.x / xy.y;
    //            int i = 0;
    //            while (true)
    //            {
    //                i++;
    //                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
    //                    break;
    //            }
    //            return new Vector2((float)System.Math.Round(f * i, 2), i);
    //        }
    //        public static Vector2 GetAspectRatio(int x, int y, bool debug)
    //        {
    //            float f = (float)x / (float)y;
    //            int i = 0;
    //            while (true)
    //            {
    //                i++;
    //                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
    //                    break;
    //            }
    //            if (debug)
    //                Debug.Log("Aspect ratio is " + f * i + ":" + i + " (Resolution: " + x + "x" + y + ")");
    //            return new Vector2((float)System.Math.Round(f * i, 2), i);
    //        }
    //        public static Vector2 GetAspectRatio(Vector2 xy, bool debug)
    //        {
    //            float f = xy.x / xy.y;
    //            int i = 0;
    //            while (true)
    //            {
    //                i++;
    //                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
    //                    break;
    //            }
    //            if (debug)
    //                Debug.Log("Aspect ratio is " + f * i + ":" + i + " (Resolution: " + xy.x + "x" + xy.y + ")");
    //            return new Vector2((float)System.Math.Round(f * i, 2), i);
    //        }

    public enum Aspecto { Aspecto5by4, Aspecto4by3, Aspecto3by2, Aspecto16by10, Aspecto16by9 };

    public Camera camara;
    public Aspecto aspectoOriginal = Aspecto.Aspecto16by9;

    float[] ratios = new float[] { 1.25f, 1.333f, 1.5f, 1.6f, 1.777f };

    void Reducir(Aspecto aspectoActual)
    {

        float porcentaje = (ratios[(int)aspectoActual] - ratios[(int)aspectoOriginal])
                       / ratios[(int)aspectoActual];
        this.camara.rect = new Rect(0f, 0f, 1f - porcentaje, 1f);
        this.camara.pixelRect = new Rect(Screen.width * porcentaje * 0.5f, 0f, Screen.width * (1f - porcentaje), (float)Screen.height);

    }

    void Aumentar(Aspecto aspectoActual)
    {

        float porcentaje = (ratios[(int)aspectoOriginal] - ratios[(int)aspectoActual])
                       / ratios[(int)aspectoOriginal];
        this.camara.rect = new Rect(0f, 0f, 1f, 1f - porcentaje);
        this.camara.pixelRect = new Rect(0f, Screen.height * porcentaje * 0.5f, (float)Screen.width, Screen.height * (1f - porcentaje));

    }

    void Awake()
    {

        float aspectoPantalla = this.camara.aspect;
        Aspecto aspectoActual;

        if (aspectoPantalla < 1.29f) aspectoActual = Aspecto.Aspecto5by4;
        else if (aspectoPantalla < 1.4f) aspectoActual = Aspecto.Aspecto4by3;
        else if (aspectoPantalla < 1.6f) aspectoActual = Aspecto.Aspecto3by2;
        else if (aspectoPantalla < 1.7) aspectoActual = Aspecto.Aspecto16by10;
        else aspectoActual = Aspecto.Aspecto16by9;

        if (aspectoActual > this.aspectoOriginal) this.Reducir(aspectoActual);
        if (aspectoActual < this.aspectoOriginal) this.Aumentar(aspectoActual);

    }
}
