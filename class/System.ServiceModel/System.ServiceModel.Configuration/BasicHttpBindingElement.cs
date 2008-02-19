//
// BasicHttpBindingElement.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2006 Novell, Inc.  http://www.novell.com
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.MsmqIntegration;
using System.ServiceModel.PeerResolvers;
using System.ServiceModel.Security;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	[MonoTODO]
	public partial class BasicHttpBindingElement
		 : StandardBindingElement,  IBindingConfigurationElement
	{
		// Static Fields
		static ConfigurationPropertyCollection properties;
		static ConfigurationProperty allow_cookies;
		static ConfigurationProperty bypass_proxy_on_local;
		static ConfigurationProperty host_name_comparison_mode;
		static ConfigurationProperty max_buffer_pool_size;
		static ConfigurationProperty max_buffer_size;
		static ConfigurationProperty max_received_message_size;
		static ConfigurationProperty message_encoding;
		static ConfigurationProperty proxy_address;
		static ConfigurationProperty reader_quotas;
		static ConfigurationProperty security;
		static ConfigurationProperty text_encoding;
		static ConfigurationProperty transfer_mode;
		static ConfigurationProperty use_default_web_proxy;

		static BasicHttpBindingElement ()
		{
			properties = PropertiesInternal;
			allow_cookies = new ConfigurationProperty ("allowCookies",
				typeof (bool), "false", new BooleanConverter (), null,
				ConfigurationPropertyOptions.None);

			bypass_proxy_on_local = new ConfigurationProperty ("bypassProxyOnLocal",
				typeof (bool), "false", new BooleanConverter (), null,
				ConfigurationPropertyOptions.None);

			host_name_comparison_mode = new ConfigurationProperty ("hostNameComparisonMode",
				typeof (HostNameComparisonMode), "StrongWildcard", null/* FIXME: get converter for HostNameComparisonMode*/, null,
				ConfigurationPropertyOptions.None);

			max_buffer_pool_size = new ConfigurationProperty ("maxBufferPoolSize",
				typeof (long), "524288", null/* FIXME: get converter for long*/, null,
				ConfigurationPropertyOptions.None);

			max_buffer_size = new ConfigurationProperty ("maxBufferSize",
				typeof (int), "65536", null/* FIXME: get converter for int*/, null,
				ConfigurationPropertyOptions.None);

			max_received_message_size = new ConfigurationProperty ("maxReceivedMessageSize",
				typeof (long), "65536", null/* FIXME: get converter for long*/, null,
				ConfigurationPropertyOptions.None);

			message_encoding = new ConfigurationProperty ("messageEncoding",
				typeof (WSMessageEncoding), "Text", null/* FIXME: get converter for WSMessageEncoding*/, null,
				ConfigurationPropertyOptions.None);

			proxy_address = new ConfigurationProperty ("proxyAddress",
				typeof (Uri), null, new UriTypeConverter (), null,
				ConfigurationPropertyOptions.None);

			reader_quotas = new ConfigurationProperty ("readerQuotas",
				typeof (XmlDictionaryReaderQuotasElement), null, null/* FIXME: get converter for XmlDictionaryReaderQuotasElement*/, null,
				ConfigurationPropertyOptions.None);

			security = new ConfigurationProperty ("security",
				typeof (BasicHttpSecurityElement), null, null/* FIXME: get converter for BasicHttpSecurityElement*/, null,
				ConfigurationPropertyOptions.None);

			text_encoding = new ConfigurationProperty ("textEncoding",
				typeof (Encoding), "utf-8", EncodingConverter.Instance, null,
				ConfigurationPropertyOptions.None);

			transfer_mode = new ConfigurationProperty ("transferMode",
				typeof (TransferMode), "Buffered", null/* FIXME: get converter for TransferMode*/, null,
				ConfigurationPropertyOptions.None);

			use_default_web_proxy = new ConfigurationProperty ("useDefaultWebProxy",
				typeof (bool), "true", new BooleanConverter (), null,
				ConfigurationPropertyOptions.None);

			properties.Add (allow_cookies);
			properties.Add (bypass_proxy_on_local);
			properties.Add (host_name_comparison_mode);
			properties.Add (max_buffer_pool_size);
			properties.Add (max_buffer_size);
			properties.Add (max_received_message_size);
			properties.Add (message_encoding);
			properties.Add (proxy_address);
			properties.Add (reader_quotas);
			properties.Add (security);
			properties.Add (text_encoding);
			properties.Add (transfer_mode);
			properties.Add (use_default_web_proxy);
		}

		public BasicHttpBindingElement ()
		{
		}

		public BasicHttpBindingElement (string name) : base (name) { }

		// Properties

		[ConfigurationProperty ("allowCookies",
			DefaultValue = false,
			 Options = ConfigurationPropertyOptions.None)]
		public bool AllowCookies {
			get { return (bool) base [allow_cookies]; }
			set { base [allow_cookies] = value; }
		}

		protected override Type BindingElementType {
			get { return typeof (BasicHttpBinding); }
		}

		[ConfigurationProperty ("bypassProxyOnLocal",
			DefaultValue = false,
			 Options = ConfigurationPropertyOptions.None)]
		public bool BypassProxyOnLocal {
			get { return (bool) base [bypass_proxy_on_local]; }
			set { base [bypass_proxy_on_local] = value; }
		}

		[ConfigurationProperty ("hostNameComparisonMode",
			 DefaultValue = "StrongWildcard",
			 Options = ConfigurationPropertyOptions.None)]
		public HostNameComparisonMode HostNameComparisonMode {
			get { return (HostNameComparisonMode) base [host_name_comparison_mode]; }
			set { base [host_name_comparison_mode] = value; }
		}

		[LongValidator ( MinValue = 0,
			 MaxValue = 9223372036854775807,
			ExcludeRange = false)]
		[ConfigurationProperty ("maxBufferPoolSize",
			 DefaultValue = "524288",
			 Options = ConfigurationPropertyOptions.None)]
		public long MaxBufferPoolSize {
			get { return (long) base [max_buffer_pool_size]; }
			set { base [max_buffer_pool_size] = value; }
		}

		[IntegerValidator ( MinValue = 1,
			MaxValue = int.MaxValue,
			ExcludeRange = false)]
		[ConfigurationProperty ("maxBufferSize",
			 DefaultValue = "65536",
			 Options = ConfigurationPropertyOptions.None)]
		public int MaxBufferSize {
			get { return (int) base [max_buffer_size]; }
			set { base [max_buffer_size] = value; }
		}

		[LongValidator ( MinValue = 1,
			 MaxValue = 9223372036854775807,
			ExcludeRange = false)]
		[ConfigurationProperty ("maxReceivedMessageSize",
			 DefaultValue = "65536",
			 Options = ConfigurationPropertyOptions.None)]
		public long MaxReceivedMessageSize {
			get { return (long) base [max_received_message_size]; }
			set { base [max_received_message_size] = value; }
		}

		[ConfigurationProperty ("messageEncoding",
			 DefaultValue = "Text",
			 Options = ConfigurationPropertyOptions.None)]
		public WSMessageEncoding MessageEncoding {
			get { return (WSMessageEncoding) base [message_encoding]; }
			set { base [message_encoding] = value; }
		}

		protected override ConfigurationPropertyCollection Properties {
			get { return properties; }
		}

		[ConfigurationProperty ("proxyAddress",
			 DefaultValue = null,
			 Options = ConfigurationPropertyOptions.None)]
		public Uri ProxyAddress {
			get { return (Uri) base [proxy_address]; }
			set { base [proxy_address] = value; }
		}

		[ConfigurationProperty ("readerQuotas",
			 Options = ConfigurationPropertyOptions.None)]
		public XmlDictionaryReaderQuotasElement ReaderQuotas {
			get { return (XmlDictionaryReaderQuotasElement) base [reader_quotas]; }
		}

		[ConfigurationProperty ("security",
			 Options = ConfigurationPropertyOptions.None)]
		public BasicHttpSecurityElement Security {
			get { return (BasicHttpSecurityElement) base [security]; }
		}

		[TypeConverter ()]
		[ConfigurationProperty ("textEncoding",
			 DefaultValue = "utf-8",
			 Options = ConfigurationPropertyOptions.None)]
		public Encoding TextEncoding {
			get { return (Encoding) base [text_encoding]; }
			set { base [text_encoding] = value; }
		}

		[ConfigurationProperty ("transferMode",
			 DefaultValue = "Buffered",
			 Options = ConfigurationPropertyOptions.None)]
		public TransferMode TransferMode {
			get { return (TransferMode) base [transfer_mode]; }
			set { base [transfer_mode] = value; }
		}

		[ConfigurationProperty ("useDefaultWebProxy",
			DefaultValue = true,
			 Options = ConfigurationPropertyOptions.None)]
		public bool UseDefaultWebProxy {
			get { return (bool) base [use_default_web_proxy]; }
			set { base [use_default_web_proxy] = value; }
		}


	}

}
