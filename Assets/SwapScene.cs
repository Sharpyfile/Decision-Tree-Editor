using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
            SceneManager.LoadScene("DecisionTreeSceneWithNativeTraitsAllInLine");

        if (Input.GetKey(KeyCode.U))
            SceneManager.LoadScene("DecisionTreeSceneWithNativeTraits");
    }
}
