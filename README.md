# .Net-Sudoku-Wave-Function-Colapse
A wave function colapse algorithm built in C# .net with the large project being the dynamic rule library for cell masking

## Wave function collapse
 This is an algorithm that procedually generates values based on spacially relative rules and data.
 It can fill a 2D array of values with new values based on rules you provide.

# RULE SETS
 Rules sets are the rules that determine where one value can be releative to another value.
 There are 2D rules and singular cell rules
 Singular cell rules and 2D rules are built in a way that they are intended to be interchangable.
 2D rules in particular require other 2D rules to function.
 ## Singular Cell Rules
  ### Rule Filter
   Contains the filters for what can exist in a singular square.
   This functions on a principle of a hard blacklist, and a soft whitelist.
   The hardblacklists will never be allowed, but a softwhitelist can act as a suggestion for what can be in a spot.
  ### Rule Set
   Can combine multiple different Singular cell rules into one rule on either AND or on OR mode
   This requires other singular cell rules to function
  ### Rule Dictionary
   Contains a dictionary of values. When provided a value, it will compute only on the rule it has setup as a pair with said value.
## 2D Rules
 
