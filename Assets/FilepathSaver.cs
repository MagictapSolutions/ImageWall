using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FilepathSaver : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField path;
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("path"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void savePath()
    {
        PlayerPrefs.SetString("path", path.text);
        SceneManager.LoadScene(1);
    }
}
