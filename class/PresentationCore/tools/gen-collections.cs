using System;
using System.IO;

public class GenCollectionType {

	enum CollectionType {
		Int32,
		Double,
		Point,
		Geometry,
		Drawing,
		PathFigure,
		PathSegment,
		GradientStop,
		GeneralTransform,
		Transform,
		Timeline,
		Clock,
		Vector
	};

	enum Namespace {
		System_Windows_Media,
		System_Windows_Media_Animation
	};

	static string GetNamespace (Namespace ns)
	{
		switch (ns) {
		case Namespace.System_Windows_Media: return "System.Windows.Media";
		case Namespace.System_Windows_Media_Animation: return "System.Windows.Media.Animation";
		default: throw new Exception ();
		}
	}

	static Namespace GetNamespaceForType (CollectionType t)
	{
		switch (t) {
		case CollectionType.Int32:
		case CollectionType.Double:
		case CollectionType.Point:
		case CollectionType.Geometry:
		case CollectionType.Drawing:
		case CollectionType.PathFigure:
		case CollectionType.PathSegment:
		case CollectionType.GradientStop:
		case CollectionType.GeneralTransform:
		case CollectionType.Transform:
		case CollectionType.Vector:
			return Namespace.System_Windows_Media;
		case CollectionType.Timeline:
		case CollectionType.Clock:
			return Namespace.System_Windows_Media_Animation;
		default: throw new Exception ();
		}
	}

	static string GetParentClass (CollectionType t)
	{
		switch (t) {	
		case CollectionType.Int32:
		case CollectionType.Double:
		case CollectionType.Point:
		case CollectionType.Vector:
			return "Freezable";
		default:
			return "Animatable";
		}
	}

	static bool HasConverter (CollectionType t)
	{
		switch (t) {
		case CollectionType.Drawing:
		case CollectionType.Geometry:
		case CollectionType.PathSegment:
		case CollectionType.GradientStop:
		case CollectionType.GeneralTransform:
		case CollectionType.Transform:
		case CollectionType.Timeline:
			return false;
		default:
			return true;
		}
	}

	static bool IsFreezableSubclass (CollectionType t)
	{
		return true;
	}

	static bool NeedsFreezeCore (CollectionType t)
	{
		switch (t) {
		case CollectionType.Geometry:
		case CollectionType.Drawing:
		case CollectionType.PathFigure:
		case CollectionType.PathSegment:
		case CollectionType.GradientStop:
		case CollectionType.GeneralTransform:
		case CollectionType.Timeline:
			return true;
		default:
			return false;
		}

	}

	static string GetParameterType (CollectionType t)
	{
		switch (t) {
		case CollectionType.Int32: return "int";
		case CollectionType.Double: return "double";
		default:
			return t.ToString();
		}
	}

	CollectionType type;

	GenCollectionType (CollectionType type)
	{
		this.type = type;
	}

