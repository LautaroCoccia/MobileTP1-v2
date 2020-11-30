using UnityEngine;
using System.Collections;

public class FadeInicioFinal : MonoBehaviour 
{
	public float Duracion = 2;
	public float Vel = 2;
	float TiempInicial;
	
	MngPts Mng;
	
	bool MngAvisado = false;

	// Use this for initialization
	void Start ()
	{
		//renderer.material = IniFin;
		Mng = (MngPts)GameObject.FindObjectOfType(typeof (MngPts));
		TiempInicial = Mng.TiempEspReiniciar;
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Mng.TiempEspReiniciar > TiempInicial - Duracion)//aparicion
		{

		}
		else if(Mng.TiempEspReiniciar < Duracion)//desaparicion
		{
			
			if(!MngAvisado)
			{
				MngAvisado = true;
				Mng.DesaparecerGUI();
			}
		}
				
	}
}
