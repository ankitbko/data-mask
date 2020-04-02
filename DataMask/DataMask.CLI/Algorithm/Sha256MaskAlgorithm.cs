using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataMask.CLI.Algorithm
{
    public class Sha256MaskAlgorithm : IMaskAlgorithm, IDisposable
    {
        private SHA256 sha = SHA256.Create();
        public T Mask<T, U>(U input)
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return (T)(object)builder.ToString();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sha?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Sha256MaskAlgorithm()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    public static class MaskAlgoExtension
    {
        public static string Mask<U>(this IMaskAlgorithm algo, U input)
        {
            return algo.Mask<string, U>(input);
        }
        public static string Mask(this IMaskAlgorithm algo, string input)
        {
            return algo.Mask<string, string>(input);
        }

        public static string Mask(this IMaskAlgorithm algo, ValueType input)
        {
            return algo.Mask<string, ValueType>(input);
        }
    }
}