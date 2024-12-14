using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

class Program {
    static void Main(string[] args) {
         // To check the length of  
        // Command line arguments   
        if(args.Length > 0 && args.Length == 4) { // Check if arguments were given and if they're no more than 4 passed

            string[] arguments = new string[2];
            string[,] arg_values = new string[args.Length,2];
            int arg_index = 0;

           // Loop through the arguments
           for(int i = 0; i < args.Length; i++) {
                // Split at the =
                arguments = args[i].Split('=');

                // Check if the first element of arguments after split is a valid flag
                bool check = CheckFlag(arguments[0]);

                // If true, continue
                if(check == true) {
                    // Assign first and second element of argument to the 2D array
                    arg_values[arg_index, 0] = arguments[0];
                    arg_values[arg_index, 1] = arguments[1];

                    // Increase the first dimension placement by one
                    arg_index++;
                
                // If false, print usage
                } else {
                    Usage();
                }

            }

            // Pass the 2D array and length to the initlization function
            InitValues(arg_values, args.Length);
        
        // If not enough args are passed, print usage
        } else {
            Usage();
        }
    }
    static void Usage() {
        string CurrentProgram = AppDomain.CurrentDomain.FriendlyName;
        Console.WriteLine("Usage: " + CurrentProgram );
        Console.WriteLine(@"--targetpath=<file_to_run_in_lnk>
            --path=<path_where_to_save_lnk> 
            --icofile=<file_to_extract_icon> 
            --icopath=<where_to_save_icon> ");
        Environment.Exit(1);
    }
    
    static void InitValues(string[,] flagvalues, int length) {

        string value = "";
        string flag = "";
        
        string targetpath = "";
        string path = "";
        string fileicon = "";
        string pathicon = "";
        
        // Loop till whatever is length
        for(int i = 0; i < length; i++) {

           // Assign first element in the 2D array place to flag
           // Assign second element in the 2D array place to value
           flag = flagvalues[i, 0];
           value = flagvalues[i, 1];

           // Assign value to whatever variable depending on the flag value
           if(flag == "--targetpath") {
                targetpath = value;
           } else if(flag == "--path") {
                path = value;
           } else if(flag == "--icofile") {
                fileicon = value;
           } else if(flag == "--icopath") {
                pathicon = value;
           } else {
                // Break if a invalid flag was found
                break;
           }
        }
        // Pass all the variables from the if-else block to the shortcut function
        CreateShortcut(targetpath, path, fileicon, pathicon);

    }

    static bool CheckFlag(string flag) {
        string[] flags = {"--targetpath", "--path", "--icofile", "--icopath"};

        // Check if the flag is valid in the flags array
        if(flags.Contains(flag)) {
            return true;
        } else {
            return false;
        }
    }

    static void CreateShortcut(string targetpath, string path, string fileicon, string pathicon) {
                IShellLink link = (IShellLink)new ShellLink();

                // Extract icon of file

                Icon AppIcon = Icon.ExtractAssociatedIcon(fileicon);

                // Check if icon is null or not
                if (AppIcon != null) {
                    // Save the icon or do something else with it..
                    // Create a stream
                    FileStream fs = new FileStream(pathicon, FileMode.OpenOrCreate);
                    // Save the icon
                    AppIcon.Save(fs);
                    // Close the filestream
                    fs.Close();
                }

                // setup shortcut information
                link.SetDescription("This is the description when hovered over.");
                link.SetPath(targetpath);
                link.SetIconLocation(pathicon, 0);
                link.SetShowCmd(0);

                // save it
                IPersistFile file = (IPersistFile)link;
                //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //file.Save(Path.Combine(desktopPath,path), false);
                file.Save(path, false);
    }
}

//This could always be better adjusted to take more input instead of just being set.
//Then outside of your class but in your namespace
[ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    internal class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotkey(out short pwHotkey);
        void SetHotkey(short wHotkey);
        void GetShowCmd(out int piShowCmd);
        void SetShowCmd(int iShowCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
        void Resolve(IntPtr hwnd, int fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }