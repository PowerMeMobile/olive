//
// Microsoft.Web.BindingCollection
//
// Author:
//   Chris Toshok (toshok@ximian.com)
//
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if NET_2_0

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Microsoft.Web
{
	[ControlBuilder (typeof (BindingCollectionBuilder))]
	public class BindingCollection : Collection<Binding>, IScriptObject
	{
		IScriptObject owner;
		string id;
		bool readOnly;

		public BindingCollection (IScriptObject owner)
			: this (owner, false)
		{
		}

		public BindingCollection (IScriptObject owner, bool readOnly)
		{
			this.id = "";
			this.owner = owner;
			this.readOnly = readOnly;
		}

		protected void ClearItems ()
		{
			throw new NotImplementedException ();
		}

		protected void InsertItem (int index, Binding item)
		{
			throw new NotImplementedException ();
		}

		protected void RemoveItem (int index)
		{
			throw new NotImplementedException ();
		}

		protected void SetItem (int index, Binding item)
		{
			throw new NotImplementedException ();
		}

		string IScriptObject.ID {
			get { return id; }
			set { id = (value == null ? "" : value); }
		}

		IScriptObject IScriptObject.Owner {
			get { return owner; }
		}

		ScriptTypeDescriptor IScriptObject.GetTypeDescriptor ()
		{
			throw new NotImplementedException ();
		}
	}

	class BindingCollectionBuilder : ControlBuilder
	{
		public override bool AllowWhitespaceLiterals () 
		{
			return false;
		}

		public override Type GetChildControlType (string tagName, IDictionary attribs) 
		{
			if (System.String.Compare (tagName, "binding", true) != 0)
				return null;

			return typeof (Binding);
		}
	}

}

#endif
