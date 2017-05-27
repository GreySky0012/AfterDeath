using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public enum SceneList { Demo, First, Second, Third, Main };//in order to load scene by id

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

    /// <summary>
    /// to show a dialog message box
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    public void MessageBox(string title, string content)
    {
        //未完成
    }
    
    /// <summary>
    /// show exceptions with message box
    /// </summary>
    /// <param name="ex"></param>
    public void ShowError(MyException ex)
    {
        MessageBox("Error", "Error: "+ex._exceptionId);
    }

    /// <summary>
    /// save the inform of player
    /// </summary>
    public void Save()
    {
        Saver.Save(_player._info);
    }
}