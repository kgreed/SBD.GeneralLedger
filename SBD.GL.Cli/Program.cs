using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Accessibility;
using SBD.GL.Win;

using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Validation.Win;
//using DevExpress.ExpressApp.Xpo;
 


namespace SBD.GL.Cli
{
    /// <summary>
    /// I arent sure that this is really necessary
    /// </summary>
    static class CliProgram
    {
        [STAThread]
        static int Main(string[] args)
        {
            Console.WriteLine("Generating caches");

            using (var winApplication = new GLWindowsFormsApplication())
            {
                try
                {
                     
                    if (Directory.Exists(winApplication.PreCompileOutputDirectory))
                    {
                        Directory.Delete(winApplication.PreCompileOutputDirectory, true);
                    }

                    Directory.CreateDirectory(winApplication.PreCompileOutputDirectory);

                //    InMemoryDataStoreProvider.Register();
               //     winApplication.ConnectionString = InMemoryDataStoreProvider.ConnectionString;
                    winApplication.SplashScreen = null;

                 //   winApplication.Modules.Add(new ValidationModule());
                 //   winApplication.Modules.Add(new ValidationWindowsFormsModule());
                  //  winApplication.Modules.Add(new InlineEditFormsWindowsFormsModule());
                  try
                  {
                      winApplication.Setup();
                    }
                  catch (Exception e)
                  {
                      Console.WriteLine(e);
                        MessageBox.Show(e.ToString());
                      
                      throw;
                  }
                    

                    Console.WriteLine("Fin");

                }
                catch (Exception e)
                {
                    var color = Console.ForegroundColor;
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error:");
                        Console.WriteLine(new string('=', Console.BufferWidth));
                        Console.WriteLine(e.ToString());
                        return 1;
                    }
                    finally
                    {
                        Console.ForegroundColor = color;
                    }
                }
                Console.WriteLine($"Caches created at '{winApplication.PreCompileOutputDirectory}'");
                Console.WriteLine("Caches completed  ( Were you in release mode? )");
                Console.ReadKey();
                return 0;
            }
        }
    }
}
