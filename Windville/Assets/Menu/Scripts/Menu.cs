using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	private AudioListener[] AudioListeners;
	public Texture TexturaCreditos, TexturaFundo, TexturaGraficos;
	private bool EstaMenu, EstaGraficos, EstaCreditos;
	public GUIStyle EstiloBotoesPrincipais, EstiloBotoesGraficos;
	private string ResolucaoLargura = "1920", ResolucaoAltura = "1080";
	public Font Fonte;
	public int tamanhoLetra = 10;

	void Start ()
	{
		EstaMenu = true;
		Cursor.visible = true;
		Time.timeScale = 1;
	}

	void OnGUI ()
	{
		GUI.skin.font = Fonte;
		EstiloBotoesPrincipais.fontSize = Screen.height / 100 * tamanhoLetra;
		EstiloBotoesGraficos.fontSize = Screen.height / 100 * tamanhoLetra;

		//Menu Principal
		if (EstaMenu == true) {
			GUI.skin.button = EstiloBotoesPrincipais;
			GUI.DrawTexture (new Rect (Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 2, Screen.width, Screen.height), TexturaFundo);
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, Screen.width / 8, Screen.height / 14), "Play")) {
				SceneManager.LoadScene ("Tutorial");
			}

			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + Screen.height / 12, Screen.width / 8, Screen.height / 14), "Options")) {
				EstaMenu = false;
				EstaGraficos = true;
			}

			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + Screen.height / 6, Screen.width / 8, Screen.height / 14), "Credits")) {
				EstaMenu = false;
				EstaCreditos = true;
			}

			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + Screen.height / 4, Screen.width / 8, Screen.height / 14), "Quit")) {
				Application.Quit ();
			}
		}

		//Menu Gráficos
		if (EstaGraficos == true) {
			GUI.skin.button = EstiloBotoesGraficos;
			GUI.DrawTexture (new Rect (Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 2, Screen.width, Screen.height), TexturaGraficos);
			//Botões
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 2.2f, Screen.height / 2 + Screen.height / 2.5f, Screen.width / 8, Screen.height / 14), "Back")) {
				EstaMenu = true;
				EstaGraficos = false;
			}
			//Resolução                              
			ResolucaoLargura = GUI.TextField (new Rect (Screen.width / 2 - Screen.width / 3, Screen.height / 2, Screen.width / 8, Screen.height / 14), ResolucaoLargura);
			ResolucaoAltura = GUI.TextField (new Rect (Screen.width / 2 - Screen.width / 3, Screen.height / 2 + Screen.height / 12, Screen.width / 8, Screen.height / 14), ResolucaoAltura);

			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 3, Screen.height / 2 + Screen.height / 6, Screen.width / 8, Screen.height / 14), "Apply")) {
				Screen.SetResolution (int.Parse (ResolucaoLargura), int.Parse (ResolucaoAltura), true);
			}

			//Tela cheia
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 14, Screen.height / 2, Screen.width / 8, Screen.height / 14), "Switch")) {
				Screen.fullScreen = !Screen.fullScreen;
			}
		}

		//Menu Créditos
		if (EstaCreditos == true) {
			GUI.skin.button = EstiloBotoesGraficos;
			GUI.DrawTexture (new Rect (Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 2, Screen.width, Screen.height), TexturaCreditos);
			//Botões
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 2.2f, Screen.height / 2 + Screen.height / 2.5f, Screen.width / 8, Screen.height / 14), "Back")) {
				EstaMenu = true;
				EstaCreditos = false;

			}

		}
	}
}
