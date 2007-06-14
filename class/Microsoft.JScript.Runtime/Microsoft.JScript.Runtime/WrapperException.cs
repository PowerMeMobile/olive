using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.JScript.Runtime {
	public class WrapperException : Exception, _Exception, ISerializable {
		public WrapperException ()
		{
		}

		public WrapperException (string message)
		{
		}

		public WrapperException (string message, Exception innerException)
		{
		}

		public override string ToString ()
		{
			return base.ToString ();
		}

		public override string Message {
			get { return base.Message; }
		}
	}
}
