using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
public class ControllerUxInput : MonoBehaviour {

    

    PlayerIndex pIdx = PlayerIndex.One;
    GamePadState state;
    GamePadState prevState;
    public int activeIndex;
    public bool UpDown;
    public bool LeftRight;
    //public Sprite selectImage;

    public Button currentButton;
    public Selectable selectable;

    // Use this for initialization
    void Start()
    {

        selectable = currentButton;
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
        //Vector3 joystickDir = new Vector3(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 0);
        //selectable = currentButton.FindSelectable(joystickDir);
        //currentButton = (Button)selectable;


        
            if (prevState.ThumbSticks.Left.Y == 0 && state.ThumbSticks.Left.Y > 0)
            {
                Selectable trySelect = currentButton.FindSelectableOnUp();
                
                if (trySelect != null)
                {
                    selectable = trySelect;
                    currentButton = (Button)selectable;
                }
            }
            if (prevState.ThumbSticks.Left.Y == 0 && state.ThumbSticks.Left.Y < 0)
            {
                Selectable trySelect = currentButton.FindSelectableOnDown();

                if (trySelect != null)
                {
                    selectable = trySelect;
                    currentButton = (Button)selectable;
                }
            }
        

        
            if (prevState.ThumbSticks.Left.X == 0 && state.ThumbSticks.Left.X > 0)
            {
                 Selectable trySelect = currentButton.FindSelectableOnRight();

                 if (trySelect != null)
                 {
                     selectable = trySelect;
                     currentButton = (Button)selectable;
                 }
            
            }
            if (prevState.ThumbSticks.Left.X == 0 && state.ThumbSticks.Left.X < 0)
            {
                 Selectable trySelect = currentButton.FindSelectableOnLeft();

                  if (trySelect != null)
                  {
                       selectable = trySelect;
                       currentButton = (Button)selectable;
                  }

            }
       



        if (getButtonDown())
        {
            
            currentButton.onClick.Invoke();
        }
        
        currentButton.Select();
    }
}
