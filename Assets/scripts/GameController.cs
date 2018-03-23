using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Work log
//Initial File - Zak Olyarnik
public class GameController : MonoBehaviour {

	public Vector3 alphaSpawn, betaSpawn;
	public AlphaController alpha;
	public BetaController beta;
	public GameObject alphaPrefab, betaPrefab;
	public CameraController alphacam;
	public CameraController betacam;
	public GameObject canvas, controllerMappings, blackScreen;

	public InputField aSpeed, bSpeed, throwSpeed, camSpeed;
	public Slider aSpeedSlider, bSpeedSlider, throwSpeedSlider, camSpeedSlider;

	// Singleton
	public static GameController instance;
	public static GameController GetInstance()
	{ return instance; }

	void Awake()
	{ instance = this; }


	private void Start()
	{
		alphaSpawn = alpha.gameObject.transform.position;
		betaSpawn = beta.gameObject.transform.position;
		blackScreen.SetActive(true);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MenuScene");
		}
	}

	public void Onoff()
	{
		canvas.SetActive(!canvas.activeSelf);
	}

	public void MappingsOnoff()
	{
		controllerMappings.SetActive(!controllerMappings.activeSelf);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void SetAlphaSpeed(string speed)
	{
		float temp = float.Parse(speed);
		aSpeedSlider.value = temp;
		alpha.speed = temp;
	}

	public void SetAlphaSpeed(float speed)
	{
		aSpeed.text = Mathf.Round(aSpeedSlider.value).ToString();
		alpha.speed = speed;
	}

	public void SetAlphaThrowSpeed(string speed)
	{
		float temp = float.Parse(speed);
		throwSpeedSlider.value = temp;
		//alpha.throwSpeed = temp;
	}

	public void SetAlphaThrowSpeed(float speed)
	{
		throwSpeed.text = Mathf.Round(throwSpeedSlider.value).ToString();
		//alpha.throwSpeed = speed;
	}

	public void SetBetaSpeed(string speed)
	{
		float temp = float.Parse(speed);
		bSpeedSlider.value = temp;
		beta.speed = temp;
	}

	public void SetBetaSpeed(float speed)
	{
		bSpeed.text = Mathf.Round(bSpeedSlider.value).ToString();
		beta.speed = speed;
	}

	public void SetCamSpeed(string speed)
	{
		float temp = float.Parse(speed);
		camSpeedSlider.value = temp;
		alphacam.camSensitivity = temp;
		betacam.camSensitivity = temp;
	}

	public void SetCamSpeed(float speed)
	{
		camSpeed.text = Mathf.Round(camSpeedSlider.value).ToString();
		alphacam.camSensitivity = speed;
		betacam.camSensitivity = speed;
	}

	public IEnumerator EndGame()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("MenuScene");
		//endScreen.SetActive(true);
	}
}
