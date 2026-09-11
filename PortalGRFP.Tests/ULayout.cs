using NUnit.Framework;
using PortalGRFP.Business.Common.Factories;
using PortalGRFP.Business.Common.FileProcessor.DAT;
using PortalGRFP.Business.Common.FileProcessor.Implementations;
using PortalGRFP.Business.Common.Strategies;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Entities.Models.DatLayouts.FR.FARMACOS;
using PortalGRFP.Entities.Models.DatLayouts.FR.MARZAM;
using PortalGRFP.Entities.Models.DatLayouts.FR.NANDRO;
using PortalGRFP.Entities.Models.DatLayouts.LDCOM;
using PortalGRFP.Entities.Models.DatLayouts.POPSAE;
using System.IO;
using System.Linq;

namespace PortalGRFP.Tests
{
    public class ULayout
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void AVG()
        {
            //string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\FARMACOS\CATALOGO.txt");
            //string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\FARMACOS\CATALOGO.txt");
            //string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\FARMACOS\CATALOGO.txt");
            //string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\FARMACOS\CATALOGO.txt");
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\NADRO\AUTOIICN.067");
            //string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\MARZAM\CATALOGO.DAT");
            var chesum = lines.Select(s => s.Length).Distinct().ToArray();
            Assert.IsNotNull(chesum);
        }

        #region [Pruebas Layouts]
        [Test]
        public void TestNADRO1()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\NADRO\AUTOIICN.067");
            var tem = Normalizer.NormalizeLayout<NANDRO1>(lines);
            Assert.IsNotNull(tem);
        }
        [Test]
        public void TestMARZAM1()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\MARZAM\CATALOGO.DAT");
            var tem = Normalizer.NormalizeLayout<MARZAM1>(lines);
            Assert.IsNotNull(tem);
        }
        [Test]
        public void TestFARMACOS1()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\FARMACOS\CATALOGO.txt");
            var tem = Normalizer.NormalizeLayout<FARMACOS1>(lines);
            Assert.IsNotNull(tem);
        }
        [Test]
        public void TestLDCOM1()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\PMP202009231044.txt");
            var tem = Normalizer.NormalizeLayout<LDCOM1>(lines);
            //new LayoutLoader<LDCOM1>(TipoCargas.PMP_LDCOM, LayoutTypes.LDCOM49).ProcessData(tem, "PMP202009231044.txt", 4);
            Assert.IsNotNull(tem);
        }

        [Test]
        public void POPSAE1()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\ETL\FARMATODO.DAT");
            var tem = Normalizer.NormalizeLayout<POPSAE1>(lines);
            Assert.IsNotNull(tem);
        }       
        #endregion

        #region [Factoria y Estrategia]
        [Test]
        public void DatProcessor()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\FARMATODO.DAT");
            INormalizer tem = new DatProcessor<POPSAE1>(LayoutTypes.POPSAE61);
            var t1 = tem.Process(lines);
            Assert.IsNotNull(t1);
        }

        [Test]
        public void UDatLayoutFactory()
        {
            string[] lines = File.ReadAllLines(@"E:\Trabajo\APSO\FARMATODO.DAT");
            INormalizer tem = DatLayoutFactory.GetReadProcessor(LayoutTypes.POPSAE61);
            var t1 = tem.Process(lines);
            Assert.IsNotNull(t1);
        }

        [Test]
        public void DatLayoutFactoryDynamic()
        {
            var t1 = DatProcessingStrategy.ReadData(@"E:\Trabajo\APSO\PMP202009231044.txt");
            Assert.IsNotNull(t1);
        }
        #endregion
    }
}