using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using ReUse_Std.Base;
using ReUse_Std.Common;

namespace ReUse_Std.Utilities.External.Xml
{
    /// <summary>
    /// Common XML Utilities
    /// </summary>
    public static class XML_Utilities
    {
        #region Load and Save xml

        /// <summary>
        /// Get Data From Xml File with current FilePath and optional AddCurrentDirectory
        /// </summary>
        public static XmlDocument _Xl(this string FilePath, bool AddCurrentDirectory = true)
        {
            XmlDocument xml = new XmlDocument();
            var dir = AddCurrentDirectory ? Environment.CurrentDirectory + "\\" : "";
            xml.Load(dir + FilePath);
            return xml;
        }

        /// <summary>
        /// Get Linq Data From Xml File with current FilePath and optional AddCurrentDirectory
        /// </summary>
        public static XDocument _Xq(this string FilePath, bool AddCurrentDirectory = true)
        {
            var dir = AddCurrentDirectory ? Environment.CurrentDirectory + "\\" : "";
            return XDocument.Load(dir + FilePath);
        }

        /// <summary>
        /// Get new Xml Linq Data with current RootElementTitle and optional AddCurrentDirectory
        /// </summary>
        public static XDocument _Xn(this string RootElementTitle)
        {
            var x = new XDocument(new XElement(RootElementTitle));
            //x.Add(new XElement(RootElementTitle));
            return x;
        }

        /// <summary>
        /// Save Linq Data To Xml File with current FilePath and optional AddCurrentDirectory
        /// </summary>
        public static XDocument _S(this XDocument XmlData, string FilePath, bool AddCurrentDirectory = true)
        {
            var dir = AddCurrentDirectory ? Environment.CurrentDirectory + "\\" : "";
            XmlData.Save(dir + FilePath);
            return XmlData;
        }

        #endregion

        #region Process XML Data

        /// <summary>
        /// Process XML Data from FilePath (with AddCurrentDirectory) using MethodToProcessRootElement with optional SaveDocumentAfterProcess (to optional SaveFilePath and SaveAddCurrentDirectory)
        /// </summary>
        public static T[] X<T>(this string FilePath, f<XElement, T[]> MethodToProcessRootElement, bool AddCurrentDirectory = true, bool SaveDocumentAfterProcess = false, string SaveFilePath = null, bool SaveAddCurrentDirectory = true)
        {
            var r = FilePath._Xq(AddCurrentDirectory);
            var res = MethodToProcessRootElement(r.Root);
            if (SaveDocumentAfterProcess)
                r._S(SaveFilePath ?? FilePath, SaveAddCurrentDirectory);
            return res;
        }

        #endregion

        #region Get data Xml Document

        /// <summary>
        /// Get Data From Xml Element with current ElementPath and AttributePath
        /// </summary>
        public static string _G(this XmlDocument CurrDoc, string ElementPath, string AttributePath = null, Xv ValueType = Xv.V)
        {
            var r = CurrDoc.DocumentElement[ElementPath];
            if (r == null)
                return null;
            if (!AttributePath.C() || !r.HasAttribute(AttributePath))
                return r._G(ValueType);

            var a = r.Attributes[AttributePath];
            if (a == null)
                return null;
            return a._G(ValueType);
        }

        /// <summary>
        /// Get Attributes Data From Xml Element with current ElementPath and optional TheseAttributesOnly (or all)
        /// </summary>
        public static IDictionary<string, string> _G(this XmlDocument CurrDoc, string ElementPath, Xv ValueType = Xv.V, IEnumerable<string> TheseAttributesOnly = null)
        {
            var r = CurrDoc.DocumentElement[ElementPath];
            if (r == null || r.Attributes == null || r.Attributes.Count < 0)
                return null;

            var a = new Dictionary<string, string>();
            foreach (XmlAttribute item in r.Attributes)
                if (TheseAttributesOnly == null || TheseAttributesOnly.Contains(item.Name))
                    a.Add(item.Name, item._G(ValueType));

            return a;
        }


