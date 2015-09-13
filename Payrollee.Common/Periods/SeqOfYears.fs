

namespace Payrollee.Common
open System

type SeqOfYears(years: uint32[]) = 
    static member END_YEAR_ARRAY with get() = 2100u;

    static member END_YEAR_INTER with get() = 2099u;

    member x.InitWithYears(years: uint32[]) : uint32[] =
        let yearCompare(year: uint32) : uint32 = 
            if year=0u then SeqOfYears.END_YEAR_ARRAY else year

        years |> Array.sortBy yearCompare

    member x.Milestones with get() = x.InitWithYears(years)

    member x.YearsIntervalForPeriod(period: MonthPeriod) : SpanOfYears =
        let forPeriodAccumulator(agr: SpanOfYears) (x: uint32) : SpanOfYears =
            let intYear = match x with
                          | 0u -> SeqOfYears.END_YEAR_ARRAY
                          | _ -> x
            let intFrom = match period.Year with
                          | y when y >= intYear -> intYear
                          | _ -> agr.YearFrom
            let intUpto = match period.Year with
                          | y when y < intYear -> 
                              if agr.YearUpto = 0u then (intYear-1u) else agr.YearUpto
                          | _ -> agr.YearUpto
            SpanOfYears(intFrom, intUpto)

        let forPeriodInitValue = SpanOfYears()
        x.Milestones |> Array.fold (forPeriodAccumulator) forPeriodInitValue 

    member x.ToYearsIntervalList() : SpanOfYears[] =
        let toListAccumulator(agr: SpanOfYears[]) (x: uint32) : SpanOfYears[] =
            let firstPart = agr |> Array.filter (fun y -> y.YearUpto <> 0u)

            match agr.Length with 
            | 0 -> 
                Array.append firstPart [| SpanOfYears(x,0u) |]
            | _ ->    
                let lastPart = Array.get agr (agr.Length-1)
                match x with 
                | 0u -> 
                    let historyFrom = lastPart.YearFrom
                    let historyUpto = SeqOfYears.END_YEAR_INTER

                    Array.append firstPart [| SpanOfYears(historyFrom, historyUpto) |]
                | _ ->
                    let historyFrom = lastPart.YearFrom
                    let historyUpto = max (x-1u) historyFrom

                    Array.append firstPart [| SpanOfYears(historyFrom, historyUpto); SpanOfYears(x, 0u) |]

        let toListInitValue = [| |]
        x.Milestones |> Array.fold (toListAccumulator) toListInitValue 
