using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Module
{
    public static class HandyDefaults
    {
        public static GstCategory DefaultGstCategory(IObjectSpace objectSpace, bool IsPandL)
        {
            var defaultCode = IsPandL ? "GST" : "N-T";
            var gstCode = HandySettings.GetOrMakeSetting(objectSpace, defaultCode, "GST");

            var gstCategory = objectSpace.FindObject<GstCategory>(
                CriteriaOperator.Parse("[Code]=?", gstCode));
            return gstCategory;
        }
    }
}