        /// <summary>
        /// Get Data From current XmlElement
        /// </summary>
        public static string _G(this XmlElement CurrElement, Xv ValueType = Xv.V)
        {
            return ValueType == Xv.T ? CurrElement.InnerText : ValueType == Xv.X ? CurrElement.InnerXml : ValueType == Xv.O ? CurrElement.OuterXml : CurrElement.Value;
        }

        /// <summary>
        /// Get Data From current XmlAttribute
        /// </summary>
        public static string _G(this XmlAttribute CurrElement, Xv ValueType = Xv.V)
        {
            return ValueType == Xv.T ? CurrElement.InnerText : ValueType == Xv.X ? CurrElement.InnerXml : ValueType == Xv.O ? CurrElement.OuterXml : CurrElement.Value;
        }

        #endregion

        #region Get data Linq

        /// <summary>
        /// Get Common Data with chaining (return CurrRootElem) From current CurrRootElem with MethodToProcess with optional SubElementPath (or CurrRootElem) and SelectElementsPath (sub select)
        /// </summary>
        public static XElement _G<T>(this XElement CurrRootElem, out T CurrentValue, f<XElement, T> MethodToProcess, string SubElementPath = null, string SelectElementsPath = null)
        {
            CurrentValue = CurrRootElem._g(MethodToProcess, (ref XElement i) =>
            {
                if (SubElementPath != null)
                {
                    if (SelectElementsPath != null)
                    {
                        var q = CurrRootElem.Element(SelectElementsPath);
                        if (q != null)
                            i = q.Element(SubElementPath);
                        else
                            i = null;
                    }
                    else
                        i = CurrRootElem.Element(SubElementPath);
                }

            });
            return CurrRootElem;
        }

        /// <summary>
        /// Get Attributes Data and Element Values From current CurrRootElem with optional SubElementPath (or CurrRootElem) and TheseAttributesOnly (or all)
        /// </summary>
        public static IDictionary<string, string> _G(this XElement CurrRootElem, IEnumerable<string> TheseAttributesOnly = null, string SubElementPath = null, string SelectElementsPath = null, bool AddElementValue = true)
        {
            var rs = new Dictionary<string, string>();
            CurrRootElem._G(out rs, r =>
            {
                var a = new Dictionary<string, string>();
                if (AddElementValue)
                    a.Add(r.Name.LocalName, r.Value);
                var att = r.Attributes();
                if (att == null || att.Count() < 0)
                    return a;

                foreach (XAttribute item in att)
                    if (TheseAttributesOnly == null || TheseAttributesOnly.Contains(item.Name.LocalName))
                        a.Add(item.Name.LocalName, item.Value);

                return a;
            }, SubElementPath, SelectElementsPath);
            return rs;
        }

        /// <summary>
        /// Get Common Data with chaining (return CurrRootElem) From current CurrRootElem with MethodToProcess with optional SubElementPath (or child Elements), TheseAttributesOnly (or all) and SelectElementsPath (sub select)
        /// </summary>
        public static XElement _Gd<T>(this XElement CurrRootElem, out T CurrentValue, f<IDictionary<string, string>, T> MethodToProcess, string SubElementPath = null, string SelectElementsPath = null, IEnumerable<string> TheseAttributesOnly = null, bool AddElementValue = true)
        {
            return CurrRootElem._G(out CurrentValue, r =>
            {
                return MethodToProcess(r._G(TheseAttributesOnly, SelectElementsPath, null, AddElementValue));
            }, SubElementPath);
        }

        /// <summary>
        /// Get List of Common Data with chaining (return CurrRootElem) From current CurrRootElem Elements with MethodToProcess with optional SubElementPath (or child Elements) and SelectElementsPath (container for array) 
        /// </summary>
        public static XElement _Ga<T>(this XElement CurrRootElem, out IEnumerable<T> CurrentValues, f<XElement, T> MethodToProcess, string SubElementsPath = null, string SelectElementsPath = null)
        {
            if (SelectElementsPath != null)
            {
                if (SubElementsPath != null)
                    CurrentValues = CurrRootElem.Element(SelectElementsPath)._g(e => e.Elements(SubElementsPath), MethodToProcess);
                else
                    CurrentValues = CurrRootElem.Element(SelectElementsPath)._g(e => e.Elements(), MethodToProcess);
            }
            else
            {
                if (SubElementsPath != null)
                    CurrentValues = CurrRootElem._g(e => e.Elements(SubElementsPath), MethodToProcess);
                else
                    CurrentValues = CurrRootElem._g(e => e.Elements(), MethodToProcess);
            }

            return CurrRootElem;
        }

