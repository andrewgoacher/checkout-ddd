module Kata.Domain.Core
type DomainError =
    | AggregateNotFound of string
    | EntityNotFound of string
    | Validation of string
    | GenericError of string

type Quantity = private Quantity of int
type Money = private Money of decimal
type Price = private Price of Money
type Description = private Description of string

module Money =

    let createMoney (amount:decimal) =
        if System.Math.Round(amount,2) <> amount then
            Error <| Validation "Too many decimal places"
        else
            Ok <| Money amount

    let toDecimal money =
        let (Money m) = money
        m

module Price =

    let createPrice amount =
        if amount <= 0m then
            Error <| Validation "Price must be more than 0"
        else
            match Money.createMoney amount with
            | Error e -> Error e
            | Ok money -> Ok <| Price money

module Description =
    let createDescription desc =
        if System.String.IsNullOrEmpty desc then
            Error <| Validation "Description should not be an empty string"
        else
            Ok <| Description desc

module Quantity =

    let createQuantity amount =
        if amount < 0 then
            Error <| Validation "Amount should be greater than 0"
        else
            Ok <| Quantity amount

    let increment qty amount =
        let (Quantity q) = qty
        createQuantity (q + amount)

    let incrementBy qty1 qty2 =
        let (Quantity q1) = qty1
        let (Quantity q2) = qty2
        createQuantity (q1 + q2)

    let decrementBy qty1 qty2 =
        let (Quantity q1) = qty1
        let (Quantity q2) = qty2
        createQuantity (q1 - q2)

    let cost qty price =
        let (Quantity q) = qty
        let (Price money) = price
        let (Money amount) = money
        match Money.createMoney ((decimal)q * amount) with
        | Ok money -> money
        | _ -> Money 0m