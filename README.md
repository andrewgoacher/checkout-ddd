# Checkout Kata

Implement the code for a checkout system that handles pricing schemes such as "pineapples cost 50, three pineapples cost 130."

Implement the code for a supermarket checkout that calculates the total price of a number of items. In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our store, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced individually. In addition, some items are multi-priced: buy n of them, and they’ll cost you y pence. For example, item A might cost 50 individually, but this week we have a special offer: buy three As and they’ll cost you 130. In fact the prices are:

| SKU  | Unit Price | Special Price |
| ---- | ---------- | ------------- |
| A    | 50         | 3 for 130     |
| B    | 30         | 2 for 45      |
| C    | 20         |               |
| D    | 15         |               |

The checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two Bs and price them at 45 (for a total price so far of 95). **The pricing changes frequently, so pricing should be independent of the checkout.**

## Additional requirement

The UK government has introduced a carrier bag charge at all checkouts. For ease of implementation assume that all items must be bagged when scanned at the checkout.

Each carrier bag can hold a maximum of 5 items.

All carrier bags are charged at the price of 5 per bag, except in Wales where bags will be charged at 10.

## Goals

* Refamiliarize yourself with the patterns and tools available in .NET Core e.g., middleware, Filters etc before hand cranking a solution when a canonical solution exists.
* Document a best practice vanilla solution for .NET 5 API which covers key areas in brief (Documented OpenAPI, extensible, testable, secure, extensibility, reusability…).

## Tasks

- [x] Add Swagger Docs for API
- [ ] Create Domain layer
- [ ] Add persistence
- [ ] Add API Layer

## Thought Process

* Going to try and use some of the concepts I've learned from the DDD book I'm reading.
I'm going to stick to a relatively basic pattern because I haven't finished the book and I'm not going to fully realise all the patterns.
  
* Ignoring special discounts for now.
  Adding **Items** to a **Basket** seems a good start.
  * Items have a **name** and a **price**
  * Basket will keep a running of the cost of the items
  * Baket should be able to **add** and **remove** items  
  I like some of the command concepts but at the moment I'm not comfortable building a cQS/CQRS style API
  So for now I'm going to take the Event/Handling style code from the book but not necessarily have commands.    
  Will see how it goes.  


Using Mongo DB 

Open CLI and run

```
mongo
use BasketDb
db.createCollection('Basket')
```