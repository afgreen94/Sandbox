using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fractals.Visualization
{
    public abstract class Renderer
    {
        public abstract void Render(IState state);

    }

    public interface IState { }
}
