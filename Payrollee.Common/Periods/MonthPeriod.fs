

namespace Payrollee.Common
open System

[<Class>]
type MonthPeriod(code: uint32) = 
    static member PRESENT with get() = 0u

    static member TERM_BEG_FINISHED with get() = 32u

    static member TERM_END_FINISHED with get() = 0u

    static member  WEEKSUN_SUNDAY with get() = 0

    static member  WEEKMON_SUNDAY with get() = 7

    static member CreateFromYearAndMonth(year, month) : MonthPeriod = 
        MonthPeriod(year, month)

    static member Empty() : MonthPeriod = 
        MonthPeriod(MonthPeriod.PRESENT)

    static member BeginYear(year) : MonthPeriod = 
        MonthPeriod(year*100u + 1u)

    static member EndYear(year) : MonthPeriod = 
        MonthPeriod(year * 100u + 12u)

    member x.Code with get() = code

    new (year, month) as this = MonthPeriod(year*100u + month)

    member x.Year with get() = (code / 100u)

    member x.Month with get() = (code % 100u)

    member x.YearInt() = 
        ((int) x.Code / 100)

    member x.MonthInt() = 
        ((int) x.Code % 100)

    member x.MonthOrder() = 
        Math.Max(0, x.YearInt() - 2000)*12 + x.MonthInt()

    member x.DaysInMonth() = 
        DateTime.DaysInMonth(x.YearInt(), x.MonthInt())

    member x.BeginOfMonth() =
        DateTime(x.YearInt(), x.MonthInt(), 1)

    member x.EndOfMonth() =
        DateTime(x.YearInt(), x.MonthInt(), x.DaysInMonth())

    member x.DateOfMonth(dayOrdinal : int) =
        let periodDay = Math.Min(Math.Max(1, dayOrdinal), x.DaysInMonth())
        DateTime(x.YearInt(), x.MonthInt(), periodDay)

    member x.WeekDayOfMonth(dayOrdinal: int) =
        let periodDate = x.DateOfMonth(dayOrdinal)
        let periodDateCwd = periodDate.DayOfWeek
        x.DayOfWeekMonToSun(periodDateCwd)

    member x.DayOfWeekMonToSun(periodDateCwd : DayOfWeek) =
        // DayOfWeek Sunday = 0
        // Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6
        if (periodDateCwd = DayOfWeek.Sunday) then MonthPeriod.WEEKMON_SUNDAY else (int)periodDateCwd

    member x.Description() = 
        let firstPeriodDay = x.BeginOfMonth()
        firstPeriodDay.ToString("MMMM yyyy")

    member x.isEqualToPeriod(other : MonthPeriod) = 
        x.Code = other.Code

     override x.Equals(obj) =
        match obj with
        | :? MonthPeriod as key -> (x.isEqualToPeriod(key))
        | _ -> false
 
    override x.GetHashCode() = 
        hash [x.Code]

    member x.CompareToPeriod(other:MonthPeriod) =
        compare x.Code other.Code

    interface System.IComparable with
      member x.CompareTo obj =
          match obj with
          | :? MonthPeriod as other -> 
              x.CompareToPeriod(other)
          | _ -> invalidArg "obj" "cannot compare values of different types"
   
    override x.ToString() = 
        x.Code.ToString()

