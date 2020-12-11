// Learn more about F# at http://fsharp.org

open System
open FParsec

type PassportField =
    | Byr of int32
    | Iyr of int32
    | Eyr of int32
    | Hgt of int32
    | Hcl of string
    | Ecl of string
    | Pid of string
    | Cid of string
    
type pp = Passport of PassportField

//ignore spaces and tabs
let ws = skipMany (pchar ' ' <|> pchar '\t')

//read spaces/tabs until a newline or eof is encountered
let eol = ws .>> (skipNewline <|> eof)

let parseInt = ws >>. pint32 .>> ws

let parseByr: Parser<Parser<(int32 -> PassportField),obj>,unit> = skipString "byr" >>. parseInt |>> fun i ->
    if i >= 1920 && i <= 2002
    then (preturn Byr)
    else failFatally "Expecting val between 1920 and 2002"

let parsePassport = ws >>. choice [ parseByr ] >>. eol

let parseAst = manyTill (spaces >>. parsePassport) eof

let tryParse (s: string) =
        match (run parseAst s) with
        | Failure(msg, err, state) -> printfn "Malformed code: %s\n" msg
        | Success(res, state, pos) -> printfn "%A\n" res

[<EntryPoint>]
let main argv =
    tryParse "byr 2003"
    0 // return an integer exit code
