using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using iTextSharp.text;
using WIA;
using Image = System.Drawing.Image;

namespace ScannerService.WiaApi
{
    /// <summary>
    /// نشان دهنده کلاس رابط برای کار کردن با اسکنر
    /// </summary>
    public sealed class WiaScannerAdapter : IDisposable
    {

        #region Fields
        public static bool JustImage { get; set; }
        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        private bool _disposed;
        #endregion

        #region Destructor
        ~WiaScannerAdapter()
        {
            Dispose(false);
        }
        #endregion

        #region Properties
        private ICommonDialog WiaManager { get; set; }


        #endregion

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // no managed resources to cleanup
            }

            // cleanup unmanaged resources
            if (WiaManager != null)
                Marshal.ReleaseComObject(WiaManager);

            _disposed = true;
        }

        #endregion

        #region ScanImage
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        public Image ScanImage(ImageFormat outputFormat, string fileName)
        {

            if (outputFormat == null)
                throw new ArgumentNullException("outputFormat");

            var filePerm =
                 new FileIOPermission(FileIOPermissionAccess.AllAccess, fileName);
            filePerm.Demand();

            ImageFile imageObject = null;

            try
            {
                if (WiaManager == null)
                    WiaManager = new CommonDialogClass();
                imageObject =
                     WiaManager.ShowAcquireImage(WiaDeviceType.ScannerDeviceType,
                          WiaImageIntent.ColorIntent, WiaImageBias.MinimizeSize,
                          outputFormat.Guid.ToString("B"), false, true, true);

                imageObject.SaveFile(fileName);
                return Image.FromFile(fileName);
            }
            catch (COMException ex)
            {
                const string message = "Error scanning image";
                throw new WiaOperationException(message, ex);
            }
            finally
            {
                if (imageObject != null)
                    Marshal.ReleaseComObject(imageObject);
            }
        }

        #endregion

        #region InternalClasses
        class WIA_DPS_DOCUMENT_HANDLING_SELECT
        {
            public const uint FEEDER = 0x00000001;
            public const uint FLATBED = 0x00000002;
        }

        class WIA_DPS_DOCUMENT_HANDLING_STATUS
        {
            public const uint FEED_READY = 0x00000001;
        }

        class WIA_PROPERTIES
        {
            private const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
            private const uint WIA_DIP_FIRST = 2;
            private const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            private const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            //
            // Scanner only device properties (DPS)
            //
            private const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
            public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
        }
        #endregion

        #region Scan
        /// <summary>
        /// Use scanner to scan an image (with user selecting the scanner from a dialog).
        /// </summary>
        /// <returns>Scanned images.</returns>
        public static string Scan()
        {
            ICommonDialog dialog = new CommonDialogClass();
            var device = dialog.ShowSelectDevice(WiaDeviceType.UnspecifiedDeviceType, true, false);

            if (device != null)
            {
                return Scan(device.DeviceID);
            }
            else
            {
                throw new COMException("You must select a device for scanning.");
            }
        }

        #endregion

        #region GetScanAsImage
        public static string GetScanAsImage()
        {
            string imgResult;
            var dialog = new CommonDialogClass();
            try
            {
                // نمایش فرم پیشفرض اسکنر
                var image = dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType);

                // ذخیره تصویر در یک فایل موقت
                var filename = Path.GetTempFileName();
                image.SaveFile(filename);
                var img = Image.FromFile(filename);

                // img جهت ارسال سمت کاربر و نمایش در تگ Base64 تبدیل تصویر به 
                imgResult = Convert.ToBase64String(imageToByteArray(img));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // از آنجاییه که امکان نمایش خطا وجود ندارد در صورت بروز خطا رشته خالی 
                // بازگردانده می‌شود که به معنای نبود تصویر می‌باشد

                // a base-64 image to test
                return
                    @"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";
            }
            return imgResult;
        }
        #endregion
    

        #region Scan With Scanner Id
        /// <summary>
        /// Use scanner to scan an image (scanner is selected by its unique id).
        /// </summary>
        /// <param name="scannerId"></param>
        /// <returns>Scanned images.</returns>
        public static string Scan(string scannerId)
        {
            var images = new List<Image>();
            var hasMorePages = true;
            while (hasMorePages)
            {
                var manager = new DeviceManager();
                var device = (from DeviceInfo info in manager.DeviceInfos where info.DeviceID == scannerId select info.Connect()).FirstOrDefault();

                if (device == null)
                {
                    // enumerate available devices
                    var availableDevices = manager.DeviceInfos.Cast<DeviceInfo>().Aggregate("", (current, info) => current + (info.DeviceID + "\n"));

                    // show error with available devices
                    throw new COMException("The device with provided ID could not be found. Available Devices:\n" + availableDevices);
                }

                var item = device.Items[1];
                // scan image
                var wiaCommonDialog = new CommonDialogClass();
                var image = new ImageFile();
                try
                {
                  image = (ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatBMP);

                    // save to temp file
                    var fileName = Path.GetTempFileName();
                    File.Delete(fileName);
                    image.SaveFile(fileName);
                    image = null;

                    // add file to output list
                    images.Add(Image.FromFile(fileName));
                }
                catch (COMException ex)
                {
                    const string message = "Error scanning image";
                    throw new WiaOperationException(message, ex);
                }
                finally
                {
                    item = null;
                    if (image != null) Marshal.ReleaseComObject(image);
                    Marshal.ReleaseComObject(wiaCommonDialog);
                    //determine if there are any more pages waiting
                    Property documentHandlingSelect = null;
                    Property documentHandlingStatus = null;

                    foreach (Property prop in device.Properties)
                    {
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                            documentHandlingSelect = prop;

                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            documentHandlingStatus = prop;
                    }

                    // assume there are no more pages
                    hasMorePages = false;

                    // may not exist on flatbed scanner but required for feeder
                    if (documentHandlingSelect != null)
                    {
                        // check for document feeder
                        if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                        {
                            hasMorePages = ((Convert.ToUInt32(documentHandlingStatus.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0);
                        }
                    }
                }
            }

            return  new ImageToPdf
            {
                FitImagesToPage = true,
                ImageCompressionFormat = ImageFormat.MemoryBmp,
                PdfPageSize = PageSize.A4
            }.ExportToPdfAsBase64String(
                images
                );
        }

        #endregion

        #region Private
        private static byte[] imageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
        #endregion

        #region GetDevices
        /// <summary>
        /// Gets the list of available WIA devices.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDevices()
        {
            var manager = new DeviceManager();

            return (from DeviceInfo info in manager.DeviceInfos select info.DeviceID).ToList();
        }
        #endregion

    }
}

