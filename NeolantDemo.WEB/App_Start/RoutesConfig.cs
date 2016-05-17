using System.Globalization;

namespace NeolantDemo.WEB
{
    /// <summary>
    /// Константы маршрутов контроллеров.
    /// </summary>
    public static class RoutesConfig
    {
        internal const string Version = "v1";
        internal const string BaseUrl = "odata/" + Version;
        internal const string Namespace = "Function";

        internal const string ControllerFacilities = "Facilities";
        internal const string ControllerFacilityWithProperties = "FacilityWithProperties";
        internal const string ControllerFacilityClasses = "FacilityClasses";
        internal const string ControllerPropertyKinds = "PropertyKinds";
        internal const string ControllerCommonUniversalProperties = "CommonUniversalProperties";

        internal const string FunctionDescendants = "Descendants";
        internal const string FunctionHierarchy = "Hierarchy";


        internal const string RouteFacilityWithPropertiesDescendantsFilterByKind =
            ODataParameterInstanceS + "/" + Namespace + "." + FunctionDescendants + ODataParameterKindS;

        internal const string RouteFacilityFunctionHierarchy =
            ODataParameterInstanceS + "/" + Namespace + "." + FunctionHierarchy;


        internal const string KindS = "kindS";
        private const string ParameterInstanceS = "{instanceS}";
        private const string ParameterKindS = "{kindS}";
        internal const string ODataParameterInstanceS = "(" + ParameterInstanceS + ")";
        private const string ODataParameterKindS = "(" + KindS + "=" + ParameterKindS + ")";

        /// <summary>
        /// Получает маршрут к объектам, отфильтрованным по родителю и виду объекта + доп. фильтр. 
        /// </summary>
        /// <param name="instanceS">Идентификатор родителя объекта.</param>
        /// <param name="kindS">Идентификатор вида объекта.</param>
        /// <returns></returns>
        public static string GetRouteFacilityWithPropertiesDescendantsFilterByKind(long instanceS, long? kindS)
        {
            string result = string.Format(
                "{0}/{1}{2}", BaseUrl, ControllerFacilityWithProperties, RouteFacilityWithPropertiesDescendantsFilterByKind)
                .Replace(ParameterInstanceS, instanceS.ToString(CultureInfo.InvariantCulture))
                .Replace(ParameterKindS, kindS.HasValue ? kindS.ToString() : "null");

            return result;
        }

        /// <summary>
        /// Получает маршрут к иерархии объекта.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <returns></returns>
        public static string GetRouteFacilityFunctionHierarchy(long instanceS)
        {
            string result = string.Format("{0}/{1}{2}", BaseUrl, ControllerFacilities, RouteFacilityFunctionHierarchy)
                .Replace(ParameterInstanceS, instanceS.ToString(CultureInfo.InvariantCulture));

            return result;
        }        
    }
}