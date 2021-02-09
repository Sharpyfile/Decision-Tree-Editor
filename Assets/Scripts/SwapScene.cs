using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            SceneManager.LoadScene("DecisionTreeSceneWithNativeTraitsAllInLine");

        if (Input.GetKey(KeyCode.Alpha2))
            SceneManager.LoadScene("DecisionTreeSceneWithNativeTraits");

        if (Input.GetKey(KeyCode.Alpha3))
            SceneManager.LoadScene("DecisionTreeSceneWithoutNativeTraits");

        if (Input.GetKey(KeyCode.Alpha4))
            SceneManager.LoadScene("DecisionTreeSceneWithPandaBT");

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
