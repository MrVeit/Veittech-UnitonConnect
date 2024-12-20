using UnityEngine.Networking;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.Core.Utils
{
    public sealed class WebRequestUtils
    {
        public const UnityWebRequest.Result SUCCESS = UnityWebRequest.Result.Success;

        public const UnityWebRequest.Result IN_PROGRESS = UnityWebRequest.Result.InProgress;

        public const UnityWebRequest.Result CONNECTION_ERROR = UnityWebRequest.Result.ConnectionError;
        public const UnityWebRequest.Result PROTOCOL_ERROR = UnityWebRequest.Result.ProtocolError;
        public const UnityWebRequest.Result DATA_PROCESSING_ERROR = UnityWebRequest.Result.DataProcessingError;

        public const string HEADER_CONTENT_TYPE = "Content-Type";
        public const string HEADER_AUTHORIZATION = "Authorization";

        public const string HEADER_VALUNE_CONTENT_TYPE_JSON = "application/json";

        public static void SetRequestHeader(UnityWebRequest webRequest,
            string header, string headerValue)
        {
            webRequest.SetRequestHeader(header, headerValue);
        }

        public static string GetAppManifestLink(bool isTesting, DAppConfig config)
        {
            var dAppManifestLink = ProjectStorageConsts.GetTestAppManifest();

            if (!isTesting && config == null)
            {
                UnitonConnectLogger.LogError("Failed to detect the configuration of your dApp" +
                    " to generate the manifest. It can be assigned via the `Uniton Connect -> dApp Config` configuration window");

                return string.Empty;
            }

            if (!isTesting)
            {
                dAppManifestLink = ProjectStorageConsts.GetAppManifest(config.Data.ProjectLink,
                    ProjectStorageConsts.APP_DATA_FILE_NAME);
            }

            return dAppManifestLink;
        }
    }
}
