using UnityEngine;
using System.Collections;
using TMPro;
/// <summary>
/// clase encargada de TODA la visualizacion
/// de cada player, todo aquello que corresconda a 
/// cada seccion de la pantalla independientemente
/// </summary>
public class Visualizacion : MonoBehaviour 
{
	public enum Lado{Izq, Der}
	public Lado LadoAct;
	
	ControlDireccion Direccion;
	Player Pj;
	
	public enum Mode{single, multi }
	public Mode gameMode;

	//las distintas camaras
	public Camera CamCalibracion;
	public Camera CamConduccion;
	public Camera CamDescarga;
	
	
	//EL DINERO QUE SE TIENE
	public Vector2[]DinPos;
	public Vector2 DinEsc = Vector2.zero;
	
	public TextMeshProUGUI GS_Din;
	
	//EL VOLANTE
	public Vector2[] VolantePos;
	public float VolanteEsc = 0;
	
	
	
	//PARA EL INVENTARIO
	public Vector2[] FondoPos;
	public Vector2 FondoEsc = Vector2.zero;
	
	//public Vector2 SlotsEsc = Vector2.zero;
	//public Vector2 SlotPrimPos = Vector2.zero;
	//public Vector2 Separacion = Vector2.zero;
	
	//public int Fil = 0;
	//public int Col = 0;
	
	public Texture2D TexturaVacia;//lo que aparece si no hay ninguna bolsa
	public Texture2D TextFondo;
	
	public float Parpadeo = 0.8f;
	public float TempParp = 0;
	public bool PrimIma = true;
	
	public GameObject[] TextInvIzq;
	public GameObject[] TextInvDer;
	
	public GUISkin GS_Inv;
	
	//BONO DE DESCARGA
	public Vector2 BonusPos = Vector2.zero;
	public Vector2 BonusEsc = Vector2.zero;
	
	public Color32 ColorFondoBolsa;	
	public Vector2 ColorFondoPos = Vector2.zero;
	public Vector2 ColorFondoEsc = Vector2.zero;
	
	public Vector2 ColorFondoFondoPos = Vector2.zero;
	public Vector2 ColorFondoFondoEsc = Vector2.zero;
	
	public GUISkin GS_FondoBonusColor;
	public GUISkin GS_FondoFondoBonusColor;
	public GUISkin GS_Bonus;
	
	
	//CALIBRACION MAS TUTO BASICO
	public Vector2 ReadyPos = Vector2.zero;
	public Vector2 ReadyEsc = Vector2.zero;
	public Texture2D[] ImagenesDelTuto;
	public float Intervalo = 0.8f;//tiempo de cada cuanto cambia de imagen
	float TempoIntTuto = 0;
	int EnCurso = -1;
	public Texture2D ImaEnPosicion;
	public Texture2D ImaReady;
	public GUISkin GS_TutoCalib;	
	
	//NUMERO DEL JUGADOR
	public Texture2D TextNum1; 
	public Texture2D TextNum2;
	public GameObject Techo;
	
	Rect R;
	
	//------------------------------------------------------------------//
	
	// Use this for initialization
	void Start () 
	{
		TempoIntTuto = Intervalo;
		Direccion = GetComponent<ControlDireccion>();
		Pj = GetComponent<Player>();
	}
	
	// Update is called once per frame
	
	
	void Update()
	{	
		switch(Pj.EstAct)
		{
			
			
		case Player.Estados.EnConduccion:
			//inventario
			SetInv3();
			//contador de dinero
			SetDinero();
			//el volante
			break;
			
			
			
		case Player.Estados.EnDescarga:
			//inventario
			SetInv3();
			//el bonus
			SetBonus();
			//contador de dinero
			SetDinero();			
			break;
			
			
		case Player.Estados.EnCalibracion:
			//SetCalibr();
			break;
			
			
		case Player.Estados.EnTutorial:
			SetInv3();
			SetTuto();
			break;
		}
		
		GUI.skin = null;
	}
	
