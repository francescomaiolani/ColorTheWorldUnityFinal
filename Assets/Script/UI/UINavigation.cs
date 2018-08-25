using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour {

    public string sceneToChange;
    GameController gameController;

    public GameObject mask, orangePanel;
    public Color32[] panelColor;

    private void Start()
    {

        if ( GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        mask.SetActive(true);
        orangePanel.SetActive(true);
        if (gameController != null)
        {
            if (gameController.lastSceneTransitionColorUsed.Equals(null))
                orangePanel.GetComponent<SpriteRenderer>().color = panelColor[Random.Range(0, panelColor.Length)];
            else
                orangePanel.GetComponent<SpriteRenderer>().color = gameController.lastSceneTransitionColorUsed;
        }
        else {
            orangePanel.GetComponent<SpriteRenderer>().color = panelColor[Random.Range(0, panelColor.Length)];
        }


        mask.GetComponent<Animator>().SetTrigger("SceneStart");
        Invoke("DeactivateSceneChange", 0.4f);
#if UNITY_EDITOR
       // if (GameObject.FindObjectOfType<GameController>() == null)
            //SetSceneToChange("Home");
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
            Color32 chosenColor = panelColor[Random.Range(0, panelColor.Length)];
            orangePanel.GetComponent<SpriteRenderer>().color = chosenColor;
            if (gameController != null)
                 gameController.lastSceneTransitionColorUsed = chosenColor;
            
            mask.GetComponent<Animator>().SetTrigger("SceneEnd");

            Invoke("ChangeScene", 0.5f);
        }

        else
            ChangeScene();
       
    }

}
