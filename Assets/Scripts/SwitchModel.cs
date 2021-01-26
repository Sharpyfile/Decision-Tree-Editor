using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchModel : MonoBehaviour
{
    public enum CharacterModel
    {
        Player,
        Dragon
    }
    // Start is called before the first frame update
    // x = x, y = 10, z = z - 10
    public Camera camera;
    private PlayerMovement player;
    private DragonMovement dragon;

    public CharacterModel model = CharacterModel.Player;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        player = GetComponentInChildren<PlayerMovement>();
        dragon = GetComponentInChildren<DragonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (model == CharacterModel.Player)
                model = CharacterModel.Dragon;
            else
                model = CharacterModel.Player;
        }

        if (model == CharacterModel.Player)
        {
            camera.transform.position = new Vector3(player.transform.position.x, 20.0f, player.transform.position.z - 10.0f);
        }
        else
        {
            camera.transform.position = new Vector3(dragon.transform.position.x, 20.0f, dragon.transform.position.z - 10.0f);
        }
    }
}
