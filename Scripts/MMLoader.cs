using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}