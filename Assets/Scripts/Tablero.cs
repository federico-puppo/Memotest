using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablero : MonoBehaviour {

    [Header("Valores")]
    [SerializeField] int m_AreaJuegoX = 4;
    [SerializeField] int m_AreaJuegoY = 4;
    [SerializeField] Vector2 m_SeparacionCartas = Vector2.zero;
    // Use this for initialization
    [Header("Referencias")]
    [SerializeField] GameObject m_Carta;
    [SerializeField] Transform m_AreaDeJuego;
    [SerializeField] private Sprite[] m_Imagenes;

    public int m_cartasRestantes{get; set;}

    public void InicializarTablero () {

        if (m_AreaJuegoX * m_AreaJuegoY % 2 != 0)
        {
            m_AreaJuegoY -= 1;
        }

        Vector2 posInicialCarta = CalcularPosInicial();
        int cantCartas = m_AreaJuegoX * m_AreaJuegoY;
        List<int> idCarta = CrearListaDeIDsMezclada(cantCartas);
        int cartasCreadas = 0;
		
        for (int x= 0; x < m_AreaJuegoX; x++)
        {
            for (int y=0; y< m_AreaJuegoY; y++)
            {
                GameObject cartaGo = Instantiate(m_Carta);
                cartaGo.transform.SetParent(m_AreaDeJuego);
                Carta cartaActual = cartaGo.GetComponent<Carta>();
                cartaActual.id = idCarta[cartasCreadas];
                cartaActual.SetImagen(m_Imagenes[cartaActual.id]);
                float posX = (x * m_SeparacionCartas.x) - posInicialCarta.x;
                float posY = (y * m_SeparacionCartas.y) - posInicialCarta.y;
                cartaGo.transform.localPosition = new Vector3(posX, 0,posY);
                cartasCreadas++;
            }
            m_cartasRestantes = cartasCreadas;
        }
	}
    private Vector2 CalcularPosInicial()
    {
        float posicionMaximaX = (m_AreaJuegoX - 1) * m_SeparacionCartas.x;
        float mitadPosMaxX = posicionMaximaX / 2;
        float posicionMaximaY = (m_AreaJuegoY - 1) * m_SeparacionCartas.y;
        float mitadPosMaxY = posicionMaximaY / 2;
        return new Vector2(mitadPosMaxX, mitadPosMaxY);
    }

    private List<int> CrearListaDeIDsMezclada(int cantCartas)
    {
        List<int> idCartas = new List<int>();

        for (int i = 0; i < cantCartas;i++)
        {
            idCartas.Add(i/2);
            
        }
        idCartas.Shuffle();
        return idCartas;
    }

}
