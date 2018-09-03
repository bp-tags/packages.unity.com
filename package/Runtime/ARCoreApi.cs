using System;
using System.Runtime.InteropServices;
using UnityEngine.XR.ARExtensions;

namespace UnityEngine.XR.ARCore
{
    internal static class Api
    {
        public enum ArStatus
        {
            /// The operation was successful.
            AR_SUCCESS = 0,

            /// One of the arguments was invalid, either null or not appropriate for the
            /// operation requested.
            AR_ERROR_INVALID_ARGUMENT = -1,

            /// An internal error occurred that the application should not attempt to
            /// recover from.
            AR_ERROR_FATAL = -2,

            /// An operation was attempted that requires the session be running, but the
            /// session was paused.
            AR_ERROR_SESSION_PAUSED = -3,

            /// An operation was attempted that requires the session be paused, but the
            /// session was running.
            AR_ERROR_SESSION_NOT_PAUSED = -4,

            /// An operation was attempted that the session be in the TRACKING state,
            /// but the session was not.
            AR_ERROR_NOT_TRACKING = -5,

            /// A texture name was not set by calling ArSession_setCameraTextureName()
            /// before the first call to ArSession_update()
            AR_ERROR_TEXTURE_NOT_SET = -6,

            /// An operation required GL context but one was not available.
            AR_ERROR_MISSING_GL_CONTEXT = -7,

            /// The configuration supplied to ArSession_configure() was unsupported.
            /// To avoid this error, ensure that Session_checkSupported() returns true.
            AR_ERROR_UNSUPPORTED_CONFIGURATION = -8,

            /// The android camera permission has not been granted prior to calling
            /// ArSession_resume()
            AR_ERROR_CAMERA_PERMISSION_NOT_GRANTED = -9,

            /// Acquire failed because the object being acquired is already released.
            /// For example, this happens if the application holds an ::ArFrame beyond
            /// the next call to ArSession_update(), and then tries to acquire its point
            /// cloud.
            AR_ERROR_DEADLINE_EXCEEDED = -10,

            /// There are no available resources to complete the operation.  In cases of
            /// @c acquire methods returning this error, This can be avoided by
            /// releasing previously acquired objects before acquiring new ones.
            AR_ERROR_RESOURCE_EXHAUSTED = -11,

            /// Acquire failed because the data isn't available yet for the current
            /// frame. For example, acquire the image metadata may fail with this error
            /// because the camera hasn't fully started.
            AR_ERROR_NOT_YET_AVAILABLE = -12,

            /// The android camera has been reallocated to a higher priority app or is
            /// otherwise unavailable.
            AR_ERROR_CAMERA_NOT_AVAILABLE = -13,

            /// The ARCore APK is not installed on this device.
            AR_UNAVAILABLE_ARCORE_NOT_INSTALLED = -100,

            /// The device is not currently compatible with ARCore.
            AR_UNAVAILABLE_DEVICE_NOT_COMPATIBLE = -101,

            /// The ARCore APK currently installed on device is too old and needs to be
            /// updated.
            AR_UNAVAILABLE_APK_TOO_OLD = -103,

            /// The ARCore APK currently installed no longer supports the ARCore SDK
            /// that the application was built with.
            AR_UNAVAILABLE_SDK_TOO_OLD = -104,

            /// The user declined installation of the ARCore APK during this run of the
            /// application and the current request was not marked as user-initiated.
            AR_UNAVAILABLE_USER_DECLINED_INSTALLATION = -105
        }

        public enum ArInstallStatus
        {
            /// The requested resource is already installed.
            AR_INSTALL_STATUS_INSTALLED = 0,
            /// Installation of the resource was requested. The current activity will be
            /// paused.
            AR_INSTALL_STATUS_INSTALL_REQUESTED = 1
        }

