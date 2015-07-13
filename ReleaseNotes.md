# Release Notes #

## v1.4.0, 10/17/14 ##

Fixes to New Window command, a new Hex response body viewer, and other features.

  * [Issue 21](https://code.google.com/p/im-only-resting/issues/detail?id=21) Add an "Export Pretty Response Body..." File menu item
  * [Issue 57](https://code.google.com/p/im-only-resting/issues/detail?id=57) Do not prompt for save when executing New Window action
  * [Issue 58](https://code.google.com/p/im-only-resting/issues/detail?id=58) Persist settings and history just before executing the New Window action
  * [Issue 59](https://code.google.com/p/im-only-resting/issues/detail?id=59) Add binary and or hex response body views for file content
  * [Issue 60](https://code.google.com/p/im-only-resting/issues/detail?id=60) Detect zip and other binary file extension endings when exporting response body
  * [Issue 61](https://code.google.com/p/im-only-resting/issues/detail?id=61) Add option to allow ignoring ssl validation errors
  * [Issue 62](https://code.google.com/p/im-only-resting/issues/detail?id=62) Support gzip and deflate automatic decoding
  * [Issue 63](https://code.google.com/p/im-only-resting/issues/detail?id=63) Scroll to x,0 on json or xml format
  * [Issue 64](https://code.google.com/p/im-only-resting/issues/detail?id=64) Submitting HEAD verb causes error
  * [Issue 65](https://code.google.com/p/im-only-resting/issues/detail?id=65) Bad response time numbers

## v1.3.0, 9/7/14 ##

Various enhancements and bug fixes including support for file associations, basic authorization via url user info, and status bar notifications.

  * [Issue 15](https://code.google.com/p/im-only-resting/issues/detail?id=15) Allow associating IOR request files with the app such that opening an IOR file launches it with the file open
  * [Issue 22](https://code.google.com/p/im-only-resting/issues/detail?id=22) Add status bar at bottom of form with error notifications
  * [Issue 26](https://code.google.com/p/im-only-resting/issues/detail?id=26) Add "New" and "New Window" File commands
  * [Issue 35](https://code.google.com/p/im-only-resting/issues/detail?id=35) Remove the Clear button
  * [Issue 46](https://code.google.com/p/im-only-resting/issues/detail?id=46) New Feature: Support Basic http authentication via username:password info in URL
  * [Issue 50](https://code.google.com/p/im-only-resting/issues/detail?id=50) Do not send Expect: 100-continue header unless explicitly specified
  * [Issue 51](https://code.google.com/p/im-only-resting/issues/detail?id=51) Overly aggressive header value validation
  * [Issue 52](https://code.google.com/p/im-only-resting/issues/detail?id=52) Upgrade to NLog 3.1 which contains an important bug fix
  * [Issue 53](https://code.google.com/p/im-only-resting/issues/detail?id=53) Cookie header gets corrupted in actual request

## v1.2.0, 8/1/14 ##

A new Log Viewer, persistent History, and several other improvements.

  * [Issue 30](https://code.google.com/p/im-only-resting/issues/detail?id=30): Pressing Clear button sets Response Time value to "0ms" - it should just set it to blank
  * [Issue 40](https://code.google.com/p/im-only-resting/issues/detail?id=40): Enhancement: New Setting follow redirects
  * [Issue 41](https://code.google.com/p/im-only-resting/issues/detail?id=41): New Feature: easier interface for re-running queries
  * [Issue 42](https://code.google.com/p/im-only-resting/issues/detail?id=42): Cookie ignored in Headers
  * [Issue 44](https://code.google.com/p/im-only-resting/issues/detail?id=44): Upgrade various 3rd party library dependencies
  * [Issue 45](https://code.google.com/p/im-only-resting/issues/detail?id=45): Write content format failures to log file
  * [Issue 48](https://code.google.com/p/im-only-resting/issues/detail?id=48): New Feature: visual log viewer
  * [Issue 49](https://code.google.com/p/im-only-resting/issues/detail?id=49): New Feature: persistent History to compliment snapshots

## v1.1.1, 3/21/13 ##

This release fixes a regression issue from version 1.1.0 where a newly opened file is marked as modified.

  * [Issue 33](https://code.google.com/p/im-only-resting/issues/detail?id=33): Scintilla Regression - TextChanged event is too active and makes newly opened files marked "dirty"

## v1.1.0, 3/15/13 ##

Improved text editing and rendering using Scintilla.

  * [Issue 2](https://code.google.com/p/im-only-resting/issues/detail?id=2): Implement greater under/redo on request text boxes
  * [Issue 27](https://code.google.com/p/im-only-resting/issues/detail?id=27): Attempt to pretty print response data with text/javascript content-type header as json
  * [Issue 29](https://code.google.com/p/im-only-resting/issues/detail?id=29): Regression: Clear button throws exception
  * [Issue 32](https://code.google.com/p/im-only-resting/issues/detail?id=32): Implement Scintilla as rich text editor for text editing and rendering

## v1.0.1, 1/10/13 ##

A couple high-priority bug fixes.

  * [Issue 24](https://code.google.com/p/im-only-resting/issues/detail?id=24): Handle TPL exceptions so that they don't cause fatal app exit
  * [Issue 25](https://code.google.com/p/im-only-resting/issues/detail?id=25): Fatal exception app shutdown on NPE in request Task callback due to unresolved host when Browser render mode

## v1.0.0, 12/8/12 ##

Initial release.