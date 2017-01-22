using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
	public GameObject Menu;
	public GameObject Credits;

	public void onBeginPress()
	{
		SceneManager.LoadScene("Waves");
	}
	public  void onCreditsPress()
	{
		Menu.SetActive(false);
		Credits.SetActive(true);
	}
	public void offCreditsPress()
	{
		Menu.SetActive(true);
		Credits.SetActive(false);
	}
	public void onExitPress()
	{
		Application.Quit();
	}
}
