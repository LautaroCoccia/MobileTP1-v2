using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Obstaculos { desactivados, activados }

    public Obstaculos OBS = Obstaculos.desactivados;

    public static GameManager _instanceGameManager;
    private void Awake()
    {
        if (_instanceGameManager == null)
        {
            _instanceGameManager = this;
        }
        else if (_instanceGameManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void StartCorrutineLoadScene(string sceneTittle)
    {
        StartCoroutine(Scene(sceneTittle));
    }
    IEnumerator Scene(string name)
    {
        
        SceneManager.LoadSceneAsync(name);
        yield return new WaitForSecondsRealtime(1f);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
    public void OnWhithObs()
    {
        OBS = Obstaculos.activados;
    }

    public void WithOutObs()
    {
        OBS = Obstaculos.desactivados;
    }
}


