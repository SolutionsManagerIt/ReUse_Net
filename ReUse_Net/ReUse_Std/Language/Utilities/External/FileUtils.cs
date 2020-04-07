using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//using System.Data;
//using System.Data.SqlClient;
//using System.Xml;
using System.IO;
using ReUse_Std.Common;
using ReUse_Std.Base.Texts;
using ReUse_Std.Base;

namespace ReUse_Std.Utilities.External.Files
{
    /// <summary>
    /// File Utils
    /// </summary>
    public static class F_Extensions
    {
        #region Standard operations
        #region Check  Files

        /// <summary>
        /// Check If curr FileWithPath Exists
        /// </summary>
        public static bool _FC(this string FileWithPath_ToCheckIfExists)
        {
            if (FileWithPath_ToCheckIfExists.C())
                return File.Exists(FileWithPath_ToCheckIfExists);
            return false;
        }

        /// <summary>
        /// Check If curr FilesWithPath Exists (any or CheckAllExists)
        /// </summary>
        public static bool _FC(this IEnumerable<string> FilesWithPath, bool CheckAllExists = false)
        {
            if (!FilesWithPath.C())
                return false;

            var Files = FilesWithPath.G();
            var FilesExistsCount = Files.Count(e => File.Exists(e));

            if (FilesExistsCount > 0)
                return (!CheckAllExists || FilesExistsCount == Files.Count());
            return false;
        }

        #endregion

        #region Get  Files

        /// <summary>
        /// Get all FilePaths from current that exists
        /// </summary>
        public static IEnumerable<string> _FGp(this IEnumerable<string> FilesWithPath, IEnumerable<string> DefaultValueOnError = null)
        {
            if (!FilesWithPath.C())
                return DefaultValueOnError;

            var Files = FilesWithPath.G();
            return Files.Where(e => File.Exists(e));
        }

        /// <summary>
        /// Get all byte data from current FilesWithPath
        /// </summary>
        public static IDictionary<string, byte[]> _FGb(this IEnumerable<string> FilesWithPath, IDictionary<string, byte[]> DefaultValueOnError = null)
        {
            if (!FilesWithPath.C())
                return DefaultValueOnError;

            var Files = FilesWithPath._FGp(new List<string>());

            IDictionary<string, byte[]> Result = new Dictionary<string, byte[]>();

            foreach (string FilePath in Files)
            {
                var File = FilePath._FGb();
                if (File != null)
                    Result.Add(FilePath, File);
            }

            if (Result.Count > 0)
                return Result;

            return DefaultValueOnError;
        }

        /// <summary>
        /// Get byte data from current FileWithPath
        /// </summary>
        public static byte[] _FGb(this string FileWithPath, byte[] DefaultValueOnError = null)
        {
            if (FileWithPath._FC())
                return File.ReadAllBytes(FileWithPath);
            return DefaultValueOnError;
        }

        /// <summary>
        /// Get all List of string from current FilesWithPath
        /// </summary>
        public static IDictionary<string, IEnumerable<string>> _FGs(this IEnumerable<string> FilesWithPath, Encoding FileEncoding = null, IDictionary<string, IEnumerable<string>> DefaultValueOnError = null)
        {
            if (!FilesWithPath.C())
                return DefaultValueOnError;

            var Files = FilesWithPath._FGp(new List<string>());

            IDictionary<string, IEnumerable<string>> Result = new Dictionary<string, IEnumerable<string>>();

            foreach (string FilePath in Files)
            {
                var File = FilePath._FGs(FileEncoding);
                if (File != null)
                    Result.Add(FilePath, File);
            }

            if (Result.Count > 0)
                return Result;

            return DefaultValueOnError;
        }

        /// <summary>
        /// Get List of string from current FileWithPath
        /// </summary>
        public static IEnumerable<string> _FGs(this string FileWithPath, Encoding FileEncoding = null, IEnumerable<string> DefaultValueOnError = null)
        {
            if (FileWithPath.C())
                return File.ReadAllLines(FileWithPath, FileEncoding ?? Encoding.ASCII);
            return DefaultValueOnError;
        }

        /// <summary>
        /// Get All Text from current FileWithPath
        /// </summary>
        public static string _FG(this string FileWithPath, Encoding FileEncoding = null, string DefaultValueOnError = null)
        {
            if (FileWithPath.C())
                return File.ReadAllText(FileWithPath, FileEncoding ?? Encoding.ASCII);
            return DefaultValueOnError;
        }

        #endregion

        #region Write  Files

