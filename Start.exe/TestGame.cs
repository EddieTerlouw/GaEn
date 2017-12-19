using GaEn;
using Microsoft.Xna.Framework;

namespace Start
{
    public class TestGame : Game
    {
        public TestGame()
        {
            Engine.Instance(this);
            IsMouseVisible = true;
        }
    }
}
