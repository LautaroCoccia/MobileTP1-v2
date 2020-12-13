using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets {

    public Player player;
    private MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows,
        Joystick
    }

    public GameObject paso1;
    bool agarrar = false;
    public GameObject paso2;
    bool levantar = false;
    public GameObject paso3;
    bool dejar = false;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;
    private void Start()
    {
#if UNITY_EDITOR

        if( player.IdPlayer == 0)
        {
            miInput = MoveType.WASD;
        }
        if(player.IdPlayer == 1)
        {
            miInput = MoveType.Arrows;
        }
#elif UNITY_ANDROID
        if( player.IdPlayer == 0)
        {
             miInput=MoveType.Joystick;
        }
        if(player.IdPlayer == 1)
        {
            miInput=MoveType.Joystick;
        }
#endif
    }
    private void Update() {
        switch (miInput) {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.S)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D)) {
                    TercerPaso();
                }
                break;
            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow)) {
                    TercerPaso();
                }
                break;
            case MoveType.Joystick:
                if (!Tenencia() && Desde.Tenencia() && agarrar)
                {
                    PrimerPaso();
                    agarrar = false;
                }
                if (Tenencia() && levantar)
                {
                    SegundoPaso();
                    levantar = false;
                }
                if (segundoCompleto && Tenencia() && dejar)
                {
                    TercerPaso();
                    dejar = false;
                }
                break;
            default:
                break;
        }
    }
    public void LeftButton() 
    {
        agarrar = true; 
    }
    public void MidButton()
    { 
        levantar = true; 
    }
    public void RightButton() 
    {
        dejar = true;
    }
    void PrimerPaso() {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso() {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor) {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet) {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
