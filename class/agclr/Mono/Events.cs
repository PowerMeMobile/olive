//
// Events.cs
//
// Author:
//   Miguel de Icaza (miguel@novell.com)
//
// Copyright 2007 Novell, Inc.
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
using System;
using System.Windows;
using System.Windows.Input;

namespace Mono {

	delegate void CallbackMouseEvent (IntPtr target, int state, double x, double y);
	delegate void PlainEvent (IntPtr target);
	delegate void KeyboardEvent (IntPtr target, int state, int platformcode, int key);
	
	internal class Events {
		static CallbackMouseEvent mouse_motion      = new CallbackMouseEvent (mouse_motion_notify_callback);
		static CallbackMouseEvent mouse_button_down = new CallbackMouseEvent (mouse_button_down_callback);
		static CallbackMouseEvent mouse_button_up   = new CallbackMouseEvent (mouse_button_up_callback);
		static CallbackMouseEvent mouse_enter       = new CallbackMouseEvent (mouse_enter_callback);

		static KeyboardEvent keydown = new KeyboardEvent (keydown_callback);
		static KeyboardEvent keyup   = new KeyboardEvent (keyup_callback);
		
		static PlainEvent got_focus   = new PlainEvent (got_focus_callback);
		static PlainEvent lost_focus  = new PlainEvent (lost_focus_callback);
		static PlainEvent loaded      = new PlainEvent (loaded_callback);
		static PlainEvent mouse_leave = new PlainEvent (mouse_leave_callback);

		static void got_focus_callback (IntPtr target)
		{
		}

		static void lost_focus_callback (IntPtr target)
		{
		}

		static void loaded_callback (IntPtr target)
		{
		}

		static void mouse_leave_callback (IntPtr target)
		{
		}

		static void keyup_callback (IntPtr target, int state, int platformcode, int key)
		{
		}

		static void keydown_callback (IntPtr target, int state, int platformcode, int key)
		{
		}
		
		static void mouse_motion_notify_callback (IntPtr target, int state, double x, double y)
		{
			object o = DependencyObject.Lookup (target);
			if (o == null){
				Console.WriteLine ("Motion event for {0} that was never registered", target);
				return;
			}
			UIElement e = o as UIElement;
			if (e == null)
				throw new Exception ("An object that is a UIElement is no longer that");

			e.InvokeMouseMove (new MouseEventArgs (e, state, x, y));
		}
		
		static void mouse_button_down_callback (IntPtr target, int state, double x, double y)
		{
		}
		
		static void mouse_button_up_callback (IntPtr target, int state, double x, double y)
		{
		}
		
		static void mouse_enter_callback (IntPtr target, int state, double x, double y)
		{
		}
		
		internal static void InitSurface (IntPtr surface)
		{
			NativeMethods.surface_register_events (
				surface,
				mouse_motion, mouse_button_down, mouse_button_up, mouse_enter,
				got_focus, lost_focus, loaded, mouse_leave,
				keydown, keyup);
		}
	}
	
}
