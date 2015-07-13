# ScintillaNET #

ScintillaNET is a managed wrapper / WinForms control around the native Scintilla text editing control used by Notepad++. It gives us rich text editing, syntax highlighting, etc.

IOR is compiled as a Win32 app, therefore we use the native SciLexer.dll instead of SciLexer64.dll. Scintilla.dll is included in the IOR project bin for runtime resolution, but at design-time the dll needs to be placed in C:\Windows to use the WinForms designer. See https://scintillanet.codeplex.com/workitem/23101 for details (note: C:\Windows\System32 does NOT work).