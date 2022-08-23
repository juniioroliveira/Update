using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace Update
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static string programa;


        static void Main(string[] args)
        {

            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            var handle = GetConsoleWindow();

            // Ocultar
            ShowWindow(handle, SW_HIDE);

            // Mostrar
            //ShowWindow(handle, SW_SHOW);

            Thread.Sleep(500);

            INIFile ini = new INIFile();
            programa = Directory.GetCurrentDirectory() + @"\" + ini.IniReadValue("Programa", "Executavel");

            string PathFilesOrigem = Directory.GetCurrentDirectory() + @"\Temp\";
            string PathDestino = Directory.GetCurrentDirectory() + @"\";
            
            if (mover(PathFilesOrigem, PathDestino))
            {
                limpar(PathFilesOrigem);
                executar();
            }
        }

        private static void limpar(string origem)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(origem);

                foreach (FileInfo f in dir.GetFiles())
                {
                    File.Delete(f.FullName);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool mover(string origem, string destino)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(origem);

                foreach (FileInfo f in dir.GetFiles())
                {
                    File.Move(f.FullName, destino + f.Name, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public static void executar()
        {
            System.Diagnostics.Process.Start(programa);
            Environment.Exit(0);
        }
    }
}
