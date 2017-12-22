using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GaEn.Input
{
    public sealed class InputManager
    {
        #region Singleton
        private static object _object = new object();
        private static InputManager instance;

        static internal InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_object)
                    {
                        if (instance == null)
                            instance = new InputManager();
                    }
                }

                return instance;
            }
        }

        private InputManager() { }
        #endregion

        #region Actions
        private List<InputAction> actions = new List<InputAction>();

        public InputAction this[string action] { get { return actions.Find(a => a.Name == action); } }

        public void AddAction(InputAction action)
        {
            if (!actions.Contains(action))
                actions.Add(action);
        }
        #endregion

        #region XNA
        internal void Update(GameTime gameTime)
        {
            KeyboardState KB = Keyboard.GetState();
            MouseState M = Mouse.GetState();

            foreach (InputAction action in actions)
                action.Update(KB, M);
        }
        #endregion

    }
}