        public enum ArAvailability
        {
            AR_AVAILABILITY_UNKNOWN_ERROR = 0,
            /// ARCore is not installed, and a query has been issued to check if ARCore
            /// is is supported.
            AR_AVAILABILITY_UNKNOWN_CHECKING = 1,
            /// ARCore is not installed, and the query to check if ARCore is supported
            /// timed out. This may be due to the device being offline.
            AR_AVAILABILITY_UNKNOWN_TIMED_OUT = 2,
            /// ARCore is not supported on this device.
            AR_AVAILABILITY_UNSUPPORTED_DEVICE_NOT_CAPABLE = 100,
            /// The device and Android version are supported, but the ARCore APK is not
            /// installed.
            AR_AVAILABILITY_SUPPORTED_NOT_INSTALLED = 201,
            /// The device and Android version are supported, and a version of the
            /// ARCore APK is installed, but that ARCore APK version is too old.
            AR_AVAILABILITY_SUPPORTED_APK_TOO_OLD = 202,
            /// ARCore is supported, installed, and available to use.
            AR_AVAILABILITY_SUPPORTED_INSTALLED = 203
        }

        public enum ArPrestoApkInstallStatus
        {
            ARPRESTO_APK_INSTALL_UNINITIALIZED = 0,
            ARPRESTO_APK_INSTALL_REQUESTED = 1,

            ARPRESTO_APK_INSTALL_SUCCESS = 100,

            ARPRESTO_APK_INSTALL_ERROR = 200,
            ARPRESTO_APK_INSTALL_ERROR_DEVICE_NOT_COMPATIBLE = 201,
            ARPRESTO_APK_INSTALL_ERROR_USER_DECLINED = 203,
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        [DllImport("UnityARCore")]
        static internal extern void ArPresto_checkApkAvailability(
            Action<ArAvailability, IntPtr> on_result, IntPtr context);

        [DllImport("UnityARCore")]
        static internal extern void ArPresto_requestApkInstallation(
            bool userRequested, Action<ArPrestoApkInstallStatus, IntPtr> on_result, IntPtr context);

        [DllImport("UnityARCore")]
        static internal extern void ArPresto_update();
#else
        static internal void ArPresto_checkApkAvailability(
            Action<ArAvailability, IntPtr> on_result, IntPtr context)
        {
            on_result(ArAvailability.AR_AVAILABILITY_UNKNOWN_ERROR, context);
        }

        static internal void ArPresto_requestApkInstallation(
            bool userRequested, Action<ArPrestoApkInstallStatus, IntPtr> on_result, IntPtr context)
        {
            on_result(ArPrestoApkInstallStatus.ARPRESTO_APK_INSTALL_ERROR, context);
        }

        static internal void ArPresto_update() { }
#endif
    }

    internal static class ARCoreEnumExtensions
    {
        public static SessionAvailability AsSessionAvailability(this Api.ArAvailability arCoreAvailability)
        {
            switch (arCoreAvailability)
            {
                case Api.ArAvailability.AR_AVAILABILITY_SUPPORTED_NOT_INSTALLED:
                case Api.ArAvailability.AR_AVAILABILITY_SUPPORTED_APK_TOO_OLD:
                    return SessionAvailability.Supported;

                case Api.ArAvailability.AR_AVAILABILITY_SUPPORTED_INSTALLED:
                    return SessionAvailability.Supported | SessionAvailability.Installed;

                default:
                    return SessionAvailability.None;
            }
        }

        public static SessionInstallationStatus AsSessionInstallationStatus(this Api.ArPrestoApkInstallStatus arCoreStatus)
        {
            switch (arCoreStatus)
            {
                case Api.ArPrestoApkInstallStatus.ARPRESTO_APK_INSTALL_ERROR_DEVICE_NOT_COMPATIBLE:
                    return SessionInstallationStatus.ErrorDeviceNotCompatible;

                case Api.ArPrestoApkInstallStatus.ARPRESTO_APK_INSTALL_ERROR_USER_DECLINED:
                    return SessionInstallationStatus.ErrorUserDeclined;

                case Api.ArPrestoApkInstallStatus.ARPRESTO_APK_INSTALL_REQUESTED:
                    // This shouldn't happen
                    return SessionInstallationStatus.Error;

                case Api.ArPrestoApkInstallStatus.ARPRESTO_APK_INSTALL_SUCCESS:
                    return SessionInstallationStatus.Success;

                case Api.ArPrestoApkInstallStatus.ARPRESTO_APK_INSTALL_ERROR:
                default:
                    return SessionInstallationStatus.Error;
            }
        }
    }
}
