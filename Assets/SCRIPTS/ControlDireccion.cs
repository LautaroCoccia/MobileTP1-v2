using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ControlDireccion : MonoBehaviour 
{
	public enum TipoInput { Mouse, Kinect, AWSD, Arrows, VirtualJoystic }
	private TipoInput InputAct = ControlDireccion.TipoInput.Mouse;

	public Player player;
	public Transform ManoDer;
	public Transform ManoIzq;

	public Joystick Joy;

	public float MaxAng = 90;
	public float DesSencibilidad = 90;

	public bool irDerecha = false;
	public bool irIzquierda = false;

	float Giro = 0;

	public enum Sentido { Der, Izq }
	Sentido DirAct;

	public bool Habilitado = true;
	//float Diferencia;

	//---------------------------------------------------------//

	// Use this for initialization
	void Start()
	{
#if UNITY_EDITOR
		if( player.IdPlayer== 0 )
        {
			InputAct = TipoInput.AWSD;
		}
		if (player.IdPlayer==1)
        {
			InputAct = TipoInput.Arrows;
        }
#elif UNITY_ANDROID
		if(player.IdPlayer== 0 )
		{
			InputAct = TipoInput.VirtualJoystic;
		}
		if(player.IdPlayer== 1)
		{
			InputAct = TipoInput.VirtualJoystic;
		}
#endif
	}


	// Update is called once per frame
	void Update()
	{
		switch (InputAct)
		{
			case TipoInput.Mouse:
				if (Habilitado)
					gameObject.SendMessage("SetGiro", MousePos.Relation(MousePos.AxisRelation.Horizontal));//debe ser reemplanado
				break;

			case TipoInput.Kinect:

				if (ManoIzq.position.y > ManoDer.position.y)
				{
					DirAct = Sentido.Der;
				}
				else
				{
					DirAct = Sentido.Izq;
				}

				switch (DirAct)
				{
					case Sentido.Der:
						if (Angulo() <= MaxAng)
							Giro = Angulo() / (MaxAng + DesSencibilidad);
						else
							Giro = 1;

						if (Habilitado)
							gameObject.SendMessage("SetGiro", Giro);//debe ser reemplanado

						break;

					case Sentido.Izq:
						if (Angulo() <= MaxAng)
							Giro = (Angulo() / (MaxAng + DesSencibilidad)) * (-1);
						else
							Giro = (-1);

						if (Habilitado)
							gameObject.SendMessage("SetGiro", Giro);//debe ser reemplanado

						break;
				}
				break;
			case TipoInput.AWSD:
				if (Habilitado)
				{
					if (Input.GetKey(KeyCode.A))
					{
						gameObject.SendMessage("SetGiro", -1);
					}
					if (Input.GetKey(KeyCode.D))
					{
						gameObject.SendMessage("SetGiro", 1);
					}
				}
				break;
			case TipoInput.Arrows:
				if (Habilitado)
				{
					if (Input.GetKey(KeyCode.LeftArrow))
					{
						gameObject.SendMessage("SetGiro", -1);
					}
					if (Input.GetKey(KeyCode.RightArrow))
					{
						gameObject.SendMessage("SetGiro", 1);
					}
				}
				break;
			case TipoInput.VirtualJoystic:
				if(Habilitado)
                {
					gameObject.SendMessage("SetGiro", Joy.Horizontal);
				}
				break;
		}
	}

	public float GetGiro()
	{
		

		return Giro;
	}

	float Angulo()
	{
		Vector2 diferencia = new Vector2(ManoDer.localPosition.x, ManoDer.localPosition.y)
						   - new Vector2(ManoIzq.localPosition.x, ManoIzq.localPosition.y);

		return Vector2.Angle(diferencia, new Vector2(1, 0));
	}

	
}
