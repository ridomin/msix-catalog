using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace msix_catalog_app
{

    public enum ActivateOptions

    {
        None = 0,
        DesignMode = 0x1,
        NoErrorUI = 0x2,
        NoSplashScreen = 0x4
    }

    [ComImport]
    [Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IApplicationActivationManager
    {
        IntPtr ActivateApplication(String appUserModelId, String arguments, ActivateOptions options, out UInt32 processId);
        IntPtr ActivateForFile(String appUserModelId, IntPtr itemArray, String verb, out UInt32 processId);
        IntPtr ActivateForProtocol(String appUserModelId,  IntPtr itemArray, out UInt32 processId);
    }

    [ComImport]
    [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
    public class ApplicationActivationManager : IApplicationActivationManager
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr itemArray, [In] String verb, [Out] out UInt32 processId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr itemArray, [Out] out UInt32 processId);
    }

    public class PackageActivator
    {
        public static uint StartApp(string pfn)
        {
            uint pid;
            var am = new ApplicationActivationManager();
            try
            {
                am.ActivateApplication(pfn, null, ActivateOptions.None, out pid);
                return pid;
            }
            catch (Exception ex)
            {
                App.TelemetryClient.TrackException(ex);
                System.Windows.MessageBox.Show(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return 0;
            
        }
    }

}
