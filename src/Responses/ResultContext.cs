using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Responses
{
    public static class ResultContext
    {
        public static readonly LayerEnum Layer;
        public static readonly string ApplicationName;

        static ResultContext()
        {
            var config = GetConfiguration();
            Layer = config.Layer;
            ApplicationName = config.ApplicationName;
        }

        public static (LayerEnum Layer, string ApplicationName) GetConfiguration()
        {
            var assemblyName = AssemblyContext.GetAssemblyName();

            var split = assemblyName.Split('.');
            if (!Enum.TryParse<LayerEnum>(split[0], out var layer))
                layer = LayerEnum.None;

            if (split.Length > 1)
                return (layer, GetApplicationName(split[1]));
            else
                return (layer, GetApplicationName(split[0]));
        }

        private static string GetApplicationName(string applicationName)
        {
            var upperLetters = Regex.Matches(applicationName, "[A-Z]");

            int lastUpperLetter = -1;

            string result = string.Join(string.Empty, upperLetters.Cast<Match>().Take(4).Select(x => x.Value));

            if (upperLetters.Count >= 4) return result;
            else if (upperLetters.Count > 0)
                lastUpperLetter = applicationName.IndexOf(upperLetters[upperLetters.Count - 1].Value);

            if (lastUpperLetter >= -1)
                return (result + string.Join(string.Empty, applicationName.Skip(lastUpperLetter + 1).Take(4 - upperLetters.Count))).ToUpper();

            return null;
        }
    }
}