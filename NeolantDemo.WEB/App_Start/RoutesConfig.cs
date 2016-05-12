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
    }
}