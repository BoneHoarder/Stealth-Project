using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string sceneName;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(string sceneName){
        sceneName= sceneName.Trim();
        SceneManager.LoadScene(sceneName);
   }
}
