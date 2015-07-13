I'm Only Resting is a feature-rich WinForms-based HTTP client that makes building and managing HTTP requests easy and offers various smart response content rendering modes.

![https://im-only-resting.googlecode.com/svn/images/front-page-example.png](https://im-only-resting.googlecode.com/svn/images/front-page-example.png)

Feature Overview
  * Easy request management
    * Save, Save As..., and Open... file menu options
    * URL auto-completion
    * Persistent request history with temporary response snapshots.
  * Choose from 3 response body output modes
    * Raw - content is displayed as plain text honoring the content-type charset
    * Pretty - content is displayed as plain text honoring the content-type charset and additionally performs pretty-printing on XML, JSON, and HTML content-types.
      * XML pretty-printing provided by the [.NET `XDocument` type](http://msdn.microsoft.com/en-us/library/system.xml.linq.xdocument(v=vs.100).aspx)
      * JSON pretty-printing provided by [Json.NET](http://json.codeplex.com/)
      * HTML pretty-printing provided by [HTML Tidy](http://tidy.sourceforge.net/)
    * Rendered - content is rendered in an embedded IE browser instance. XML, HTML, and images are rendered using IE's rendering engine. JSON is rendered using JSON.NET pretty-printing embedded in an html pre tag. Other types of plain text are embedded in an html pre tag. IE's search feature is enabled.
  * Export raw response body bytes with automatic file-type extension detection based on the content-type
  * Attempt to pretty-print XML and JSON request bodies using .NET `XDocument` type and Json.NET respectively
  * Configure a proxy server
  * User-interface for managing persistent user-settings
  * Request-response split panel view can toggle between horizontal and vertical orientation
  * Rich text editing and syntax highlighting provided by [ScintillaNET](http://scintillanet.codeplex.com/)
  * Interactive interface for inspecting log file


---


&lt;wiki:gadget url="http://www.ohloh.net/p/711761/widgets/project\_factoids.xml" height="195" border="0" width="100%" /&gt;

You are welcome to [Pay What You Want](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WXSPTXDPECJ9L) for I'm Only Resting via PayPal.

Copyright 2012-2014 [Swensen Software](http://www.swensensoftware.com)