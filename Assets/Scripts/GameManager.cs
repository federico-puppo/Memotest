using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instancia;
    private Tablero m_Tablero;
    [Header("Configuracion")]
    [SerializeField] float cdTime = 1.5f;
    [SerializeField] private int m_intentosRestantes = 5;
    [Header("Sonidos")]
    [SerializeField] AudioSource m_soundFx;
    [SerializeField] AudioClip m_acierto;
    [SerializeField] AudioClip m_error;
    [Header("Particulas")]
    [SerializeField] ParticlesManager particlesManager;
    [Header("UI")]
    [SerializeField] GameObject PanelPausa;
    [SerializeField] GameObject PanelVictoria;
    [SerializeField] GameObject PanelDerrota;
    [SerializeField] Text texto_CartasRestantes;
    [SerializeField] Text texto_IntentosRestantes;


    private bool m_PuedeSeleccionarCarta = true;
    private Carta m_FirstPick = null;
    private Carta m_SecondPick = null;

    public int IntentosRestantes
    {
        get
        {
            return m_intentosRestantes;
        }

        set
        {
            m_intentosRestantes = value;
        }
    }

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
    private void Awake ()
    {
        Singleton();
        m_Tablero = GetComponent<Tablero>();
	} 

    void Start ()
    {
        m_Tablero.InicializarTablero();
        ActualizarTexto();
	}

    public void ProcesarClickCarta(Carta carta)
    {
        if (!m_PuedeSeleccionarCarta && IntentosRestantes > 0 || PanelPausa.activeSelf == true || PanelDerrota.activeSelf == true){return;}
        if (!m_FirstPick)
        {
            PrimeraCartaSeleccionada(carta);
        }
        else if (!m_SecondPick)
        {
            SegundaCartaSeleccionada(carta);
        }
    }
    private void PrimeraCartaSeleccionada(Carta carta)
    {
        m_FirstPick = carta;
        carta.Reveal();
    }

    private void SegundaCartaSeleccionada(Carta carta)
    {
        if (carta == m_FirstPick) { return; }
        m_SecondPick = carta;
        carta.Reveal();
        StartCoroutine(BloquearSeleccionPorTiempo(cdTime));
        StartCoroutine(CheckCard(m_FirstPick, m_SecondPick));        

    }

    private IEnumerator CheckCard(Carta firstPick, Carta secondPick)
    {
        yield return new WaitForSeconds(1.0f);
        if (firstPick.id == secondPick.id)
        {
            Coincidencia(m_FirstPick, m_SecondPick);
        }
        else
        {
            NoCoincidencia(m_FirstPick, m_SecondPick);
        }
        
    }

    private void Coincidencia(Carta firstPick, Carta secondPick)
    {
        StartCoroutine(BloquearSeleccionPorTiempo(cdTime));
        particlesManager.Emitir(firstPick.transform);
        particlesManager.Emitir(secondPick.transform);
        m_FirstPick.Acierto();
        m_SecondPick.Acierto();        
        m_FirstPick = null;
        m_SecondPick = null;        
        m_Tablero.m_cartasRestantes -= 2;
        ActualizarTexto();
        if (m_acierto!=null)m_soundFx.PlayOneShot(m_acierto);
        if (m_Tablero.m_cartasRestantes <= 0)
        {
            PanelVictoria.SetActive(true);
        }
    }

    private void NoCoincidencia(Carta firstPick, Carta secondPick)
    {
        StartCoroutine(BloquearSeleccionPorTiempo(cdTime));
        m_FirstPick = null;
        m_SecondPick = null;
        IntentosRestantes--;
        ActualizarTexto();
        if (m_error != null) m_soundFx.PlayOneShot(m_error);
        if (IntentosRestantes <= 0)
        {
            Perder();
        }
        else
        {
            firstPick.Reset();
            secondPick.Reset();
            StartCoroutine(BloquearSeleccionPorTiempo(cdTime));
        }
    }
    private void Perder()
    {
        m_PuedeSeleccionarCarta = false;
        PanelDerrota.SetActive(true);

        foreach (Carta c in UnityEngine.Object.FindObjectsOfType<Carta>())
        {
            c.Reveal();
        }
    }


    IEnumerator BloquearSeleccionPorTiempo(float tiempo)
    {
        m_PuedeSeleccionarCarta = false;
        yield return new WaitForSeconds(tiempo);
        m_PuedeSeleccionarCarta = true;
    }

    public void ActualizarTexto()
    {
        if (texto_CartasRestantes != null)
        {
            texto_CartasRestantes.text = ("PARES RESTANTES: " + (m_Tablero.m_cartasRestantes/2).ToString());
        }
        if (texto_IntentosRestantes != null)
        {
            texto_IntentosRestantes.text = ("INTENTOS RESTANTES: " + m_intentosRestantes.ToString());
        }
    }
    public void MostrarMenu(bool estado)
    {
        PanelPausa.SetActive(estado);
    }
    public void ResiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void IrAMenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


