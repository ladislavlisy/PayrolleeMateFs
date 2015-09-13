

namespace Payrollee.Common
open System

type SpanOfYears(yearFrom, yearUpto) = 
    member x.YearFrom with get() = yearFrom

    member x.YearUpto with get() = yearUpto

    new () as this = 
        SpanOfYears(0u, 0u)

    static member CreateFromYear(year) : SpanOfYears = 
        SpanOfYears(year, year)

    static member CreateFromYearToYear(from, upto) : SpanOfYears =
        SpanOfYears(from, upto)

    member x.isEqualToInterval(other : SpanOfYears) = 
        x.YearFrom = other.YearFrom && x.YearUpto = other.YearUpto

     override x.Equals(obj) =
        match obj with
        | :? SpanOfYears as key -> (x.isEqualToInterval(key))
        | _ -> false
    // operator < is provided by IComparable
    // operator > is provided by IComparable
 
    override x.GetHashCode() = 
        hash [x.YearFrom, x.YearUpto]

    member x.CompareToOrder(other:SpanOfYears) =
        let compFrom = compare x.YearFrom other.YearFrom
        match compFrom with
        | 0 -> compare x.YearUpto other.YearUpto
        | _ -> compFrom

    interface System.IComparable with
      member x.CompareTo obj =
          match obj with
          | :? SpanOfYears as other -> x.CompareToOrder(other)
          | _ -> invalidArg "obj" "cannot compare values of different types"
   
    member x.ClassName() =
        let compFrom = compare x.YearFrom x.YearUpto
        match compFrom with
        | 0 -> x.YearFrom.ToString()
        | _ -> x.YearFrom.ToString() + "to" + x.YearUpto.ToString()

    override x.ToString() = 
        x.ClassName()


