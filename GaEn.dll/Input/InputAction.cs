using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GaEn.Input
{
    public class InputAction
    {
        #region Enums
        public enum InputActionTypes { One, All }
        #endregion

        #region Properties
        public string Name { get; private set; }
        public InputActionTypes Type { get; set; } = InputActionTypes.One;

        private bool stateDownOld = false;
        private bool stateDownNew = false;
        #endregion

        #region Constructor
        public InputAction(string name)
        {
            Name = name;
        }
        #endregion

        #region XNA
        internal void Update(KeyboardState KB, MouseState M)
        {
            stateDownOld = stateDownNew;
            switch (Type)
            {
                case InputActionTypes.One:
                    stateDownNew = IsKeyDown(KB) || IsMouseButtonDown(M);
                    break;
                case InputActionTypes.All:
                    stateDownNew = IsKeyDown(KB) && IsMouseButtonDown(M);
                    break;
                default:
                    throw new Exception($"The supplied type {Type} is not supported for the InputAction.");
            }
        }
        #endregion

        #region Methods
        public bool IsDown() { return stateDownNew; }
        public bool IsPressed() { return !stateDownOld && stateDownNew; }
        public bool IsReleased() { return stateDownOld && !stateDownNew; }
        #endregion

        #region Keyboard
        private List<Keys> keys = new List<Keys>();

        public void Add(Keys key)
        {
            if (!keys.Contains(key))
                keys.Add(key);
        }

        private bool IsKeyDown(KeyboardState KB)
        {
            switch (Type)
            {
                case InputActionTypes.One:
                    return keys.Any(k => KB.IsKeyDown(k));
                case InputActionTypes.All:
                    return keys.All(k => KB.IsKeyDown(k));
            }
            return false;
        }
        #endregion

        #region Mouse
        public enum MouseButtons { LeftButton, RightButton, MiddleButton, XButton1, XButton2 }

        private List<MouseButtons> buttons = new List<MouseButtons>();

        public void Add(MouseButtons button)
        {
            if (!buttons.Contains(button))
                buttons.Add(button);
        }

        private bool IsMouseButtonDown(MouseState state)
        {
            switch (Type)
            {
                case InputActionTypes.One:
                    return buttons.Any(b => IsMouseButtonDown(state, b));
                case InputActionTypes.All:
                    return buttons.All(b => IsMouseButtonDown(state, b));
            }
            return false;
        }

        private bool IsMouseButtonDown(MouseState state, MouseButtons button)
        {
            ButtonState buttonState = ButtonState.Released;

            switch (button)
            {
                case MouseButtons.LeftButton:
                    buttonState = state.LeftButton;
                    break;
                case MouseButtons.RightButton:
                    buttonState = state.RightButton;
                    break;
                case MouseButtons.MiddleButton:
                    buttonState = state.MiddleButton;
                    break;
                case MouseButtons.XButton1:
                    buttonState = state.XButton1;
                    break;
                case MouseButtons.XButton2:
                    buttonState = state.XButton2;
                    break;
            }

            return buttonState == ButtonState.Pressed;
        }
        #endregion
    }
}
