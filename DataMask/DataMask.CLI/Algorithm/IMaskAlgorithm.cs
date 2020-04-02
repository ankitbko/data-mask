using System;
using System.Collections.Generic;
using System.Text;

namespace DataMask.CLI.Algorithm
{
    public interface IMaskAlgorithm
    {
        T Mask<T, U>(U input);
    }
}
