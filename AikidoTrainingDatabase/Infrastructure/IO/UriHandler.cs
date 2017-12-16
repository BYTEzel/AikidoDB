using System;
using System.Text.RegularExpressions;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    /// <summary>
    /// The handler class is able to encode string information within a Uri.
    /// This can be used to store the base64 representation within a BitmapImage.
    /// </summary>
    class UriHandler
    {
        private const string ADD_SUFFIX = "Q:/urihandlerstring_";

        public UriHandler()
        {

        }

        /// <summary>
        /// Checks if a Uri is in the UriHandler format and returns true 
        /// if this statement holds true.
        /// </summary>
        /// <param name="uri">Uri which should be checked.</param>
        /// <returns>True, if the uri is of the type UriHandler and contains 
        /// additional information.</returns>
        public bool checkUri(Uri uri)
        {
            return uri.ToString().Contains(ADD_SUFFIX);
        }

        /// <summary>
        /// Creates a valid Uri string containing the additional information.
        /// </summary>
        /// <param name="info">Information which should be encoded in Uri format.</param>
        /// <returns></returns>
        public Uri encodeStringInfo(string info)
        {
            return new Uri(ADD_SUFFIX + info);
        }

        /// <summary>
        /// Get the additional information from a UriHandler uri.
        /// </summary>
        /// <param name="uri">Uri in the UriHandler format.</param>
        /// <returns>The encoded information or string.Empty in case the format is not valid.</returns>
        public string decodeStringInfo(Uri uri)
        {
            if (checkUri(uri))
            {
                return Regex.Split(uri.ToString(), ADD_SUFFIX)[1];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
