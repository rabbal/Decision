namespace ScannerService.WiaApi
{
    public enum WiaScannerError : uint
    {
        LibraryNotInstalled = 0x80040154,
        OutputFileExists = 0x80070050,
        ScannerNotAvailable = 0x80210015,
        OperationCancelled = 0x80210064
    }
}
