module Kata.Domain.Checkout

open Core

    type DiscountId = private DiscountId of System.Guid
    type BasketId = private BasketId of System.Guid
    type ItemId = private ItemId of string

    type Item = private {
        ParentId: BasketId;
        Id: ItemId;
        Price: Price;
        Quantity: Quantity;
    }

    type Discount = private {
        ParentId: BasketId;
        Id: DiscountId;
        Amount: Money;
        Description: Description;
    }

    type Basket = private {
        Id: BasketId;
        Items: Item list;
        Discounts: Discount list;
    }

    type DiscountEvents =
        | DiscountCreated of DiscountId * BasketId * Description * Money

    type ItemEvents =
        | ItemCreated of BasketId * ItemId * Price * Quantity
        | QuantityIncremented of Quantity
        | QuantityDecremeneted of Quantity

    type BasketEvents =
        | BasketCreated of BasketId
        | ItemAdded of ItemId  * Price * Quantity
        | ItemQuantityIncreased of ItemId * Quantity
        | ItemQuantityReduced of ItemId * Quantity
        | DiscountsRemoved
        | DiscountAdded of DiscountId * Money * Description

    type CheckoutEvent =
        | DiscountEvent of DiscountEvents
        | ItemEvent of ItemEvents
        | BasketEvent of BasketEvents

    module ItemId =
        let createItemId id =
            if System.String.IsNullOrEmpty id then
                Error <| Validation "Item Id cannot be an empty string"
            elif id.Length > 1 then
                Error <| Validation "Item Id should be a single character"
            else
                Ok <| ItemId id

    module Item =
        let newItem (itemEvent: ItemEvents) =
            match itemEvent with
            | ItemCreated (basketId, itemId, price, quantity) ->
                Ok { ParentId = basketId; Id = itemId; Price = price; Quantity = quantity }
            | _ -> Error <| GenericError "Cannot process any other events"

        let apply (item:Item) (evt: CheckoutEvent) =
            match evt with
            | ItemEvent e ->
                match e with
                | QuantityIncremented q ->
                    let newQuantity = Quantity.incrementBy item.Quantity q
                    match newQuantity with
                    | Error e -> Error e
                    | Ok quantity -> Ok { item with Quantity = quantity}
                | QuantityDecremeneted q ->
                    let newQuantity = Quantity.decrementBy item.Quantity q
                    match newQuantity with
                    | Error e -> Error e
                    | Ok quantity -> Ok { item with Quantity = quantity}
            | _ -> Error <| GenericError "Unable to process this event"

        let total item =
            Quantity.cost item.Quantity item.Price

    module DiscountId =
        let createDiscountId id =
            if id = System.Guid.Empty then
                Error <| Validation "Invalid id"
            else
                Ok <| DiscountId id

    module Discount =
        let newDiscount (discountEvent: DiscountEvents) =
            match discountEvent with
            | DiscountCreated (discountId, basketId, description, price) ->
                Ok { ParentId = basketId; Id = discountId; Amount = price; Description = description }

    module BasketId =
        let createBasketId id =
            if id = System.Guid.Empty then
                Error <| Validation "Invalid id"
            else
                Ok <| BasketId id

    module Basket =
        let newBasket (basketEvent: BasketEvents) =
            match basketEvent with
            | BasketCreated id -> Ok { Id = id; Items = []; Discounts = []}
            | _ -> Error <| GenericError "Cannot process this event"

        let total basket =
            let itemsTotal =
                basket.Items
                |> List.map Item.total
                |> List.map Money.toDecimal
                |> List.sum
            let discountsTotal =
                basket.Discounts
                |> List.map (fun (discount: Discount) -> discount.Amount)
                |> List.map Money.toDecimal
                |> List.sum
            Money.createMoney (itemsTotal - discountsTotal)

        let rec apply basket (evt: CheckoutEvent) =
            match evt with
            | BasketEvent e ->
                match e with
                | ItemAdded (itemId, price, quantity) ->
                    let existingItem =
                        basket.Items
                        |> List.exists (fun (item:Item) -> item.Id = itemId)
                    if existingItem then
                        let newEvt = ItemQuantityIncreased (itemId, quantity)
                        apply basket (BasketEvent newEvt)
                    else
                        let item = Item.newItem (ItemCreated (basket.Id, itemId, price, quantity))
                        match item with
                        | Error e -> Error e
                        | Ok i ->
                            let items = i :: basket.Items
                            Ok { basket with Items = items }
                | DiscountsRemoved -> Ok { basket with Discounts = [] }
                | DiscountAdded (id, amount, desc) ->
                    let discountEvent = DiscountCreated (id, basket.Id, desc, amount)
                    let discount = Discount.newDiscount discountEvent
                    match discount with
                    | Error e -> Error e
                    | Ok dis ->
                        let discounts = dis :: basket.Discounts
                        Ok { basket with Discounts = discounts }
                | ItemQuantityIncreased (itemId, quantity) ->
                    // todo: Update me
                    Ok basket
                | ItemQuantityReduced (itemId, quantity) ->
                    // todo: Update me
                    Ok basket
            | _ -> Error <| GenericError "Unable to process event"