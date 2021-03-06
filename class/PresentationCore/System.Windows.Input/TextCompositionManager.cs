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
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Chris Toshok (toshok@novell.com)
//

using System;
using System.Security;
using System.Windows;
using System.Windows.Threading;

namespace System.Windows.Input {

	public sealed class TextCompositionManager : DispatcherObject
	{
		public static readonly RoutedEvent PreviewTextInputEvent = new RoutedEvent ("PreviewTextInput",
											    typeof (TextCompositionEventHandler),
											    typeof (TextCompositionManager),
											    RoutingStrategy.Tunnel);
		public static readonly RoutedEvent PreviewTextInputStartEvent = new RoutedEvent ("PreviewTextInputStart",
												 typeof (TextCompositionEventHandler),
												 typeof (TextCompositionManager),
												 RoutingStrategy.Tunnel);
		public static readonly RoutedEvent PreviewTextInputUpdateEvent = new RoutedEvent ("PreviewTextInputUpdate",
												  typeof (TextCompositionEventHandler),
												  typeof (TextCompositionManager),
												  RoutingStrategy.Tunnel);
		public static readonly RoutedEvent TextInputEvent = new RoutedEvent ("TextInput",
										     typeof (TextCompositionEventHandler),
										     typeof (TextCompositionManager),
										     RoutingStrategy.Bubble);
		public static readonly RoutedEvent TextInputStartEvent = new RoutedEvent ("TextInputStart",
											  typeof (TextCompositionEventHandler),
											  typeof (TextCompositionManager),
											  RoutingStrategy.Bubble);
		public static readonly RoutedEvent TextInputUpdateEvent = new RoutedEvent ("TextInputUpdate",
											  typeof (TextCompositionEventHandler),
											  typeof (TextCompositionManager),
											  RoutingStrategy.Bubble);

		internal TextCompositionManager ()
		{
		}

		public static void AddPreviewTextInputHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void AddPreviewTextInputStartHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void AddPreviewTextInputUpdateHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void AddTextInputHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void AddTextInputStartHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void AddTextInputUpdateHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void RemovePreviewTextInputHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void RemovePreviewTextInputStartHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void RemovePreviewTextInputUpdateHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void RemoveTextInputHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void RemoveTextInputStartHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		public static void RemoveTextInputUpdateHandler (DependencyObject element, TextCompositionEventHandler handler)
		{
			throw new NotImplementedException ();
		}

		[SecurityCritical]
		public static bool CompleteComposition (TextComposition composition)
		{
			throw new NotImplementedException ();
		}

		[SecurityCritical]
		public static bool StartComposition (TextComposition composition)
		{
			throw new NotImplementedException ();
		}

		[SecurityCritical]
		public static bool UpdateComposition (TextComposition composition)
		{
			throw new NotImplementedException ();
		}

	}

}

