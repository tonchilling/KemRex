using Kemrex.Database;
using Kemrex.FWCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kemrex.SoilCalculator.Web.Models
{
    public class SoilCalculatedModel
    {
        private UnitOfWork uow;
        public SoilCalculatedModel(UnitOfWork _uow)
        {
            uow = _uow;
        }
        public int calId { get; set; }
        public decimal inputC { get; set; }
        public decimal inputDegree { get; set; }
        public decimal inputSafeload { get; set; }
        public int ModelId { get; set; }
        public string ProjectName { get; set; }
        public string CalRemark { get; set; }
        private TblPile _ModelInfo;
        public TblPile ModelInfo => _ModelInfo ?? (_ModelInfo = uow.ModelInfo.Get(ModelId));
        public decimal ClayDia { get
            {
                decimal rs = 0;
                try { rs = ModelInfo.PileDia; }
                catch { rs = 0; }
                return rs;
            }
        }
        public decimal ClayAb
        {
            get
            {
                decimal rs = 0;
                try { rs = (decimal)((Math.PI * Math.Pow((double)ClayDia, 2)) / 4); }
                catch { rs = 0; }
                return rs;
            }
        }
        public decimal Qu
        {
            get
            {
                decimal rs = 0;
                try { rs = (9 * inputC * ClayAb); }
                catch { rs = 0; }
                return rs;
            }
        }
        public decimal ShearH
        {
            get
            {
                decimal rs = 0;
                try { rs = ModelInfo.PileSpiralLength; }
                catch { rs = 0; }
                return rs;
            }
        }
        public decimal ShearArea
        {
            get
            {
                decimal rs = 0;
                try { rs = (decimal)(Math.PI * ((double)ClayDia + 0.02) * (double)ShearH); }
                catch { rs = 0; }
                return rs;
            }
        }
        public decimal ShearResist
        {
            get
            {
                decimal rs = 0;
                try { rs = ShearArea * inputC; }
                catch { rs = 0; }
                return rs;
            }
        }

        public decimal UltCompressForce
        {
            get
            {
                decimal rs = 0;
                try { rs = Qu + ShearResist; }
                catch { rs = 0; }
                return rs;
            }
        }

        public decimal Qcomp
        {
            get
            {
                decimal rs = 0;
                try { rs = UltCompressForce / inputSafeload; }
                catch { rs = 0; }
                return rs;
            }
        }

        public decimal UpliftH
        {
            get
            {
                decimal rs = 0;
                try { rs = ModelInfo.PileSpiralDepth; }
                catch { rs = 0; }
                return rs;
            }
        }
        public decimal UpliftHcone
        {
            get
            {
                decimal rs = 0;
                double redians = (Math.PI / 180) * (double)inputDegree;
                try { rs = (decimal)(Math.PI * (Math.Pow((double)UpliftH * Math.Tan(redians), 2) * (double)UpliftH)/3); }
                catch { rs = 0; }
                return rs;
            }
        }

        public decimal WeightOfSoil
        {
            get
            {
                decimal rs = 0;
                try { rs = UpliftHcone * (decimal)1800; }
                catch { rs = 0; }
                return rs;
            }
        }

        public decimal UltUpliftForce
        {
            get
            {
                decimal rs = 0;
                try { rs = WeightOfSoil + ShearResist; }
                catch { rs = 0; }
                return rs;
            }
        }

        public decimal Quplift
        {
            get
            {
                decimal rs = 0;
                try { rs = UltUpliftForce / inputSafeload; }
                catch { rs = 0; }
                return rs;
            }
        }
    }
}