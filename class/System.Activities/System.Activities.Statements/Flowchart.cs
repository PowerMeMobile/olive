using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Transactions;
using System.Activities;
using System.Activities.Debugger;
using System.Activities.Expressions;
using System.Activities.Hosting;
using System.Windows.Markup;

namespace System.Activities.Statements
{
	[ContentProperty ("Nodes")]
	public sealed class Flowchart : NativeActivity
	{
		public Collection<FlowNode> Nodes { get { throw new NotImplementedException (); } }
		public FlowNode StartNode { get; set; }
		public Collection<Variable> Variables { get { throw new NotImplementedException (); } }

		protected override void Execute (NativeActivityContext context)
		{
			throw new NotImplementedException ();
		}
	}
}
