namespace TestCommon
open System
open NUnit.Framework
open Payrollee.Common

[<TestFixture>]
type TestMonthPeriod() = 
    let testPeriodCodeJan = 201401u
    let testPeriodCodeFeb = 201402u
    let testPeriodCode501 = 201501u
    let testPeriodCode402 = 201402u


    [<Test>]
    member x.Should_Compare_Different_Periods_AsEqual_When_2014_01() =
        let testPeriodOne = MonthPeriod (testPeriodCodeJan)

        let testPeriodTwo = MonthPeriod (testPeriodCodeJan)

        Assert.AreEqual(testPeriodOne, testPeriodTwo)

    [<Test>]
    member x.Should_Compare_Different_Periods_AsEqual_When_2014_02() =
        let testPeriodOne = MonthPeriod (testPeriodCodeFeb)

        let testPeriodTwo = MonthPeriod (testPeriodCodeFeb)

        Assert.AreEqual(testPeriodOne, testPeriodTwo)

    [<Test>]
    member x.Should_Compare_Different_Periods_SameYear_AsGreater() =
        let testPeriodOne = MonthPeriod (testPeriodCodeJan)

        let testPeriodTwo = MonthPeriod (testPeriodCodeFeb)

        Assert.AreNotEqual(testPeriodTwo, testPeriodOne)

        Assert.Greater(testPeriodTwo, testPeriodOne)
     
    [<Test>]
    member x.Should_Compare_Different_Periods_SameYear_AsLess() =
        let testPeriodOne = MonthPeriod (testPeriodCodeJan)

        let testPeriodTwo = MonthPeriod (testPeriodCodeFeb)

        Assert.AreNotEqual(testPeriodOne, testPeriodTwo)

        Assert.Less(testPeriodOne, testPeriodTwo)
    
    [<Test>]
    member x.Should_Compare_Different_Periods_SameMonth_AsGreater() =
        let testPeriodOne = MonthPeriod (testPeriodCodeJan)

        let testPeriodTwo = MonthPeriod (testPeriodCode501)

        Assert.AreNotEqual(testPeriodTwo, testPeriodOne)

        Assert.Greater(testPeriodTwo, testPeriodOne)
    
    [<Test>]
    member x.Should_Compare_Different_Periods_SameMonth_AsLess() =
        let testPeriodOne = MonthPeriod (testPeriodCodeJan)

        let testPeriodTwo = MonthPeriod (testPeriodCode501)

        Assert.AreNotEqual(testPeriodOne, testPeriodTwo)

        Assert.Less(testPeriodOne, testPeriodTwo)
    
    [<Test>]
    member x.Should_Compare_Different_Periods_DifferentYear_AsGreater() =
        let testPeriodOne = MonthPeriod (testPeriodCode402)

        let testPeriodTwo = MonthPeriod (testPeriodCode501)

        Assert.AreNotEqual(testPeriodTwo, testPeriodOne)

        Assert.Greater(testPeriodTwo, testPeriodOne)
    
    [<Test>]
    member x.Should_Compare_Different_Periods_DifferentYear_AsLess() =
        let testPeriodOne = MonthPeriod (testPeriodCode402)

        let testPeriodTwo = MonthPeriod (testPeriodCode501)

        Assert.AreNotEqual(testPeriodOne, testPeriodTwo)

        Assert.Less(testPeriodOne, testPeriodTwo)
    
    [<Test>]
    member x.Should_Return_Periods_Year_And_Month_2014_01() =
        let testPeriodOne = MonthPeriod (testPeriodCodeJan)

        Assert.AreEqual(testPeriodOne.Year, 2014u)
        Assert.AreEqual(testPeriodOne.Month, 1u)

        Assert.AreEqual(testPeriodOne.YearInt(), 2014)
        Assert.AreEqual(testPeriodOne.MonthInt(), 1)

    [<Test>]
    member x.Should_Return_Periods_Year_And_Month_2014_02() =
        let testPeriodTwo = MonthPeriod (testPeriodCodeFeb)

        Assert.AreEqual(testPeriodTwo.Year, 2014u)
        Assert.AreEqual(testPeriodTwo.Month, 2u)

        Assert.AreEqual(testPeriodTwo.YearInt(), 2014)
        Assert.AreEqual(testPeriodTwo.MonthInt(), 2)