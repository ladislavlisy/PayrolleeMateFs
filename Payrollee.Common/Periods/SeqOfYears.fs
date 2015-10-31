

namespace Payrollee.Common
open System

type SeqOfYears(years: uint32[]) = 
    static member END_YEAR_ARRAY with get() = 2100u;

    static member END_YEAR_INTER with get() = 2099u;

    static member TransformZeroToUpto(year: uint32) : uint32 =
        if year = 0u then SeqOfYears.END_YEAR_ARRAY else year

    static member TransformYearsToSpan(yearFrom: uint32, yearUpto: uint32) : SpanOfYears = 
        let tranUpto = SeqOfYears.TransformZeroToUpto(yearUpto)
        let spanUpto = if (tranUpto = yearFrom) then tranUpto else ((uint32)(tranUpto - 1u))
        SpanOfYears(yearFrom, spanUpto)

    member x.InitWithYears(years: uint32[]) : SpanOfYears[] =
        let sortedYears = years |> Array.sortBy SeqOfYears.TransformZeroToUpto
        let beginsCount = Math.Max(0, (sortedYears.Length - 1))
        let beginsYears = Array.sub sortedYears 0 beginsCount
        let finishCount = Math.Max(0, (sortedYears.Length - 1))
        let finishYears = Array.sub sortedYears 1 finishCount
        let sortedZiped = Array.zip beginsYears finishYears
        sortedZiped |> Array.map (fun (from, upto) -> SeqOfYears.TransformYearsToSpan(from, upto)) 

    member x.Milestones with get() = x.InitWithYears(years)

    static member SelectForPeriod(span: SpanOfYears, period: MonthPeriod) : bool = 
        period.Year >= span.YearFrom && period.Year <= span.YearUpto

    member x.YearsIntervalForPeriod(period: MonthPeriod) : SpanOfYears =
        let spanForPeriod = x.Milestones |> Array.filter (fun span -> SeqOfYears.SelectForPeriod(span, period))
        match spanForPeriod with
        | null -> SpanOfYears.Empty()
        | _ -> spanForPeriod.[0] 

    member x.YearsIntervalList() : SpanOfYears[] =
        Array.copy x.Milestones

