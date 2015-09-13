namespace TestCommon
open System
open NUnit.Framework
open Payrollee.Common

[<TestFixture>]
type TestSpanOfYears() = 

    [<Test>]
    member x.Should_Return_IntervalName_2013 () =
        let testInterval = SpanOfYears(2013u, 2013u)
        let testName = testInterval.ClassName ()
        Assert.AreEqual ("2013", testName)

    [<Test>]
    member x.Should_Return_IntervalName_2011to2013 () =
        let testInterval = SpanOfYears(2011u, 2013u)
        let testName = testInterval.ClassName ()
        Assert.AreEqual ("2011to2013", testName)

    [<Test>]
    member x.Should_Return_IntervalArray_2011_2015 () =
        let testChangeYears = [| 2011u;2012u;2014u;2016u;2017u;0u|]

        let testYearArray = SeqOfYears(testChangeYears)

        let expIntervalArray = [| 
             SpanOfYears(2011u, 2011u);
             SpanOfYears(2012u, 2013u);
             SpanOfYears(2014u, 2015u);
             SpanOfYears(2016u, 2016u);
             SpanOfYears(2017u, 2099u)
        |]

        let testIntervalArray = testYearArray.ToYearsIntervalList()
        Assert.AreEqual(expIntervalArray, testIntervalArray)

    [<Test>]
    member x.Should_Return_Interval_2011_For_Period_2011 () =
        let testChangeYears = [| 2011u;2012u;2014u;2016u;2017u;0u|]

        let testYearArray = SeqOfYears(testChangeYears)

        let testPeriod = MonthPeriod (2011u, 1u)
        let expInterval = SpanOfYears(2011u, 2011u)
        let testInterval = testYearArray.YearsIntervalForPeriod(testPeriod)
        Assert.AreEqual (expInterval, testInterval)

    [<Test>]
    member x.Should_Return_Interval_2016_For_Period_2016 () =
        let testChangeYears = [| 2011u;2012u;2014u;2016u;2017u;0u|]

        let testYearArray = SeqOfYears(testChangeYears)

        let testPeriod = MonthPeriod (2016u, 1u)
        let expInterval = SpanOfYears(2016u, 2016u)
        let testInterval = testYearArray.YearsIntervalForPeriod(testPeriod)
        Assert.AreEqual (expInterval, testInterval)

    [<Test>]
    member x.Should_Return_Interval_2012to2013_For_Period_2013 () =
        let testChangeYears = [| 2011u;2012u;2014u;2016u;2017u;0u|]

        let testYearArray = SeqOfYears(testChangeYears)

        let testPeriod = MonthPeriod (2013u, 1u)
        let expInterval = SpanOfYears(2012u, 2013u)
        let testInterval = testYearArray.YearsIntervalForPeriod(testPeriod)
        Assert.AreEqual (expInterval, testInterval)

    [<Test>]
    member x.Should_Return_Interval_2017to2099_For_Period_2018 () =
        let testChangeYears = [| 2011u;2012u;2014u;2016u;2017u;0u|]

        let testYearArray = SeqOfYears(testChangeYears)

        let testPeriod = MonthPeriod (2018u, 1u)
        let expInterval = SpanOfYears(2017u, 2099u)
        let testInterval = testYearArray.YearsIntervalForPeriod(testPeriod)
        Assert.AreEqual (expInterval, testInterval)