        /// <summary>
        /// Write All current Bytes Values to FileWithPath
        /// </summary>
        public static bool _FS(this byte[] Values, string FileWithPath, bool OverWriteIfExists = false)
        {
            if (Values.C())
                if (!FileWithPath._FC() || OverWriteIfExists)
                {
                    File.WriteAllBytes(FileWithPath, Values);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// Write or Append All current string Values to FileWithPath
        /// </summary>
        public static bool _FS(this IEnumerable<string> Values, string FileWithPath, Encoding FileEncoding = null, bool OverWriteIfExists = false, bool AppendIfExists = true)
        {
            if (Values.C())
                if (!FileWithPath._FC() || OverWriteIfExists)
                {
                    if (AppendIfExists)
                        File.AppendAllLines(FileWithPath, Values, FileEncoding ?? Encoding.ASCII);
                    else
                        File.WriteAllLines(FileWithPath, Values, FileEncoding ?? Encoding.ASCII);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// Write or Append current string Value to FileWithPath
        /// </summary>
        public static bool _FS(this string Values, string FileWithPath, Encoding FileEncoding = null, bool OverWriteIfExists = false, bool AppendIfExists = true)
        {
            if (Values.C())
                if (!FileWithPath._FC() || OverWriteIfExists)
                {
                    if (AppendIfExists)
                        File.AppendAllText(FileWithPath, Values, FileEncoding ?? Encoding.ASCII);
                    else
                        File.WriteAllText(FileWithPath, Values, FileEncoding ?? Encoding.ASCII);
                    return true;
                }

            return false;
        }

        #endregion

        #region Copy / Move  Files

        /// <summary>
        /// Copy or Move current FileWithPathSource to FileWithPathDestination
        /// </summary>
        public static bool _Ft(this string FileWithPathSource, string FileWithPathDestination, bool Move = false, bool OverWriteIfExists = false)
        {
            if (Move)
                if (!FileWithPathDestination._FC() || OverWriteIfExists)
                {
                    File.Move(FileWithPathSource, FileWithPathDestination);
                    return true;
                }
                else
                {
                    File.Copy(FileWithPathSource, FileWithPathDestination, OverWriteIfExists);
                    return true;
                }

            return false;
        }

        #endregion 
        #endregion

        #region Common cases 

        /// <summary>
        /// Process CSV or string Data from FilePath (with AddCurrentDirectory) using MethodToProcessDocument with optional SaveDocumentAfterProcess (to optional SaveFilePath and SaveAddCurrentDirectory)
        /// </summary>
        public static IEnumerable<T> V<T>(this string FilePath, f<List<string>, IEnumerable<T>> MethodToProcessDocument, Encoding FileEncoding = null, bool AddCurrentDirectory = true, bool SaveDocumentAfterProcess = false, string SaveFilePath = null, bool SaveAddCurrentDirectory = true, bool OverWriteIfExists = false, bool AppendIfExists = true)
        {
            var dq = ((AddCurrentDirectory ? Environment.CurrentDirectory + "\\" : "") + FilePath)._FGs(FileEncoding).ToList();

            var res = MethodToProcessDocument(dq);
            if (SaveDocumentAfterProcess)
                dq._FS((SaveAddCurrentDirectory ? Environment.CurrentDirectory + "\\" : "") + SaveFilePath ?? FilePath, FileEncoding, OverWriteIfExists, AppendIfExists);
            return res;
        }

        /// <summary>
        /// Get List of Data from current FileWithPath using MethodToProcessData
        /// </summary>
        public static IEnumerable<T> _FGs<T>(this string FileWithPath, f<string, T> MethodToProcessData, Encoding FileEncoding = null, IEnumerable<T> DefaultValueOnError = null)
        {
            if (MethodToProcessData == null || !FileWithPath.C())
                return DefaultValueOnError;

            var d = File.ReadAllLines(FileWithPath, FileEncoding ?? Encoding.ASCII);
            if (!d.C())
                return DefaultValueOnError;
            return d.S(t => MethodToProcessData(t));
        }

        /// <summary>
        /// Get List of Data from current FileWithPath (CSV like format with delimiters) using MethodToProcessData and optional CustomDelimiter
        /// </summary>
        public static IEnumerable<T> _FGc<T>(this string FileWithPath, f<IEnumerable<string>, T> MethodToProcessData, string CustomDelimiter = null, Encoding FileEncoding = null, IEnumerable<T> DefaultValueOnError = null)
        {
            if (MethodToProcessData == null || !FileWithPath.C())
                return DefaultValueOnError;

            var d = File.ReadAllLines(FileWithPath, FileEncoding ?? Encoding.ASCII);
            if (!d.C())
                return DefaultValueOnError;
            return d.S(t =>
            {
                string[] vl = null;
                if (CustomDelimiter == null)
                {
                    vl = t.Split(";"._A(), StringSplitOptions.None);
                    if (vl.Length == 1)
                        vl = t.Split(","._A(), StringSplitOptions.None);
                }
                else
                    vl = t.Split(CustomDelimiter._A(), StringSplitOptions.None);

                return MethodToProcessData(vl);
            });
        }

        /// <summary>
        /// Write or Append All current Values (as list of strings) to FileWithPath with MethodToProcessData
        /// </summary>
        public static bool _FS<T>(this IEnumerable<T> Values, string FileWithPath, f<T, string> MethodToProcessData, string HeadersData = null, bool OverWriteIfExists = false, bool AppendIfExists = true, Encoding FileEncoding = null)
        {
            if (!Values.C() || MethodToProcessData == null || !FileWithPath.C())
                return false;

            var v = Values.S(e => MethodToProcessData(e));
            if (HeadersData != null)
                v = HeadersData._A().Union(v);
            v._FS(FileWithPath, FileEncoding, OverWriteIfExists, AppendIfExists);

            return false;
        }

        /// <summary>
        /// Write or Append All current Values (CSV like format with delimiters) to FileWithPath with MethodToProcessData
        /// </summary>
        public static bool _FS<T>(this IEnumerable<T> Values, string FileWithPath, f<T, IEnumerable<string>> MethodToProcessData, IEnumerable<string> HeadersData = null, string CustomDelimiter = null, bool OverWriteIfExists = false, bool AppendIfExists = true, Encoding FileEncoding = null)
        {
            if (!Values.C() || MethodToProcessData == null || !FileWithPath.C())
                return false;
            var d = CustomDelimiter ?? ";";
            var v = Values.S(e => MethodToProcessData(e).Gc(d, true, false));
            if (HeadersData != null)
                v = HeadersData.Gc(d, true, false)._A().Union(v);
            return v._FS(FileWithPath, FileEncoding, OverWriteIfExists, AppendIfExists);
        }

        #endregion
    }
}
