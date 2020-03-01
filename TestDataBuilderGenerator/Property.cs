using System;

namespace TestDataBuilderGenerator
{
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Modifier { get; set; }
        public string PrivateName => "_" + char.ToLower(Name[0]) + Name.Substring(1);
        public string ConvertToPrivateField()
        {
            string newModifier = "private";
            return $"{newModifier} {Type} {PrivateName};";
        }
        public string ConvertToBuilderMethod(string BuilderName)
        {
            string name = Name.ToLower();
            return $"public {BuilderName} With{Name}({Type} {name})" + Environment.NewLine +
                $"{{" + Environment.NewLine +
                $"   {PrivateName} = {name};   " + Environment.NewLine +
                $"   return this;" + Environment.NewLine +
                $"}}";
        }
        public string ConvertToPropertySetters()
        {
            return $"{Name} = {PrivateName},";
        }
    }
}
