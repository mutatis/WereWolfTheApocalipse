using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class LoadingSceneBar : MonoBehaviour
{
    public string cena;

    /*public Text text;

    void Start()
    {
        StartCoroutine(AsynchronousLoad(cena));
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene(cena);
    }

    /*IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            text.text = ("Loading progress: " + (progress * 100) + "%");
                //progress * 100).ToString();
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                Debug.Log("Press a key to start");
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }*/
}
