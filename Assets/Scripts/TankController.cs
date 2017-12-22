using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;
public class TankController : MonoBehaviour {

    PlayerIndex pIdx;
    GamePadState state;
    GamePadState prevState;
    public int playerNum;
    public float moveX;
    public float moveY;

    public float rotX;
    public float rotY;

    public bool fire;
    public bool autoFire;
	// Use this for initialization
	void Start ()
    {
        switch(playerNum)
        {
            case 1:
                pIdx = PlayerIndex.One;
                break;
            case 2:
                pIdx = PlayerIndex.Two;
                break;
            case 3:
                pIdx = PlayerIndex.Three;
                break;
            case 4:
                pIdx = PlayerIndex.Four;
                break;
        }
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        prevState = state;
        state = GamePad.GetState(pIdx);
        moveX = state.ThumbSticks.Left.X;
        moveY = state.ThumbSticks.Left.Y;

        rotX = state.ThumbSticks.Right.X;
        rotY = state.ThumbSticks.Right.Y;

        if(state.Triggers.Right == 1 && prevState.Triggers.Right <= 0.8f)
        {
            fire = true;
        }
        else { fire = false; }

        if (state.Triggers.Left == 1 )
        {
            autoFire = true;
        }
        else { autoFire = false; }

        if (state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released)
        {
            SceneManager.LoadScene("Menu");
        }

	}
}
