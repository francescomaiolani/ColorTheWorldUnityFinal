using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour {

    public string sceneToChange;

    public GameObject mask, orangePanel;

    private void Start()
    {

        mask.SetActive(true);
        orangePanel.SetActive(true);
        mask.GetComponent<Animator>().SetTrigger("SceneStart");
        Invoke("DeactivateSceneChange", 0.3f);
#if UNITY_EDITOR
        if (GameObject.FindObjectOfType<GameController>() == null)
            SetSceneToChange("Home");
#endif

    }

    void DeactivateSceneChange() {
        mask.SetActive(false);
        orangePanel.SetActive(false);
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneToChange);
    }

    public void SetSceneToChange(string sceneName) {
        if (mask != null && orangePanel != null)
        {
            sceneToChange = sceneName;
            mask.SetActive(true);
            orangePanel.SetActive(true);
            mask.GetComponent<Animator>().SetTrigger("SceneEnd");

            Invoke("ChangeScene", 0.40f);
        }

        else
            ChangeScene();
       
    }

}
