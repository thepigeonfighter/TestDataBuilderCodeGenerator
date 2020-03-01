using System;
using System.Collections.Generic;
using System.Text;

namespace TestDataBuilderGenerator
{
    public class ClassBuilder
    {
        private static string _template = "public class #builderType : Builder<#dataType>" + Environment.NewLine +
                                            "    {" + Environment.NewLine +
                                            "        #fields" + Environment.NewLine +
                                            "        #builderMethods" + Environment.NewLine +
                                            "" + Environment.NewLine +
                                            "" + Environment.NewLine +
                                            "        public override #dataType Build()" + Environment.NewLine +
                                            "        {" + Environment.NewLine +
                                            "            return new #dataType" + Environment.NewLine +
                                            "            {" + Environment.NewLine +
                                            "                #properties" + Environment.NewLine +
                                            "            };" + Environment.NewLine +
                                            "        }" + Environment.NewLine +
                                            "    }";
        public static string BuildClass(BuilderClassProperties properties)
        {
            StringBuilder sb = new StringBuilder(_template);
            sb.Replace("#builderType", properties.BuilderType);
            sb.Replace("#dataType", properties.DataType);
            GeneratePrivateFields(sb, properties.PrivateFields);
            GenerateBuilderMethods(sb, properties.PrivateFields, properties.BuilderType);
            GenerateBuildMethod(sb, properties.PrivateFields);
            return sb.ToString();
        }

        private static void GenerateBuildMethod(StringBuilder sb, List<Property> privateFields)
        {
            string properties = "";
            foreach (var prop in privateFields)
            {
                properties += prop.ConvertToPropertySetters() + Environment.NewLine;
            }
            sb.Replace("#properties", properties);
        }

        private static void GenerateBuilderMethods(StringBuilder sb, List<Property> privateFields, string builderName)
        {
            string builderMethods = "";
            foreach (var prop in privateFields)
            {
                builderMethods += prop.ConvertToBuilderMethod(builderName) + Environment.NewLine;
            }
            sb.Replace("#builderMethods", builderMethods);

        }

        private static void GeneratePrivateFields(StringBuilder sb, List<Property> privateFields)
        {
            string fields = "";
            foreach (var prop in privateFields)
            {
                fields += prop.ConvertToPrivateField() + Environment.NewLine;
            }
            sb.Replace("#fields", fields);
        }
    }
}