	//--------------------------------------------------------//
	
	public void CambiarACalibracion()
	{
		CamCalibracion.enabled = true;
		CamConduccion.enabled = false;
		CamDescarga.enabled = false;

		switch(gameMode)
        {
			case Mode.single:
				CamCalibracion.enabled = false;
				break;
			case Mode.multi:
				CamCalibracion.enabled = true;
				break;
        }
	}
	
	public void CambiarATutorial()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = true;
		CamDescarga.enabled = false;
	}
	
	public void CambiarAConduccion()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = true;
		CamDescarga.enabled = false;
	}
	
	public void CambiarADescarga()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = false;
		CamDescarga.enabled = true;
	}
	
	//---------//
	
	public void SetLado(Lado lado)
	{
		LadoAct = lado;
		
		Rect r = new Rect();
		r.width = CamConduccion.rect.width;
		r.height = CamConduccion.rect.height;
		r.y = CamConduccion.rect.y;
		
		switch (lado)
		{
		case Lado.Der:
			r.x = 0.5f;
			break;
			
			
		case Lado.Izq:
			r.x = 0;
			break;
		}
		
		CamCalibracion.rect = r;
		CamConduccion.rect = r;
		CamDescarga.rect = r;
		
		if(LadoAct == Visualizacion.Lado.Izq)
		{
			Techo.GetComponent<Renderer>().material.mainTexture = TextNum1;
		}
		else
		{
			Techo.GetComponent<Renderer>().material.mainTexture = TextNum2;
		}
	}
	
	void SetBonus()
	{
		if(Pj.ContrDesc.PEnMov != null)
		{
			//el fondo
			GUI.skin = GS_FondoFondoBonusColor;
			
			R.width = ColorFondoFondoEsc.x *Screen.width /100;
			R.height = ColorFondoFondoEsc.y *Screen.height /100;
			R.x = ColorFondoFondoPos.x *Screen.width /100;
			R.y = ColorFondoFondoPos.y *Screen.height /100;
			if(LadoAct == Visualizacion.Lado.Der)
				R.x += (Screen.width)/2;			
			GUI.Box(R, "");
			
			
			//el fondo
			GUI.skin = GS_FondoBonusColor;
			
			R.width = ColorFondoEsc.x *Screen.width /100;
			R.height = (ColorFondoEsc.y *Screen.height /100) * (Pj.ContrDesc.Bonus / (int)Pallet.Valores.Valor2);
			R.x = ColorFondoPos.x *Screen.width /100;
			R.y = (ColorFondoPos.y *Screen.height /100) - R.height;
			if(LadoAct == Visualizacion.Lado.Der)
				R.x += (Screen.width)/2;			
			GUI.Box(R, "");
			
			
			//la bolsa
			GUI.skin = GS_Bonus;
		
			R.width = BonusEsc.x *Screen.width /100;
			R.height = R.width /2;
			R.x = BonusPos.x *Screen.width /100;
			R.y = BonusPos.y *Screen.height /100;
			if(LadoAct == Visualizacion.Lado.Der)
				R.x += (Screen.width)/2;
			GUI.Box(R, "     $" + Pj.ContrDesc.Bonus.ToString("0"));
		}
	}
	
	void SetDinero()
	{
		//GUI.skin = GS_Din;
		
		R.width = DinEsc.x *Screen.width /100;
		R.height = DinEsc.y *Screen.height /100;
		R.x = DinPos[0].x *Screen.width /100;
		R.y = DinPos[0].y *Screen.height /100;
		if(LadoAct == Visualizacion.Lado.Der)
			R.x = DinPos[1].x *Screen.width /100;
			//R.x = (Screen.width) - (Screen.width/2) - R.x;
		GUI.Box(R, "$" + PrepararNumeros(Pj.Dinero));
	}
	
	void SetCalibr()
	{
		GUI.skin = GS_TutoCalib;
		
		R.width = ReadyEsc.x *Screen.width /100;
		R.height = ReadyEsc.y *Screen.height /100;
		R.x = ReadyPos.x *Screen.width /100;
		R.y = ReadyPos.y *Screen.height /100;
		if(LadoAct == Visualizacion.Lado.Der)
			R.x = (Screen.width) - R.x - R.width;
		
		switch(Pj.ContrCalib.EstAct)
		{
		case ContrCalibracion.Estados.Calibrando:
			
			//pongase en posicion para iniciar
			GS_TutoCalib.box.normal.background = ImaEnPosicion;			
			GUI.Box(R,"");
			
			break;
			
		case ContrCalibracion.Estados.Tutorial:
			//tome la bolsa y depositela en el estante
			
			TempoIntTuto += T.GetDT();
			if(TempoIntTuto >= Intervalo)
			{
				TempoIntTuto = 0;
				if(EnCurso + 1 < ImagenesDelTuto.Length)
					EnCurso++;
				else
					EnCurso = 0;
			}
			GS_TutoCalib.box.normal.background = ImagenesDelTuto[EnCurso];
			
			GUI.Box(R,"");
			
			break;
			
		case ContrCalibracion.Estados.Finalizado:
			//esperando al otro jugador		
			GS_TutoCalib.box.normal.background = ImaReady;
			GUI.Box(R,"");
			
			break;
		}
	}
	
	void SetTuto()
	{
		if(Pj.ContrTuto.Finalizado)
		{
			GUI.skin = GS_TutoCalib;
			
			R.width = ReadyEsc.x *Screen.width /100;
			R.height = ReadyEsc.y *Screen.height /100;
			R.x = ReadyPos.x *Screen.width /100;
			R.y = ReadyPos.y *Screen.height /100;
			if(LadoAct == Visualizacion.Lado.Der)
				R.x = (Screen.width) - R.x - R.width;
			
			GUI.Box(R,"ESPERANDO AL OTRO JUGADOR");
		}
	}
	
	/*
	void SetInv()
	{
		GUI.skin = GS_Inv;
		
		//fondo
		GS_Inv.box.normal.background = TextFondo;
		R.width = FondoEsc.x * Screen.width /100;
		R.height = FondoEsc.y * Screen.height /100;
		R.x = FondoPos.x * Screen.width /100;
		R.y = FondoPos.y * Screen.height /100;
		if(LadoAct == Visualizacion.Lado.Der)
			R.x = (Screen.width) - R.x - R.width;
		GUI.Box(R,"");
		
		//bolsas
		R.width = SlotsEsc.x * Screen.width /100;
		R.height = SlotsEsc.y * Screen.height /100;
		int contador = 0;
		for(int j = 0; j < Fil; j++)
		{
			for(int i = 0; i < Col; i++)
			{
				R.x = SlotPrimPos.x * Screen.width / 100 + Separacion.x * i * Screen.width / 100;
				R.y = SlotPrimPos.y * Screen.height / 100 + Separacion.y * j * Screen.height / 100;
				if(LadoAct == Visualizacion.Lado.Der)
					R.x = (Screen.width) - R.x - R.width;
				
				if(contador < Pj.Bolasas.Length )//&& Pj.Bolasas[contador] != null)
				{
					if(Pj.Bolasas[contador]!=null)
						GS_Inv.box.normal.background = Pj.Bolasas[contador].ImagenInventario;
					else
						GS_Inv.box.normal.background = TexturaVacia;				
				}
				else
				{
					GS_Inv.box.normal.background = TexturaVacia;
				}
				GUI.Box(R,"");
				
				contador++;
			}
		}
	}
	*/
	
	
	
	
	
	void SetInv3()
	{
		GUI.skin = GS_Inv;
		
		R.width = FondoEsc.x * Screen.width /100;
		R.height = FondoEsc.y * Screen.width /100;
		R.x = FondoPos[0].x * Screen.width /100;
		R.y = FondoPos[0].y * Screen.height /100;
		
		int contador = 0;
		for(int i = 0; i < 3; i++)
		{
			if(Pj.Bolasas[i]!=null)
				contador++;
		}
		
		if(LadoAct == Visualizacion.Lado.Der)
		{
			//R.x = (Screen.width) - (Screen.width/2) - R.x;
			R.x = FondoPos[1].x * Screen.width /100;
			
			if(contador == 0)
            {
				
				TextInvDer[0].SetActive(true);
				TextInvDer[1].SetActive(false);
				TextInvDer[2].SetActive(false);
				TextInvDer[3].SetActive(false);
				TextInvDer[4].SetActive(false);
			}
			else if(contador < 3)
			{

				TextInvDer[contador].SetActive(true);
				for (int i = 0; i < contador; i++)
				{
					if (contador != i)
					{
						TextInvDer[i].SetActive(false);
					}
				}
			}

				
			else
			{
				TempParp += T.GetDT();
				
				if(TempParp >= Parpadeo)
				{
					TempParp = 0;
					if(PrimIma)
						PrimIma = false;
					else
						PrimIma = true;
				}
				
				if(PrimIma)
				{
					TextInvDer[3].SetActive(true);
					TextInvDer[0].SetActive(false);
					TextInvDer[1].SetActive(false);
					TextInvDer[2].SetActive(false);
					TextInvDer[4].SetActive(false);
				}
				else
				{
					TextInvDer[4].SetActive(true);
					TextInvDer[0].SetActive(false);
					TextInvDer[1].SetActive(false);
					TextInvDer[2].SetActive(false);
					TextInvDer[3].SetActive(false);
				}
				
			}
		}
		else
		{
			if (contador == 0)
			{

				TextInvIzq[0].SetActive(true);
				TextInvIzq[1].SetActive(false);
				TextInvIzq[2].SetActive(false);
				TextInvIzq[3].SetActive(false);
				TextInvIzq[4].SetActive(false);
			}
			else if (contador < 3)
            {
				TextInvIzq[contador].SetActive(true);
				for(int i = 0; i < contador; i++)
                {
					if(contador!=i)
                    {
						TextInvIzq[i].SetActive(false);
					}
                }
			}
				//GS_Inv.box.normal.background = TextInvIzq[contador];
			else
			{
				TempParp += T.GetDT();
				
				if(TempParp >= Parpadeo)
				{
					TempParp = 0;
					if(PrimIma)
						PrimIma = false;
					else
						PrimIma = true;
				}
				
				if(PrimIma)
				{
					TextInvIzq[3].SetActive(true);
					TextInvIzq[0].SetActive(false);
					TextInvIzq[1].SetActive(false);
					TextInvIzq[2].SetActive(false);
					TextInvIzq[4].SetActive(false);
				}
				else
				{
					TextInvIzq[4].SetActive(true);
					TextInvIzq[0].SetActive(false);
					TextInvIzq[1].SetActive(false);
					TextInvIzq[2].SetActive(false);
					TextInvIzq[3].SetActive(false);
					//GS_Inv.box.normal.background = TextInvIzq[4];
				}
			}
		}
	}
	
	public string PrepararNumeros(int dinero)
	{
		string strDinero = dinero.ToString();
		string res = "";
		
		if(dinero < 1)//sin ditero
		{
			res = "";
		}else if(strDinero.Length == 6)//cientos de miles
		{
			for(int i = 0; i < strDinero.Length; i++)
			{
				res += strDinero[i];
				
				if(i == 2)
				{
					res += ".";
				}
			}
		}else if(strDinero.Length == 7)//millones
		{
			for(int i = 0; i < strDinero.Length; i++)
			{
				res += strDinero[i];
				
				if(i == 0 || i == 3)
				{
					res += ".";
				}
			}
		}
		
		return res;
	}
	
	
	
}
