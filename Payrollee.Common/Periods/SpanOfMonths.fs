

namespace Payrollee.Common
open System

type SpanOfMonths(periodFrom: MonthPeriod, periodUpto: MonthPeriod) = 
    member x.PeriodFrom with get() = periodFrom

    member x.PeriodUpto with get() = periodUpto

    new () as this = 
        SpanOfMonths(MonthPeriod.Empty(), MonthPeriod.Empty())

    new (period: MonthPeriod) as this = 
        SpanOfMonths(period, period)

    static member CreateFromYear(year) : SpanOfMonths = 
        SpanOfMonths(MonthPeriod.BeginYear(year), MonthPeriod.EndYear(year))

    static member CreateFromMonth(period) : SpanOfMonths =
        SpanOfMonths(period, period)

    member x.isEqualToInterval(other : SpanOfMonths) = 
        x.PeriodFrom = other.PeriodFrom && x.PeriodUpto = other.PeriodUpto

     override x.Equals(obj) =
        match obj with
        | :? SpanOfMonths as key -> (x.isEqualToInterval(key))
        | _ -> false
    // operator < is provided by IComparable
    // operator > is provided by IComparable
 
    override x.GetHashCode() = 
        hash [x.PeriodFrom.GetHashCode(), x.PeriodUpto.GetHashCode()]

    member x.CompareToInterval(other:SpanOfMonths) =
        let compFrom = x.PeriodFrom.CompareToPeriod(other.PeriodFrom)
        match compFrom with
        | 0 -> x.PeriodUpto.CompareToPeriod(other.PeriodUpto)
        | _ -> compFrom

    interface System.IComparable with
      member x.CompareTo obj =
          match obj with
          | :? SpanOfMonths as other -> x.CompareToInterval(other)
          | _ -> invalidArg "obj" "cannot compare values of different types"
   
    member x.ClassName() =
        let compFrom = x.PeriodFrom.CompareToPeriod(x.PeriodUpto)
        match compFrom with
        | 0 -> x.PeriodFrom.ToString()
        | _ -> x.PeriodFrom.ToString() + "to" + x.PeriodUpto.ToString()

    override x.ToString() = 
        x.ClassName()


