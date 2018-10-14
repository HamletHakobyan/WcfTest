using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class MyService : IMyService
    {
        private readonly IEventHandler _eventHandler;

        public MyService(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }



        public async Task<DoubleReturned> GetAgeAsync()
        {
            var str =_eventHandler.PublishNeedData(new NeedData {InputData = "abc"});
            _eventHandler.PublishDoubleReturned(new DoubleReturned{DoubledValue = str.Length});
            OperationContext.Current.OperationCompleted += Current_OperationCompleted;
            await Task.Delay(2000);
            return new DoubleReturned{DoubledValue = 84};
        }

        public Task<string> GetImpersonatedName(int processId)
        {
            SafeProcessHandle processHandle = NativeMethods.OpenProcess(NativeMethods.PROCESS_QUERY_INFORMATION, false, (int)processId);
            SafeAccessTokenHandle tokenHandle;
            NativeMethods.OpenProcessToken(processHandle.DangerousGetHandle(), NativeMethods.TokenAccessLevels.AllAccess, out tokenHandle);
            using (WindowsIdentity newId = new WindowsIdentity(tokenHandle.DangerousGetHandle()))
            {
                using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
                {
                    return Task.FromResult(GetData());
                }
            }
        }

        private string GetData()
        {
            string regData = "<ERROR>";
            string text = "<ERROR>";
            if (NativeMethods.ERROR_SUCCESS == NativeMethods.RegOpenCurrentUser(NativeMethods.KEY_ALL_ACCESS, out SafeRegistryHandle registryHandle))
            {
                try
                {
                    using (registryHandle)
                    using (var key = RegistryKey.FromHandle(registryHandle))
                    using (var subKey = key.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Abc"))
                    {
                        regData = (string)subKey.GetValue("Name");
                    }
                }
                catch { }
            }

            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            try
            {
                text = File.ReadAllText(Path.Combine(path, "a.txt"));
            }
            catch
            {
            }

            return $"{WindowsIdentity.GetCurrent().Name} - {path} - {regData} - {text}";
        }

        public Task<string> GetName()
        {
            return Task.FromResult(GetData());
        }

        private async void Current_OperationCompleted(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            _eventHandler.PublishTrippleReturned(new TrippleReturned { TrippleValue = 3 * 500 });
        }

    }



    //void Main()
    //{
        //	SafeAccessTokenHandle finalToken;
        //	OpenProcessToken(GetCurrentProcess(), TokenAccessLevels.MaximumAllowed, out finalToken).Dump();
        //	finalToken.Dump();
    //}

    internal static class NativeMethods
    {

        const string KERNEL32 = "kernel32.dll";
        const String ADVAPI32 = "advapi32.dll";
        internal const int PROCESS_QUERY_INFORMATION = 0x0400;
        internal const uint KEY_ALL_ACCESS = 0xF003F;
        internal const int ERROR_SUCCESS = 0;

        [DllImport(KERNEL32, ExactSpelling = true, SetLastError = true)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport(ADVAPI32, CharSet = CharSet.Unicode, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        internal static extern
        bool OpenProcessToken(
            [In]     IntPtr ProcessToken,
            [In]     TokenAccessLevels DesiredAccess,
            [Out]    out SafeAccessTokenHandle TokenHandle);

        [DllImport(KERNEL32, CharSet = CharSet.Auto, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        internal static extern uint GetCurrentProcessId();

        [DllImport(ADVAPI32, ExactSpelling = true, SetLastError = true)]
        internal static extern uint RegOpenCurrentUser(uint samDesired, out SafeRegistryHandle resultKey);

        [DllImport(KERNEL32, CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern SafeProcessHandle OpenProcess(int access, bool inherit, int processId);

        [Serializable]
        [Flags]
        [System.Runtime.InteropServices.ComVisible(true)]
        public enum TokenAccessLevels
        {
            AssignPrimary = 0x00000001,
            Duplicate = 0x00000002,
            Impersonate = 0x00000004,
            Query = 0x00000008,
            QuerySource = 0x00000010,
            AdjustPrivileges = 0x00000020,
            AdjustGroups = 0x00000040,
            AdjustDefault = 0x00000080,
            AdjustSessionId = 0x00000100,

            Read = 0x00020000 | Query,

            Write = 0x00020000 | AdjustPrivileges | AdjustGroups | AdjustDefault,

            AllAccess = 0x000F0000 |
                                  AssignPrimary |
                                  Duplicate |
                                  Impersonate |
                                  Query |
                                  QuerySource |
                                  AdjustPrivileges |
                                  AdjustGroups |
                                  AdjustDefault |
                                  AdjustSessionId,

            MaximumAllowed = 0x02000000
        }
    }
}
