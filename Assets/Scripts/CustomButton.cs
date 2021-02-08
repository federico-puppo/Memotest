using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomButton : MonoBehaviour {

    [SerializeField] int offsetX = 0;
    [SerializeField] int offsetY = 12;
    [SerializeField] int ID = 0;

    [SerializeField] RectTransform textRect;
    private Vector3 pos;

    void Start()
    {
        pos = textRect.localPosition;
    }

    public void Down()
    {
        textRect.localPosition = new Vector3(pos.x + (float)offsetX, pos.y - (float)offsetY, pos.z);
        Click();
    }

    public void Up()
    {
        textRect.localPosition = pos;
    }
    public void INFO()
    {
        MenuPrincipal.instancia.showDificulty(ID);
    }
    public void Click()
    {
        if (ID == 0)
        {
            return;
        }
        MenuPrincipal.instancia.IniciarJuego(ID);
    }
}
