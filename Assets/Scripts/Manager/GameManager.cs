using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public enum SceneList { Demo, First, Second, Third, Main };

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

	public Player _player;

    [HideInInspector]
    public SceneManager _scene;

    void Awake()
    {
        _player = new Player();
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        _scene = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    void Update()
    {
    }

    public void SetScene(SceneList scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_" + scene.ToString());
        _scene = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    public void MessageBox(string title, string content)
    {

    }

    public void ShowError(MyException ex)
    {
        MessageBox("Error", "Error: "+ex._exceptionId);
    }

    public void Save()
    {
        Saver.Save(_player._info);
    }
}