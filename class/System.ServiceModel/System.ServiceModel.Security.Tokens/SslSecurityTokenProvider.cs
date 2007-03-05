//
// SslSecurityTokenProvider.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2006-2007 Novell, Inc.  http://www.novell.com
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

using ReqType = System.ServiceModel.Security.Tokens.ServiceModelSecurityTokenRequirement;

namespace System.ServiceModel.Security.Tokens
{
	class SslSecurityTokenProvider : CommunicationSecurityTokenProvider
	{
		SslCommunicationObject comm;

		public SslSecurityTokenProvider ()
		{
			comm = new SslCommunicationObject ();
		}

		public override ProviderCommunicationObject Communication {
			get { return comm; }
		}

		public override SecurityToken GetOnlineToken (TimeSpan timeout)
		{
			return comm.GetToken (timeout);
		}
	}

	class SslCommunicationObject : ProviderCommunicationObject
	{
		WSTrustSecurityTokenServiceProxy proxy;

		public SslCommunicationObject ()
		{
		}

		public SecurityToken GetToken (TimeSpan timeout)
		{
			TlsClientSession tls = new TlsClientSession (IssuerAddress.Uri.ToString ());
			WstRequestSecurityToken rst =
				new WstRequestSecurityToken ();

			// send ClientHello
			rst.BinaryExchange = new WstBinaryExchange ();
			rst.BinaryExchange.Value = tls.ProcessClientHello ();

Console.WriteLine ("Negotiation Context: " + rst.Context);

			Message request = Message.CreateMessage (IssuerBinding.MessageVersion, Constants.WstIssueAction, rst);
			request.Headers.MessageId = new UniqueId ();
			request.Headers.ReplyTo = new EndpointAddress (Constants.WsaAnonymousUri);
			request.Headers.To = TargetAddress.Uri;
			Message response = proxy.Issue (request);

MessageBuffer buf = response.CreateBufferedCopy (0x10000);
response = buf.CreateMessage ();
Console.WriteLine (buf.CreateMessage ());


			// receive ServerHello
			WSTrustRequestSecurityTokenResponseReader reader =
				new WSTrustRequestSecurityTokenResponseReader (response.GetReaderAtBodyContents (), SecurityTokenSerializer, null);
			reader.Read ();
			if (reader.Value.RequestedSecurityToken != null)
				return reader.Value.RequestedSecurityToken;

			tls.ProcessServerHello (reader.Value.BinaryExchange.Value);

			// send ClientKeyExchange
//			WstRequestSecurityTokenResponse rstr =
//				new WstRequestSecurityTokenResponse ();
			WstRequestSecurityToken rstr =
				new WstRequestSecurityToken ();
			rstr.Context = reader.Value.Context;
			rstr.BinaryExchange = new WstBinaryExchange ();
			rstr.BinaryExchange.Value = tls.ProcessClientKeyExchange ();

			request = Message.CreateMessage (IssuerBinding.MessageVersion, Constants.WstIssueAction, rstr);
			request.Headers.RelatesTo = response.Headers.RelatesTo;
			request.Headers.To = TargetAddress.Uri;
			// FIXME: regeneration of this instance is somehow required, but should not be.
			proxy = new WSTrustSecurityTokenServiceProxy (
				IssuerBinding, IssuerAddress);
			response = proxy.Issue (request);

			reader = new WSTrustRequestSecurityTokenResponseReader (response.GetReaderAtBodyContents (), SecurityTokenSerializer, null);
			reader.Read ();
			if (reader.Value.RequestedSecurityToken != null)
				return reader.Value.RequestedSecurityToken;

			// FIXME: continue negotiation
			throw new NotImplementedException ();
		}

		protected internal override TimeSpan DefaultCloseTimeout {
			get { throw new NotImplementedException (); }
		}

		protected internal override TimeSpan DefaultOpenTimeout {
			get { throw new NotImplementedException (); }
		}

		protected override void OnAbort ()
		{
			throw new NotImplementedException ();
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			if (State == CommunicationState.Opened)
				throw new InvalidOperationException ("Already opened.");

			EnsureProperties ();

			proxy = new WSTrustSecurityTokenServiceProxy (
				IssuerBinding, IssuerAddress);
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ();
		}

		protected override void OnClose (TimeSpan timeout)
		{
			if (proxy != null)
				proxy.Close ();
		}

		protected override IAsyncResult OnBeginClose (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ();
		}
	}
}
