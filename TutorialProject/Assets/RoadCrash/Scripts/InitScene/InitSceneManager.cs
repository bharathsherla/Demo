using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitSceneManager : MonoBehaviour
{
	#region Variables
	[SerializeField]private Text loadingText;
	[SerializeField] private Slider slider;
	[SerializeField] private string sceneToLoad;
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void Start()
	{
		StartCoroutine(LoadScene());
	}

	private IEnumerator LoadScene()
	{
		yield return null;
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		//Don't let the Scene activate until you allow it to
		asyncOperation.allowSceneActivation = false;
		Debug.Log("Pro :" + asyncOperation.progress);
		//When the load is still in progress, output the Text and progress bar
		while (!asyncOperation.isDone)
		{
			loadingText.text = "Loading ... " + (asyncOperation.progress * 100) + "%";
			slider.value = asyncOperation.progress;
			// Check if the load has finished
			if (asyncOperation.progress >= 0.9f)
			{
				asyncOperation.allowSceneActivation = true;
			}

			yield return null;
		}
	}
	#endregion
}
