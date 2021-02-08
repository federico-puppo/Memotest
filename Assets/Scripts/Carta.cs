using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour {

	public int id { get; set; }

    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Animator m_Animator;

    private void OnMouseDown()
    {
		if (GameManager.instancia != null)
		{
			GameManager.instancia.ProcesarClickCarta(this);
		}		
    }


    public void Reveal()
    {
        m_Animator.Play("Reveal");
    }
    public void Hide()
    {
        m_Animator.Play("Hide");
    }
    public void Acierto()
    {
        m_Animator.Play("Celebration");
        Destroy(gameObject, 1.2f);
    }
    public void Reset()
    {
        m_Animator.Play("Error");
        Invoke("Hide", 1.0f);
    }
    public void SetImagen(Sprite sprite)
    {
        m_SpriteRenderer.sprite = sprite;
    }
	public void Special()
	{
		m_Animator.Play("Special");
	}
}