	void OutputHeader (TextWriter tw)
	{
		tw.WriteLine (@"
/* this file is generated by gen-collection-types.cs.  do not modify */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media.Animation;

namespace {0} {{
", GetNamespace (GetNamespaceForType (type)));
	}

	void OutputFooter (TextWriter tw)
	{
		tw.WriteLine ("	}\n}"); /* close the namespace */
	}

	void OutputCollection (TextWriter tw)
	{
		tw.WriteLine (@"
	public class {0}Collection : {2}ICollection<{0}>, IList<{0}>, ICollection, IList, IFormattable
	{{
		List<{0}> list;

		public struct Enumerator : IEnumerator<{0}>, IEnumerator
		{{
			public void Reset()
			{{
				throw new NotImplementedException (); 
			}}

			public bool MoveNext()
			{{
				throw new NotImplementedException (); 
			}}

			public {0} Current {{
				get {{ throw new NotImplementedException (); }}
			}}

			object IEnumerator.Current {{
				get {{ return Current; }}
			}}

			void IDisposable.Dispose()
			{{
			}}
		}}

		public {0}Collection ()
		{{
			list = new List<{0}>();
		}}

		public {0}Collection (IEnumerable<{0}> values)
		{{
			list = new List<{0}> (values);
		}}

		public {0}Collection (int length)
		{{
			list = new List<{0}> (length);
		}}

		public new {0}Collection Clone ()
		{{
			throw new NotImplementedException ();
		}}

		public new {0}Collection CloneCurrentValue ()
		{{
			throw new NotImplementedException ();
		}}

		public bool Contains ({1} value)
		{{
			return list.Contains (value);
		}}

		public bool Remove ({1} value)
		{{
			return list.Remove (value);
		}}

		public int IndexOf ({1} value)
		{{
			return list.IndexOf (value);
		}}

		public void Add ({1} value)
		{{
			list.Add (value);
		}}

		public void Clear ()
		{{
			list.Clear ();
		}}

		public void CopyTo ({1}[] array, int arrayIndex)
		{{
			list.CopyTo (array, arrayIndex);
		}}

		public void Insert (int index, {1} value)
		{{
			list.Insert (index, value);
		}}

		public void RemoveAt (int index)
		{{
			list.RemoveAt (index);
		}}

		public int Count {{
			get {{ return list.Count; }}
		}}

		public {1} this[int index] {{
			get {{ return list[index]; }}
			set {{ list[index] = value; }}
		}}

		public static {0}Collection Parse (string str)
		{{
			throw new NotImplementedException ();
		}}

		bool ICollection<{0}>.IsReadOnly {{
			get {{ return false; }}
		}}

		public Enumerator GetEnumerator()
		{{
			return new Enumerator();
		}}

		IEnumerator<{0}> IEnumerable<{0}>.GetEnumerator()
		{{
			return GetEnumerator ();
		}}

		IEnumerator IEnumerable.GetEnumerator ()
		{{
			return GetEnumerator();
		}}

		bool ICollection.IsSynchronized {{
			get {{ return false; }}
		}}

		object ICollection.SyncRoot {{
			get {{ return this; }}
		}}

		void ICollection.CopyTo(Array array, int offset)
		{{
			CopyTo (({1}[]) array, offset);
		}}

		bool IList.IsFixedSize {{
			get {{ return false; }}
		}}

		bool IList.IsReadOnly {{
			get {{ return false; }}
		}}

		object IList.this[int index] {{
			get {{ return this[index]; }}
			set {{ this[index] = ({1})value; }}
		}}

		int IList.Add (object value)
		{{
			Add (({1})value);
			return Count;
		}}

		bool IList.Contains (object value)
		{{
			return Contains (({1})value);
		}}

		int IList.IndexOf (object value)
		{{
			return IndexOf (({1})value);
		}}

		void IList.Insert (int index, object value)
		{{
			Insert (index, ({1})value);
		}}

		void IList.Remove (object value)
		{{
			Remove (({1})value);
		}}

		public override string ToString ()
		{{
			throw new NotImplementedException ();
		}}

		public string ToString (IFormatProvider provider)
		{{
			throw new NotImplementedException ();
		}}

		string IFormattable.ToString (string format, IFormatProvider provider)
		{{
			throw new NotImplementedException ();
		}}
", type, GetParameterType(type), GetParentClass(type) != null ? GetParentClass(type) + ", " : "");
		
	}

	void OutputFreezable (TextWriter tw, CollectionType type)
	{
		if (NeedsFreezeCore (type))
			tw.WriteLine (@"
		protected override bool FreezeCore (bool isChecking)
		{{
			if (isChecking) {{
				return base.FreezeCore (isChecking);
			}}
			else {{
				return true;
			}}
		}}

");

		tw.WriteLine (@"
		protected override Freezable CreateInstanceCore ()
		{{
			return new {0}Collection();
		}}

		protected override void GetAsFrozenCore (Freezable sourceFreezable)
		{{
			throw new NotImplementedException ();
		}}

		protected override void GetCurrentValueAsFrozenCore (Freezable sourceFreezable)
		{{
			throw new NotImplementedException ();
		}}

		protected override void CloneCurrentValueCore (Freezable sourceFreezable)
		{{
			throw new NotImplementedException ();
		}}

		protected override void CloneCore (Freezable sourceFreezable)
		{{
			throw new NotImplementedException ();
		}}", type);
	}

	void OutputCollectionConverter (TextWriter tw)
	{
		tw.WriteLine (@"
	public class {0}CollectionConverter : TypeConverter
	{{
		public override bool CanConvertFrom (ITypeDescriptorContext context, Type type)
		{{
			throw new NotImplementedException ();
		}}

		public override object ConvertFrom (ITypeDescriptorContext context, CultureInfo culture, object value)
		{{
			throw new NotImplementedException (); 
		}}

		public override bool CanConvertTo (ITypeDescriptorContext context, Type type)
		{{
			throw new NotImplementedException ();
		}}

		public override object ConvertTo (ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{{
			throw new NotImplementedException (); 
		}}
", type);
	}

	void OutputCollectionFile ()
	{
		string filename = String.Format ("{1}/{0}Collection.cs", type,
						 GetNamespace (GetNamespaceForType (type)));

		Console.WriteLine ("outputting file {0}", filename);

		TextWriter tw = File.CreateText (filename);

		OutputHeader(tw);
		OutputCollection (tw);
		if (IsFreezableSubclass(type))
			OutputFreezable (tw, type);
		OutputFooter(tw);

		tw.Close ();
	}

	void OutputCollectionConverterFile ()
	{
		string filename = String.Format ("System.Windows.Media/{0}CollectionConverter.cs", type);

		Console.WriteLine ("outputting file {0}", filename);

		TextWriter tw = File.CreateText (filename);

		OutputHeader(tw);
		OutputCollectionConverter (tw);
		OutputFooter(tw);

		tw.Close ();
	}

	public static void Main (string[] args)
	{
		foreach (CollectionType ct in Enum.GetValues(typeof (CollectionType))) {
			GenCollectionType gc = new GenCollectionType(ct);

			gc.OutputCollectionFile ();
			if (HasConverter (ct))
				gc.OutputCollectionConverterFile ();
		}
	}
}