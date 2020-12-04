using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
public class MngPts : MonoBehaviour 
{
	Rect R = new Rect();
	
	public float TiempEmpAnims = 2.5f;
	float Tempo = 0;
	
	int IndexGanador = 0;
	
	public Vector2[] DineroPos;
	public Vector2 DineroEsc;
	public GUISkin GS_Dinero;
	
	public Vector2 GanadorPos;
	public Vector2 GanadorEsc;
	public GameObject[] Ganadores;
	public GUISkin GS_Ganador;
	
	public GameObject Fondo;

	public TextMeshProUGUI player1;
	public TextMeshProUGUI player2;


	public float TiempEspReiniciar = 10;
	
	
	public float TiempParpadeo = 0.7f;
	float TempoParpadeo = 0;
	bool PrimerImaParp = true;
	
	public bool ActivadoAnims = false;
	
	Visualizacion Viz = new Visualizacion();
	
	//---------------------------------//
	
	// Use this for initialization
	void Start () 
	{		
		SetGanador();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//PARA JUGAR
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || 
		   Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Mouse0) || TiempEspReiniciar <= 0)
		{
			SceneManager.LoadScene("Credits");
		}

		SetDinero();
		//REINICIAR


		//CIERRA LA APLICACION


		//CALIBRACION DEL KINECT



		TiempEspReiniciar -= Time.deltaTime;

		if(ActivadoAnims)
		{
			TempoParpadeo += Time.deltaTime;
			
			if(TempoParpadeo >= TiempParpadeo)
			{
				TempoParpadeo = 0;
				
				if(PrimerImaParp)
                {
					PrimerImaParp = false;
					
				}
				else
				{
					if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
					{
						player1.text = "";

					}
					else if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)
					{
						player2.text = "";

					}
					TempoParpadeo += 0.1f;
					PrimerImaParp = true;
				}
			}
		}
		
		
		
		if(!ActivadoAnims)
		{
			Tempo += Time.deltaTime;
			if(Tempo >= TiempEmpAnims)
			{
				Tempo = 0;
				ActivadoAnims = true;
			}
		}
		
		
	}
	
	/*
	void OnGUI()
	{
		SetGUIGanador();
		SetGUIPerdedor();
		GUI.skin = null;
	}
	*/
	
	void OnGUI()
	{
		if(ActivadoAnims)
		{
			
			SetCartelGanador();
		}
		
		GUI.skin = null;
	}
	
	//---------------------------------//
	
	/*
	void SetGUIGanador()
	{
		GUI.skin = GS_Vict;
		
		R.width = ScoreEsc.x * Screen.width /100;
		R.height = ScoreEsc.y * Screen.height /100;
		
		R.x = ScorePos.x * Screen.width / 100;
		R.y = ScorePos.y * Screen.height / 100;
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)
			R.x = (Screen.width) - R.x - R.width;
		
		GUI.Box(R, "GANADOR" + '\n' + "DINERO: " + DatosPartida.PtsGanador);
		
	}
	
	void SetGUIPerdedor()
	{
		GUI.skin = GS_Derr;
		
		R.width = ScoreEsc.x * Screen.width /100;
		R.height = ScoreEsc.y * Screen.height /100;
		
		R.x = ScorePos.x * Screen.width / 100;
		R.y = ScorePos.y * Screen.height / 100;
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
			R.x = (Screen.width) - R.x - R.width;
		
		GUI.Box(R, "PERDEDOR" + '\n' + "DINERO: " + DatosPartida.PtsPerdedor);
	}
	*/
	
	void SetGanador()
	{
		switch(DatosPartida.LadoGanadaor)
		{
		case DatosPartida.Lados.Der:
			
			Ganadores[1].SetActive(true);
			Ganadores[0].SetActive(false);
			
			break;
			
		case DatosPartida.Lados.Izq:

				Ganadores[0].SetActive(true);
				Ganadores[1].SetActive(false);

				break;
		}
	}
	
	void SetDinero()
	{
		
		
		
		//IZQUIERDA
		R.x = DineroPos[0].x * Screen.width/100;
		R.y = DineroPos[0].y * Screen.height/100;
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)//izquierda
		{
			if(!PrimerImaParp)//para que parpadee
				player1.text="$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
		}
		else
		{
			player1.text="$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
		}
		
		
		
		//DERECHA
		R.x = DineroPos[1].x * Screen.width/100;
		R.y = DineroPos[1].y * Screen.height/100;
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)//derecha
		{
			if(!PrimerImaParp)//para que parpadee
				player2.text="$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
		}
		else
		{
			player2.text="$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
		}
		
	}
	
	void SetCartelGanador()
	{

	}
	
	public void DesaparecerGUI()
	{
		ActivadoAnims = false;
		Tempo = -100;
	}
}