        /// <summary>
        /// Get List of Attributes Data with chaining (return CurrRootElem) and Element Values From current CurrRootElem Elements with MethodToProcess with optional SubElementPath (or child Elements), TheseAttributesOnly (or all) and SelectElementsPath (sub select)
        /// </summary>
        public static XElement _Gd<T>(this XElement CurrRootElem, out IEnumerable<T> CurrentValues, f<IDictionary<string, string>, T> MethodToProcess, string SubElementsPath = null, string SelectElementsPath = null, IEnumerable<string> TheseAttributesOnly = null, bool AddElementValue = true)
        {
            return CurrRootElem._Ga(out CurrentValues, r =>
            {
                return MethodToProcess(r._G(TheseAttributesOnly, SelectElementsPath, null, AddElementValue));
            }, SubElementsPath);
        }


        #endregion

        #region Set data Linq

        /// <summary>
        /// Add Element with Common Data with chaining (return CurrRootElem) to current CurrRootElem with ElementName and MethodToProcess and optional SubElementPath (or CurrRootElem)
        /// </summary>
        public static XElement _S(this XElement CurrRootElem, string ElementName, f<IEnumerable<object>> MethodToProcess, string SubElementPath = null, fr<XElement> MethodToPostProcessElement = null)
        {
            var s = SubElementPath.C();
            return CurrRootElem._gc(r =>
            {
                var x = MethodToProcess();
                if (x != null)
                {
                    var d = new XElement(ElementName, x.ToArray());
                    if (MethodToPostProcessElement != null)
                        MethodToPostProcessElement(ref d);
                    if (s)
                        CurrRootElem.Element(SubElementPath).Add(d);
                    else
                        CurrRootElem.Add(d);
                }
            }, (ref XElement i) =>
            {
                if (s)
                {
                    i = CurrRootElem.Element(SubElementPath);
                    if (i == null)
                    {
                        i = new XElement(SubElementPath);
                        CurrRootElem.Add(i);
                    }
                }
            });
        }

        /// <summary>
        /// Add Element with Attributes Data with chaining (return CurrRootElem) to current CurrRootElem with ElementName and optional SubElementPath (or CurrRootElem) and TheseAttributesOnly (or all)
        /// </summary>
        public static XElement _S(this XElement CurrRootElem, string ElementName, string SubElementPath = null, IDictionary<string, object> Attributes = null, fr<XElement> MethodToPostProcessElement = null)
        {
            return CurrRootElem._S(ElementName, () =>
            {
                return Attributes.Select(e => (object)(new XAttribute(e.Key, e.Value)));
            }, SubElementPath, MethodToPostProcessElement);
        }

        /// <summary>
        /// Add Element with Common Data with chaining (return CurrRootElem) to current CurrRootElem with ElementName and MethodToProcess and optional SubElementPath (or CurrRootElem)
        /// </summary>
        public static XElement _Sd(this XElement CurrRootElem, string ElementName, f<IDictionary<string, object>> MethodToProcess, string SubElementPath = null, fr<XElement> MethodToPostProcessElement = null)
        {
            return CurrRootElem._S(ElementName, () =>
            {
                var x = MethodToProcess();
                if (x == null)
                    return null;
                return x.S(e => new XAttribute(e.Key, e.Value ?? ""));
            }, SubElementPath, MethodToPostProcessElement);
        }

