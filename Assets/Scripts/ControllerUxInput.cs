using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
public class ControllerUxInput : MonoBehaviour {

    public Button[] clickAbles;

    PlayerIndex pIdx = PlayerIndex.One;
    GamePadState state;
    GamePadState prevState;
    public int activeIndex;
    public bool UpDown;
    public bool LeftRight;
    public Sprite selectImage;
    
    // Use this for initialization
    void Start()
    {
       
        
    }

    int downCount =0;
    public bool getButtonDown()
    {
        
        if(state.Buttons.A == ButtonState.Pressed)
        {
            downCount++;
            if (downCount >= 1)
            {
                return false;
            }
            return true;            
        }
        downCount = 0;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(pIdx);
 
        if (UpDown)
        {
            if (prevState.ThumbSticks.Left.Y == 0 && state.ThumbSticks.Left.Y > 0)
            {

                activeIndex--;
            }
            if (prevState.ThumbSticks.Left.Y == 0 && state.ThumbSticks.Left.Y < 0)
            {

                activeIndex++;
            }
        }

        if (LeftRight)
        {
            if (prevState.ThumbSticks.Left.X == 0 && state.ThumbSticks.Left.X > 0)
            {

                activeIndex++;
            }
            if (prevState.ThumbSticks.Left.X == 0 && state.ThumbSticks.Left.X < 0)
            {

                activeIndex--;
            }
        }


        if (activeIndex == clickAbles.Length)
        {
            activeIndex = 0;
        }
        if (activeIndex == -1)
        {
            activeIndex = clickAbles.Length -1;
        }

        if (getButtonDown())
        {
            
            clickAbles[activeIndex].onClick.Invoke();
        }
       
        

        clickAbles[activeIndex].Select();
       
    }
}
