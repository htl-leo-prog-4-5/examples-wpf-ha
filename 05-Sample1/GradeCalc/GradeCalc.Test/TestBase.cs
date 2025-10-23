using GradeCalc.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradeCalc.Test
{
    [TestClass]
    public abstract class TestBase
    {
        [TestInitialize]
        public virtual void InitTest()
        {
            var pt = new PrivateType(typeof(Dependency));
            pt.SetStaticField("_initDone", false);

            Dependency.InitDependencies();
        }
    }
}