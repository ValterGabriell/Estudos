using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Decorator
{
    internal class FazerMaisCoisaExtendendoController : IFazerAlgo
    {
        private readonly IFazerAlgo _fazerAlgo;

        //[FromKeydService("og")]
        public FazerMaisCoisaExtendendoController(
            IFazerAlgo fazerAlgo)
        {
            _fazerAlgo = fazerAlgo;
        }

        public string FazerAlgo()
        {
            string init;
			Start:
			try
			{
                init = _fazerAlgo.FazerAlgo();
            }
			catch (Exception)
			{
				goto Start;
            }
            return "Aqui dentro faz um monte de coisa e mais coisa ainda" + init;
        }
    }
}
