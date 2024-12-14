# Csharp-ShortcutFiles

#### Mumbo jumbo for the background of this tool:

This is a proof of concept small utility program that was written after reading a lovely article about threat actors using shortcut files to spread via USB. I wanted to see if I could write a better tool that
could mimic the files from the icon and name.

Basically what I read was that the shortcut files that threat actors used were creating random file names and whatnot which made it more obvious. Since USB drives are used to share and "carry" files from one computer to another, I thought maybe to see if I could make a util that was able to be used as part of a script or other program which can mimic the files eactly how they were. For some reason, this was harder
than I thought it would be and originally I wrote this using something like batch or vbscript or jscript (I can't remember), but it wouldn't do what I needed it to do with getting the icon exactly right so I
used C#, but in order to cover as many platforms as possible, I had to kind stick with whatever .Net version I was working on the windows version which wasn't the newest version at the time. This lead to having to implant my own command line arguments using 2D arrays and parsing the arguments given. This was because C# didn't really have a proper standard command line argument library till a lot more recently.

(I am actually quite proud of this functionality because I don't implant my own 2D arrays like this often)

I also don't really code in C# that much and very rarely do I use this language. I much prefer a language like C++ for use with the WinAPI.

------

Usage:

```c#
Usage:
--targetpath=<file_to_run_in_lnk>
            --path=<path_where_to_save_lnk> 
            --icofile=<file_to_extract_icon> 
            --icopath=<where_to_save_icon> 
```

Example:

```
./Program.exe --targetpath=helloworld.bat --path=D:\\ --icofile=test.pdf --icopath=D:\\ICONS\\
```
------
