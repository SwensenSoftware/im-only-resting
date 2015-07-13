# Introduction #

I'm Only Resting (IOR) is a feature-rich WinForms-based HTTP client that is well-suited for sending requests to REST APIs.

# Installation #

Extract the featured release zip file from the [Downloads](https://code.google.com/p/im-only-resting/wiki/Downloads?tm=2) page to any location and run im-only-resting.exe (tip: if you have a previous version installed, extract to the same location to keep your settings). Requires .NET 4.0 or above (Windows only).

# User Interface #

The user interface consists of a menu-bar (with menu-items often referred to as "commands" in this document) and a split-panel with one side containing the request interface and one side containing the response interface. Note that the split panel may toggle between horizontal and vertical orientation by double-clicking it.

## Request Panel ##

The request panel is used for constructing HTTP requests. Most of the features available have self-evident purposes.

### Url ###

The Url textbox is used for entering the request URL. If a protocol is not given, then http is assumed.

### Method ###

The Method panel allows the selection of the request HTTP method.

### Headers ###

The Headers text box allows the entry of request headers. A header is formatted as `<header name>: <header value>`. Multiple headers are specified by line separation. The same header may not be specified more than once (multiple header values should be indicated by a comma-separator on the same header line). For example:

```
accept: text/xml, text/html
content-type: text/plain; charset=utf-8
```

Generally, IOR avoids inserting headers in the transmitted request that are not explicitly entered here. Exceptions are Connection: Keep-Alive and Content-Type headers (the Content-Type header is inserted when sending a request with a body, such as a POST, where the Content-Type is not otherwise explicitly entered. The default for the Content-Type header may be set in the Settings).

### Body ###

The HTTP content may be entered in the Body text box. The text box context-menu contains commands for formatting the content as XML (provided by .NET) or JSON (provided by JSON.NET), if possible (if the content is not well-formed, then no action is executed, but no error occurs either).

### Buttons ###

The `Submit` button submits the HTTP request asynchronously. The request may be canceled before the response is received by clicking the "cancel" link in the response panel status area. Note that clicking the `Submit` button will validate the request URL and Headers, resulting in an error dialog enumerating the errors if any are detected.

## Response Panel ##

### Status and Time ###

The Status label presents information about the status of the response. While the response is being executed, a "cancel" link is available for cancelling the request. When the response is received, the HTTP status code and message are displayed.

The Time label shows the length of time it took between submitting the request and receiving the response.

### Body ###

The Body tab displays the response content. The Output panel provides options for displaying the content:
  * Raw - content is displayed as plain text honoring the content-type charset
  * Pretty - content is displayed as plain text honoring the content-type charset and additionally performs pretty-printing on XML, JSON, and HTML content-types.
    * XML pretty-printing provided by the [.NET `XDocument` type](http://msdn.microsoft.com/en-us/library/system.xml.linq.xdocument(v=vs.100).aspx)
    * JSON pretty-printing provided by [Json.NET](http://json.codeplex.com/)
    * HTML pretty-printing provided by [HTML Tidy](http://tidy.sourceforge.net/)
  * Rendered - content is rendered in an embedded IE browser instance. XML, HTML, and images are rendered using IE's rendering engine. JSON is rendered using JSON.NET pretty-printing embedded in an html pre tag. Other types of plain text are embedded in an html pre tag. IE's search feature is enabled.

### Headers ###

The headers tab displays all of the response headers.

# Settings #

The `Settings` command displays a dialog box used for setting various configurable options of I'm Only Resting. It is a traditional Windows PropertyGrid which includes explanations for each setting when selected. Validation ensures that no invalid settings are set. Pressing the `Cancel` button exits the dialog without saving. Pressing the `Save` button saves the settings and applies them immediately if possible.

Settings are persisted in user local app data, and automatically upgraded with new versions of I'm Only Resting (as long as the new version is deployed to the same location as the previous version).

# Persistence #

There are several modes of request and response persistence.

The `File -> Open`, `File -> Save...`, and `File -> Save As...` commands are used for opening and saving requests as .ior files.  The default file dialog path used by these commands may be set by the "Save Request File Dialog Folder" setting.

The `File -> Export Response Body...` command is used for save the _raw_ content bytes of a response.  The default file dialog path used by this command may be set by the "Export Response Body File Dialog Folder" setting.

A default .ior request file may be specified in the application Settings. It is also possible to associate IOR with .ior files so that double-clicking on an .ior file starts IOR with it open.

# Content Encoding #

The character encoding used for content is determined by the content-type header's charset parameter. e.g. `content-type: text/plain; charset=utf-16le` indicates that the content should be encoded as utf-16 little endian with no bom.

Encoding rules are driven by the following w3 resources: http://www.w3.org/International/questions/qa-byte-order-mark, http://www.w3.org/TR/2010/WD-html-polyglot-20100624/#character-encoding, and http://www.w3.org/TR/html4/charset.

We enumerate the rules here:
  * if no charset is given, the default is ISO-8859-1 (ascii compatible 8-bit encoding)
  * utf-8 - encodes the content as utf-8 with the optional BOM included if and only if the the "Encode UTF-8 content with BOM" setting is set to `true`
  * utf-16 - encodes the content as utf-16 big endian with the BOM included
  * utf-16be - encodes the content as utf-16 big endian with no BOM included
  * utf-16le - encodes the content as utf-16 little endian with no BOM included
  * utf-32 - encodes the content as utf-32 big endian with the BOM included
  * utf-32be - encodes the content as utf-32 big endian with no BOM included
  * utf-32le - encodes the content as utf-32 little endian with no BOM included
  * other supported encodings do not include a BOM.

# History and Snapshots #

Request history and response "snapshots" are captured after every request / response is submitted and received. These are accessed via the History menu item (which can be quickly opened with the ALT+H key combo). Whereas request history is persisted after an instance of the application is closed, corresponding response snapshots are only kept for the duration of a running application instance. Requests with response snapshots are indicated in the History menu by a camera icon.

The Max History user setting can be used to control how much history is stored. The Clear menu item under the main History menu item may be used to clear all history.

# Log Viewer #

Accessed via Tools -> Log Viewer, this feature offers a visual and interactive interface for inspecting the application log file (which is kept under <app location>\logs). This can be helpful for investing why the application might not have behaved in an expected way. For example, if you attempt to format the body of a request message as JSON and it fails, this may be able to tell you why it failed.

Error and Warn level log message notifications may appear in the status bar at the bottom of application window. Clicking on these will open the Log Viewer for further inspection.

# Basic Authorization #

Basic Authorization is a form of authorization which involves sending username and password information via the Authorization header. To learn more, see [Basic Access Authentication](http://en.wikipedia.org/wiki/Basic_access_authentication).

Currently, IOR does not support responding to 401 authorization challenges. However, it does provide two means for sending authorization information up front.

The first method is to simply hand craft the Authorization header. The down side of this method is that it requires performing the base64 encoding outside of the editor.

The second method is to enter the username and password info in the URL, e.g. http://username:password@example.com. IOR will perform the required base64 encoding and insert the Authorization header when it sees URLs formatted like this. While many HTTP clients such as web browsers support this URL syntax, the downside (and upside) is that the username and password are easily seen in plain text in the request URL.