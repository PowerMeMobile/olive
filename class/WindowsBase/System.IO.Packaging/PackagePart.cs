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
//	Chris Toshok (toshok@ximian.com)
//  Alan McGovern (amcgovern@novell.com)
//

using System;
using System.Collections.Generic;
using System.IO;

namespace System.IO.Packaging {

	public abstract class PackagePart
	{
		private CompressionOption compressionOption;
		private string contentType;
		private Package package;
		private Dictionary<string, PackageRelationship> relationships;
		private Uri uri;
		
		protected PackagePart (Package package, Uri partUri)
			: this(package, partUri, null)
		{
			
		}

		protected PackagePart (Package package, Uri partUri, string contentType)
			: this (package, partUri, contentType, CompressionOption.Normal)
		{
			
		}

		protected PackagePart (Package package, Uri partUri, string contentType, CompressionOption compressionOption)
		{
			Check.Package (package);
			Check.PartUri (partUri);
			Check.ContentTypeIsValid (contentType);

			this.package = package;
			this.uri = partUri;
			this.contentType = contentType;
			this.compressionOption = compressionOption;

			relationships = new Dictionary<string, PackageRelationship> ();
		}

		public CompressionOption CompressionOption {
			get { return compressionOption; }
		}

		public string ContentType {
			get { return contentType; }
		}

		public Package Package {
			get { return package; }
		}

		public Uri Uri {
			get { return uri; }
		}

		public PackageRelationship CreateRelationship (Uri targetUri, TargetMode targetMode, string relationshipType)
		{
			return CreateRelationship (targetUri, targetMode, relationshipType, null);
		}

		public PackageRelationship CreateRelationship (Uri targetUri, TargetMode targetMode, string relationshipType, string id)
		{
			Check.TargetUri (targetUri);
			Check.RelationshipTypeIsValid (relationshipType);
			Check.IdIsValid (id);

			PackageRelationship r = new PackageRelationship (id, package, relationshipType, Uri, targetMode, targetUri);
			relationships.Add (r.Id, r);
			return r;
		}

		public void DeleteRelationship (string id)
		{
			relationships.Remove (id);
		}

		public bool RelationshipExists (string id)
		{
			return relationships.ContainsKey (id);
		}

		public PackageRelationship GetRelationship (string id)
		{
			return relationships [id];
		}

		public PackageRelationshipCollection GetRelationships ()
		{
			return new PackageRelationshipCollection (relationships.Values);
		}

		public PackageRelationshipCollection GetRelationshipsByType (string relationshipType)
		{
			return new PackageRelationshipCollection (relationships.Values, delegate (PackageRelationship r) {
				return r.RelationshipType == relationshipType;
			});
		}

		public Stream GetStream ()
		{
			throw new NotImplementedException ();
		}

		public Stream GetStream (FileMode mode)
		{
			throw new NotImplementedException ();
		}

		public Stream GetStream (FileMode mode, FileAccess access)
		{
			throw new NotImplementedException ();
		}

		protected abstract Stream GetStreamCore (FileMode mode, FileAccess access);

		protected virtual string GetContentTypeCore ()
		{
			throw new NotImplementedException ();
		}
	}
}