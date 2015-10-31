

namespace Payrollee.Common
open System

type SymbolName(code: uint32, name: String) = 
    member x.Code with get() = code

    member x.Name with get() = name

    member x.Description() = 
        x.Name + "::" + x.Code.ToString()

    member x.isEqualToOther(other : SymbolName) = 
        x.Code = other.Code

    override x.Equals(obj) =
        match obj with
        | :? SymbolName as key -> (x.isEqualToOther(key))
        | _ -> false
 
    override x.GetHashCode() = 
        hash [x.Code]

    member x.CompareToSymbol(other:SymbolName) =
        compare x.Code other.Code

    interface System.IComparable with
      member x.CompareTo obj =
          match obj with
          | :? SymbolName as other -> 
              x.CompareToSymbol(other)
          | _ -> invalidArg "obj" "cannot compare values of different types"
   
    override x.ToString() = 
        x.Code.ToString()

