using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mime;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace FoundExtFile
{
    class Program
    {
        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
        System.UInt32 pBC,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        System.UInt32 cbSize,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
        System.UInt32 dwMimeFlags,
        out System.UInt32 ppwzMimeOut,
        System.UInt32 dwReserverd
    );

        static void Main(string[] args)
        {
            string filename = @"D:\Source\TestApp\TestApp\bin\Debug\TestApp.jpg";

            byte[] buffer = new byte[256];
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                if (fs.Length >= 256)
                {
                    fs.Read(buffer, 0, 256);
                }
                else
                {
                    fs.Read(buffer, 0, (int)fs.Length); 
                }
            }
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);

                Marshal.FreeCoTaskMem(mimeTypePtr);
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }
    }
}
