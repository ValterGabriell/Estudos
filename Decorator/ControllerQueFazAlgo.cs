using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Decorator
{
    internal class ControllerQueFazAlgo : IFazerAlgo
    {
        public string FazerAlgo()
        {
            return "Aqui dentro faz um monte de coisa";
        }
    }
}