        /// <summary>
        /// Add List of Elements with Common Data with chaining (return CurrRootElem) to current CurrRootElem with ElementName and MethodToProcess and optional SubElementPath (or CurrRootElem), SubElementCounter (no)
        /// </summary>
        public static XElement _Sa<T>(this XElement CurrRootElem, string ElementName, IEnumerable<T> Data, f<T, IEnumerable<object>> MethodToProcess, string SubElementPath = null, frv<XElement, T> MethodToPostProcessElement = null, string SubElementCounter = null)
        {
            if (Data == null)
                return CurrRootElem;
            var s = SubElementPath.C();
            var c = Data.Count();
            var n = SubElementCounter ?? "no";
            return CurrRootElem._gc(e => Data, r =>
            {
                var x = MethodToProcess(r);
                if (x.C())
                {
                    var d = new XElement(ElementName, x.ToArray());
                    if (MethodToPostProcessElement != null)
                        MethodToPostProcessElement(ref d, r);
                    if (s)
                        CurrRootElem.Element(SubElementPath).Add(d);
                    else
                        CurrRootElem.Add(d);
                }
            }, (ref XElement i) =>
            {
                if (s)
                {
                    i = CurrRootElem.Element(SubElementPath);
                    if (i == null)
                    {
                        i = new XElement(SubElementPath, new XAttribute(n, c));
                        CurrRootElem.Add(i);
                    }
                    else
                    {
                        if (i.Attribute(n) == null)
                            i.Add(new XAttribute(n, c));
                        else
                            i.SetAttributeValue(n, c);
                    }
                }
            });
        }

        /// <summary>
        /// Add List of Elements with Common Data with chaining (return CurrRootElem) to current CurrRootElem with ElementName and MethodToProcess and optional SubElementPath (or CurrRootElem)
        /// </summary>
        public static XElement _Sda<T>(this XElement CurrRootElem, string ElementName, IEnumerable<T> Data, f<T, IDictionary<string, object>> MethodToProcess, string SubElementPath = null, frv<XElement, T> MethodToPostProcessElement = null)
        {
            return CurrRootElem._Sa(ElementName, Data, (i) =>
            {
                var x = MethodToProcess(i);
                if (x == null)
                    return null;
                return x.Select(e => new XAttribute(e.Key, e.Value ?? ""));
            }, SubElementPath, MethodToPostProcessElement);
        }


        /// <summary>
        /// Add List of ElementsToAdd with chaining (return CurrRootElem) to current CurrRootElem with ElementName and Attribute Names and optional SubElementPath (or CurrRootElem)
        /// </summary>
        public static XElement _S(this XElement CurrRootElem, IEnumerable<object> ElementsToAdd, string ElementName = "m", string ElementAttr = "x", string SubElementPath = null, string SubElementCounter = null)
        {
            if (ElementsToAdd == null)
                return CurrRootElem;

            var s = SubElementPath.C();
            var c = ElementsToAdd.Count();
            var n = s ? SubElementCounter : "no";

            return CurrRootElem._gc(e => ElementsToAdd, r =>
            {
                var d = new XElement(ElementName, new XAttribute(ElementAttr, r));
                if (s)
                    CurrRootElem.Element(SubElementPath).Add(d);
                else
                    CurrRootElem.Add(d);
            }, (ref XElement i) =>
            {
                if (s)
                {
                    i = CurrRootElem.Element(SubElementPath);
                    if (i == null)
                    {
                        i = new XElement(SubElementPath);
                        CurrRootElem.Add(i);
                    }
                    i.SetAttributeValue(n, c);
                }
            });
        }

        #endregion

        #region private

        /// <summary>
        /// Get common save XML method to add SubElementPath to CurrRootElem
        /// </summary>
        private static fr<XElement> _Sc(this XElement CurrRootElem, string SubElementPath)
        {
            return (ref XElement i) =>
            {
                if (SubElementPath.C())
                {
                    i = CurrRootElem.Element(SubElementPath);
                    if (i == null)
                    {
                        i = new XElement(SubElementPath);
                        CurrRootElem.Add(i);
                    }
                }
            };
        }

        #endregion

        #region Common used Linq



        #region Get



        #endregion

        #region Set

        #endregion

        #endregion
    }

    #region structs

    /// <summary>
    /// Xml Element value types
    /// </summary>
    public enum Xv
    {
        /// <summary>
        /// Xml Element value
        /// </summary>
        V,
        /// <summary>
        /// Xml InnerText value
        /// </summary>
        T,
        /// <summary>
        /// Xml InnerXml value
        /// </summary>
        X,
        /// <summary>
        /// Xml OuterXml value
        /// </summary>
        O
    };

    #endregion
}
