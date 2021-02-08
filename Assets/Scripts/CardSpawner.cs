using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
	[SerializeField] GameObject m_spawner;
	[SerializeField] GameObject m_Carta;
	[SerializeField] private Sprite[] m_Imagenes;
	[SerializeField] private int X_range = 80;
	[SerializeField] private int Y_range = 50;
	[SerializeField] private int Z_range = 20;
	[SerializeField] private readonly float rate = 0.2f;
	// Start is called before the first frame update
	void Start()
    {
		InvokeRepeating("Spawn",0f, rate);
	}

    void Spawn()
    {
		int rng = Random.Range(0, m_Imagenes.Length);
		GameObject cartaGo = Instantiate(m_Carta);
		cartaGo.transform.SetParent(m_spawner.transform);
		Carta cartaActual = cartaGo.GetComponent<Carta>();
		cartaActual.SetImagen(m_Imagenes[rng]);
		float posX = (Random.Range(-X_range, X_range));
		float posY = (Random.Range(-Y_range, Y_range));
		float posZ = (Random.Range(-Z_range, Z_range));
		cartaGo.transform.localPosition = new Vector3(posX, posZ, posY);
		cartaGo.transform.localRotation = new Quaternion(180,0,0,0);
		cartaActual.Special();
		Destroy(cartaGo, 8f);
	}
}
