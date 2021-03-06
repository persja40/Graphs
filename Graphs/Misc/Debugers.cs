﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Misc
{
    public class Debugers
    {
        public static string DisplayObjectInfo(Object o)
        {
            StringBuilder sb = new StringBuilder();

            // Include the type of the object
            System.Type type = o.GetType();
            sb.Append("Type: " + type.Name);

            // Include information for each Field
            sb.Append("\r\n\r\nFields:");
            System.Reflection.FieldInfo[] fi = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fi.Length > 0)
            {
                foreach (FieldInfo f in fi)
                {
                    if (f.FieldType.IsValueType)
                    {
                        sb.Append("\r\n " + f.ToString() + " = " +
                                  f.GetValue(o));
                    }
                    else
                    {

                        var test = f.GetValue(o);
                        sb.Append("\r\n " + f.ToString() + " = " + DisplayObjectInfo(f.GetValue(o)));
                    }
                }
            }
            else
                sb.Append("\r\n None");

            // Include information for each Property
            sb.Append("\r\n\r\nProperties:");
            System.Reflection.PropertyInfo[] pi = type.GetProperties();
            if (pi.Length > 0)
            {
                foreach (PropertyInfo p in pi)
                {
                    sb.Append("\r\n " + p.ToString() + " = " +
                              p.GetValue(o, null));
                }
            }
            else
                sb.Append("\r\n None");

            return sb.ToString();
        }
    }
}
