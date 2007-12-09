
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public abstract class ColorKeyFrame : Freezable, IKeyFrame
{
	public static readonly DependencyProperty KeyTimeProperty
				= DependencyProperty.Register ("KeyTime", typeof (KeyTime), typeof (ColorKeyFrame));

	public static readonly DependencyProperty ValueProperty
				= DependencyProperty.Register ("Value", typeof (Color), typeof (ColorKeyFrame));

	protected ColorKeyFrame ()
	{
	}

	protected ColorKeyFrame (Color value)
	{
	}

	protected ColorKeyFrame (Color value, KeyTime keyTime)
	{
	}

	public KeyTime KeyTime {
		get { return (KeyTime)GetValue (KeyTimeProperty); }
		set { SetValue (KeyTimeProperty, value); }
	}

	public Color Value {
		get { return (Color)GetValue (ValueProperty); }
		set { SetValue (ValueProperty, value); }
	}

	object IKeyFrame.Value {
		get { return Value; }
		set { Value = (Color)value; }
	}

	public Color InterpolateValue (Color baseValue, double keyFrameProgress)
	{
		throw new NotImplementedException ();
	}

	protected abstract Color InterpolateValueCore (Color baseValue, double keyFrameProgress);
}


}