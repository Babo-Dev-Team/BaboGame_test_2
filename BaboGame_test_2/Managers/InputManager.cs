using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BaboGame_test_2
{
    public class InputManager
    {
        //private InputKeys activeKeys;
        private bool upCtrlActive;
        private bool downCtrlActive;
        private bool leftCtrlActive;
        private bool rightCtrlActive;
        private bool leftMouseClick;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        private Vector2 mousePosition;

        public InputManager(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            InputKeys.Up = upKey;
            InputKeys.Down = downKey;
            InputKeys.Left = leftKey;
            InputKeys.Right = rightKey;
            upCtrlActive = false;
            downCtrlActive = false;
            leftCtrlActive = false;
            rightCtrlActive = false;
            leftMouseClick = false;
            previousMouseState = Mouse.GetState();
            currentMouseState = previousMouseState;
            mousePosition = Vector2.Zero;
        }

        public bool UpCtrlActive()
        {
            return upCtrlActive;
        }

        public bool DownCtrlActive()
        {
            return downCtrlActive;
        }

        public bool LeftCtrlActive()
        {
            return leftCtrlActive;
        }

        public bool RightCtrlActive()
        {
            return rightCtrlActive;
        }

        public bool LeftMouseClick()
        {
            return leftMouseClick;
        }

        public void detectKeysPressed()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(InputKeys.Up))
            {
                upCtrlActive = true;
            }
            else upCtrlActive = false;

            if (keyboardState.IsKeyDown(InputKeys.Down))
            {
                downCtrlActive = true;
            }
            else downCtrlActive = false;

            if (keyboardState.IsKeyDown(InputKeys.Left))
            {
                leftCtrlActive = true;
            }
            else leftCtrlActive = false;

            if (keyboardState.IsKeyDown(InputKeys.Right))
            {
                rightCtrlActive = true;
            }
            else rightCtrlActive = false;
        }

        public void DetectMouseClicks()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                leftMouseClick = true;
            }
            else leftMouseClick = false;
        }

        public bool LeftMousePressed()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public Vector2 GetMousePosition()
        {
            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;
            return mousePosition;
        }
    }
}
