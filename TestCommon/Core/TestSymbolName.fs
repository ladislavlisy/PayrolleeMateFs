namespace TestCommon
open System
open NUnit.Framework
open Payrollee.Common

[<TestFixture>]
type TestSymbolName() = 
    let testSymbolCode1001 = 1001u;
    let testSymbolCode2001 = 2001u;
    let testSymbolCode3001 = 3001u;
    let testSymbolCode4001 = 4001u;
    let testSymbolCode5001 = 5001u;

    [<Test>]
    member x.Should_Compare_Different_Symbols_AsEqual() =
        let testSymbolOne = SymbolName (testSymbolCode1001, "Begining Symbol")

        let testSymbolTwo = SymbolName (testSymbolCode1001, "Terminal Symbol")

        Assert.AreEqual(testSymbolOne, testSymbolTwo)

    [<Test>]
    member x.Should_Compare_Different_Symbols_AsGreater() =
        let testSymbolOne = SymbolName (testSymbolCode1001, "Begining Symbol")

        let testSymbolTwo = SymbolName (testSymbolCode5001, "Terminal Symbol")

        Assert.AreNotEqual(testSymbolTwo, testSymbolOne)

        Assert.Greater(testSymbolTwo, testSymbolOne)
     
    [<Test>]
    member x.Should_Compare_Different_Symbols_AsLess() =
        let testSymbolOne = SymbolName (testSymbolCode1001, "Begining Symbol")

        let testSymbolTwo = SymbolName (testSymbolCode5001, "Terminal Symbol")

        Assert.AreNotEqual(testSymbolOne, testSymbolTwo)

        Assert.Less(testSymbolOne, testSymbolTwo)
    
