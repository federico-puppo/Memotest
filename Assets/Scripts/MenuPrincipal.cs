using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    [SerializeField] int dificultad;
    [SerializeField] int AreaX;
    [SerializeField] int AreaY;
    [SerializeField] int Intentos;
    [SerializeField] int Pares;
    [SerializeField] Text txt_Area;
    [SerializeField] Text txt_Pares;
    [SerializeField] Text txt_Intentos;
    [SerializeField] Text txt_Dificultad;
    [SerializeField] GameObject PanelDificultad;
	[SerializeField] GameObject PanelMenuPrincipal;
	[SerializeField] GameObject PanelCreditos;

	public static MenuPrincipal instancia;

    private void Singleton()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        Singleton();
    }

    public void MostrarDificultad(bool estado)
    {
        PanelDificultad.SetActive(estado);
		PanelMenuPrincipal.SetActive(!estado);
		PanelCreditos.SetActive(!estado);
	}
	public void MostrarCreditos(bool estado)
	{
		PanelCreditos.SetActive(estado);
		PanelMenuPrincipal.SetActive(!estado);
		PanelDificultad.SetActive(!estado);
	}
	public void IniciarJuego(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void showDificulty(int dificultad)
    {
        string txt_dif = "";
        switch(dificultad)
        {
            case 1:
                AreaX = 3;
                AreaY = 2;
                Intentos = 10;
                txt_dif = "MUY FACIL";
                break;
            case 2:
                AreaX = 4;
                AreaY = 3;
                Intentos = 10;
                txt_dif = "FACIL";
                break;
            case 3:
                AreaX = 4;
                AreaY = 4;
                Intentos = 8;
                txt_dif = "MEDIO";
                break;
            case 4:
                AreaX = 5;
                AreaY = 4;
                Intentos = 8;
                txt_dif = "DIFICIL";
                break;
            case 5:
                AreaX = 6;
                AreaY = 5;
                Intentos = 5;
                txt_dif = "MUY DIFICIL";
                break;
            case 6:
                AreaX = 8;
                AreaY = 5;
                Intentos = 5;
                txt_dif = "EXTREMO";
                break;
            case 7:
                AreaX = 10;
                AreaY = 5;
                Intentos = 5;
                txt_dif = "IMPOSIBLE";
                break;
        }
        txt_Area.text = (AreaX + " X " + AreaY);
        txt_Intentos.text = Intentos.ToString();
        txt_Pares.text = ((AreaX*AreaY)/2).ToString();
        txt_Dificultad.text = txt_dif;

    }
    public void Salir()
    {
        Application.Quit();
    }
}
