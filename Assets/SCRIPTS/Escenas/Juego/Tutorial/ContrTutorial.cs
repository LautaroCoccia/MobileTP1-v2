using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;
	public float TiempTuto = 15;
	public float Tempo = 0;
	
	public bool Finalizado = false;
	bool Iniciado = false;
	
	LevelManager LV;
	
	//------------------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		LV = GameObject.Find("GameManager").GetComponent<LevelManager>();
		
		Pj.ContrTuto = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if(Iniciado)
		{
			if(Tempo < TiempTuto)
			{
				Tempo += T.GetDT();
				if(Tempo >= TiempTuto)
				{
					Finalizar();
				}
			}
		}
		*/
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//
	
	public void Iniciar()
	{
		Pj.GetComponent<Frenado>().RestaurarVel();
		Iniciado = true;
	}
	
	public void Finalizar()
	{
		Finalizado = true;
		LV.FinTutorial(Pj.IdPlayer);
	    Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.VaciarInv();
	}
}
