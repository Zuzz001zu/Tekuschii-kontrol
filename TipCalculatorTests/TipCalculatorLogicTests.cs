using Microsoft.VisualStudio.TestTools.UnitTesting;
using TipCalculator;
using System;

namespace TipCalculatorTests
{
    /// <summary>
    /// Тесты класса TipCalculatorLogic.
    /// Покрывают: корректные данные, граничные значения, некорректные данные.
    /// Используются: AreEqual, AreNotEqual, IsTrue, IsFalse, ThrowsException.
    /// </summary>
    [TestClass]
    public class TipCalculatorLogicTests
    {
        // ══════════════════════════════════════════════════════
        //  CalculateTip — корректные данные
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculateTip_Bill1000_Tip0_Returns0()
        {
            double result = TipCalculatorLogic.CalculateTip(1000, 0);
            Assert.AreEqual(0.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTip_Bill1000_Tip5_Returns50()
        {
            double result = TipCalculatorLogic.CalculateTip(1000, 5);
            Assert.AreEqual(50.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTip_Bill200_Tip10_Returns20()
        {
            double result = TipCalculatorLogic.CalculateTip(200, 10);
            Assert.AreEqual(20.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTip_Bill400_Tip15_Returns60()
        {
            double result = TipCalculatorLogic.CalculateTip(400, 15);
            Assert.AreEqual(60.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTip_Tip0_ResultIsNotPositive()
        {
            double result = TipCalculatorLogic.CalculateTip(500, 0);
            // При 0% чаевых результат не должен быть положительным
            Assert.IsFalse(result > 0);
        }

        [TestMethod]
        public void CalculateTip_Tip15_ResultIsPositive()
        {
            double result = TipCalculatorLogic.CalculateTip(500, 15);
            Assert.IsTrue(result > 0);
        }

        // ══════════════════════════════════════════════════════
        //  CalculateTip — граничные значения
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculateTip_VerySmallBill_Tip10_Returns0()
        {
            // 0.01 * 10% = 0.001 → Math.Round(..., 2) = 0.00
            double result = TipCalculatorLogic.CalculateTip(0.01, 10);
            Assert.AreEqual(0.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTip_LargeBill_Tip15_ReturnsCorrect()
        {
            double result = TipCalculatorLogic.CalculateTip(100000, 15);
            Assert.AreEqual(15000.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTip_Tip5And15_NotEqual()
        {
            double tip5  = TipCalculatorLogic.CalculateTip(1000, 5);
            double tip15 = TipCalculatorLogic.CalculateTip(1000, 15);
            Assert.AreNotEqual(tip5, tip15);
        }

        // ══════════════════════════════════════════════════════
        //  CalculateTip — некорректные данные (исключения)
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculateTip_ZeroBill_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculateTip(0, 10));
        }

        [TestMethod]
        public void CalculateTip_NegativeBill_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculateTip(-500, 10));
        }

        [TestMethod]
        public void CalculateTip_InvalidPercent7_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculateTip(500, 7));
        }

        [TestMethod]
        public void CalculateTip_InvalidPercent100_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculateTip(500, 100));
        }

        [TestMethod]
        public void CalculateTip_NegativePercent_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculateTip(500, -5));
        }

        // ══════════════════════════════════════════════════════
        //  CalculateTotal — корректные данные
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculateTotal_Bill300_Tip0_ReturnsExactBill()
        {
            double result = TipCalculatorLogic.CalculateTotal(300, 0);
            Assert.AreEqual(300.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTotal_Bill1000_Tip10_Returns1100()
        {
            double result = TipCalculatorLogic.CalculateTotal(1000, 10);
            Assert.AreEqual(1100.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTotal_WithTip_IsGreaterThanBill()
        {
            double result = TipCalculatorLogic.CalculateTotal(500, 5);
            Assert.IsTrue(result > 500);
        }

        [TestMethod]
        public void CalculateTotal_ZeroTip_EqualsBill()
        {
            double result = TipCalculatorLogic.CalculateTotal(750, 0);
            Assert.AreEqual(750.0, result, 0.001);
        }

        [TestMethod]
        public void CalculateTotal_Tip5_NotEqualToBill()
        {
            double result = TipCalculatorLogic.CalculateTotal(750, 5);
            Assert.AreNotEqual(750.0, result);
        }

        // ══════════════════════════════════════════════════════
        //  CalculatePerPerson — корректные данные
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculatePerPerson_1Guest_ReturnsTotal()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(600, 1);
            Assert.AreEqual(600.0, result, 0.001);
        }

        [TestMethod]
        public void CalculatePerPerson_2Guests_ReturnsHalf()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(1000, 2);
            Assert.AreEqual(500.0, result, 0.001);
        }

        [TestMethod]
        public void CalculatePerPerson_4Guests_ReturnsQuarter()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(800, 4);
            Assert.AreEqual(200.0, result, 0.001);
        }

        [TestMethod]
        public void CalculatePerPerson_MultipleGuests_LessThanTotal()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(1000, 3);
            Assert.IsTrue(result < 1000);
        }

        [TestMethod]
        public void CalculatePerPerson_2And4Guests_NotEqual()
        {
            double per2 = TipCalculatorLogic.CalculatePerPerson(1000, 2);
            double per4 = TipCalculatorLogic.CalculatePerPerson(1000, 4);
            Assert.AreNotEqual(per2, per4);
        }

        // ══════════════════════════════════════════════════════
        //  CalculatePerPerson — граничные значения
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculatePerPerson_100Guests_ReturnsSmallAmount()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(100, 100);
            Assert.AreEqual(1.0, result, 0.001);
        }

        // ══════════════════════════════════════════════════════
        //  CalculatePerPerson — некорректные данные (исключения)
        // ══════════════════════════════════════════════════════

        [TestMethod]
        public void CalculatePerPerson_0Guests_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculatePerPerson(500, 0));
        }

        [TestMethod]
        public void CalculatePerPerson_NegativeGuests_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                TipCalculatorLogic.CalculatePerPerson(500, -2));
        }
    }
}
