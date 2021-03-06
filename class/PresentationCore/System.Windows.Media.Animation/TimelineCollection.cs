
/* this file is generated by gen-collection-types.cs.  do not modify */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media.Animation;

namespace System.Windows.Media.Animation {


	public class TimelineCollection : Animatable, ICollection<Timeline>, IList<Timeline>, ICollection, IList, IFormattable
	{
		List<Timeline> list;

		public struct Enumerator : IEnumerator<Timeline>, IEnumerator
		{
			public void Reset()
			{
				throw new NotImplementedException (); 
			}

			public bool MoveNext()
			{
				throw new NotImplementedException (); 
			}

			public Timeline Current {
				get { throw new NotImplementedException (); }
			}

			object IEnumerator.Current {
				get { return Current; }
			}

			void IDisposable.Dispose()
			{
			}
		}

		public TimelineCollection ()
		{
			list = new List<Timeline>();
		}

		public TimelineCollection (IEnumerable<Timeline> values)
		{
			list = new List<Timeline> (values);
		}

		public TimelineCollection (int length)
		{
			list = new List<Timeline> (length);
		}

		public new TimelineCollection Clone ()
		{
			throw new NotImplementedException ();
		}

		public new TimelineCollection CloneCurrentValue ()
		{
			throw new NotImplementedException ();
		}

		public bool Contains (Timeline value)
		{
			return list.Contains (value);
		}

		public bool Remove (Timeline value)
		{
			return list.Remove (value);
		}

		public int IndexOf (Timeline value)
		{
			return list.IndexOf (value);
		}

		public void Add (Timeline value)
		{
			list.Add (value);
		}

		public void Clear ()
		{
			list.Clear ();
		}

		public void CopyTo (Timeline[] array, int arrayIndex)
		{
			list.CopyTo (array, arrayIndex);
		}

		public void Insert (int index, Timeline value)
		{
			list.Insert (index, value);
		}

		public void RemoveAt (int index)
		{
			list.RemoveAt (index);
		}

		public int Count {
			get { return list.Count; }
		}

		public Timeline this[int index] {
			get { return list[index]; }
			set { list[index] = value; }
		}

		public static TimelineCollection Parse (string str)
		{
			throw new NotImplementedException ();
		}

		bool ICollection<Timeline>.IsReadOnly {
			get { return false; }
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator();
		}

		IEnumerator<Timeline> IEnumerable<Timeline>.GetEnumerator()
		{
			return GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator();
		}

		bool ICollection.IsSynchronized {
			get { return false; }
		}

		object ICollection.SyncRoot {
			get { return this; }
		}

		void ICollection.CopyTo(Array array, int offset)
		{
			CopyTo ((Timeline[]) array, offset);
		}

		bool IList.IsFixedSize {
			get { return false; }
		}

		bool IList.IsReadOnly {
			get { return false; }
		}

		object IList.this[int index] {
			get { return this[index]; }
			set { this[index] = (Timeline)value; }
		}

		int IList.Add (object value)
		{
			Add ((Timeline)value);
			return Count;
		}

		bool IList.Contains (object value)
		{
			return Contains ((Timeline)value);
		}

		int IList.IndexOf (object value)
		{
			return IndexOf ((Timeline)value);
		}

		void IList.Insert (int index, object value)
		{
			Insert (index, (Timeline)value);
		}

		void IList.Remove (object value)
		{
			Remove ((Timeline)value);
		}

		public override string ToString ()
		{
			throw new NotImplementedException ();
		}

		public string ToString (IFormatProvider provider)
		{
			throw new NotImplementedException ();
		}

		string IFormattable.ToString (string format, IFormatProvider provider)
		{
			throw new NotImplementedException ();
		}


		protected override bool FreezeCore (bool isChecking)
		{{
			if (isChecking) {{
				return base.FreezeCore (isChecking);
			}}
			else {{
				return true;
			}}
		}}



		protected override Freezable CreateInstanceCore ()
		{
			return new TimelineCollection();
		}

		protected override void GetAsFrozenCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}

		protected override void GetCurrentValueAsFrozenCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}

		protected override void CloneCurrentValueCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}

		protected override void CloneCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}
	}
}
